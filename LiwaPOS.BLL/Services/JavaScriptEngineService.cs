using Jint;
using Jint.Runtime;
using LiwaPOS.BLL.Factories;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Context;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LiwaPOS.BLL.Services
{
    public class JavaScriptEngineService
    {
        private readonly DataContext _context;
        private readonly ActionFactory _actionFactory;
        private readonly ICustomNotificationService _customNotificationService;
        private readonly IAppActionService _appActionService;
        private readonly IHttpService _httpService;

        public JavaScriptEngineService(DataContext context, ActionFactory actionFactory, ICustomNotificationService customNotificationService, IAppActionService appActionService, IHttpService httpService)
        {
            _context = context;
            _actionFactory = actionFactory;
            _customNotificationService = customNotificationService;
            _appActionService = appActionService;
            _httpService = httpService;
        }

        // JavaScript kodunu çalıştırmak için kullanılan method
        public object ExecuteJavaScript(string script)
        {
            // Jint JavaScript motorunu oluştur
            var engine = new Engine(cfg =>
            {
                cfg.AllowClr(); // .NET'te tanımlanan action'ları JavaScript içinde kullanmak için
                cfg.TimeoutInterval(TimeSpan.FromMinutes(1)); // JavaScript kodunun çalışma süresini sınırlamak için
                cfg.MaxStatements(1000); // JavaScript kodunun maksimum kaç satır olabileceğini belirlemek için
                cfg.CatchClrExceptions(); // JavaScript kodunda hata oluştuğunda .NET tarafında da hata oluşmasını sağlamak için
            });

            #region .NET'te tanımlanan action'ları JavaScript içinde kullanmak için tanıtıyoruz         
            engine.SetValue("console", new
            {
                log = new Action<object>(Console.WriteLine),
                error = new Action<object>(msg => Console.Error.WriteLine($"Error: {msg}")),
                warn = new Action<object>(msg => Console.WriteLine($"Warning: {msg}"))
            });

            engine.SetValue("showMessage", new Func<string, string, NotificationIcon, NotificationPosition, NotificationButtonType, int, bool, bool?>(
                (title, message, iconType, position, buttonType, duration, isDialog) =>
                {
                    return _customNotificationService.ShowNotification(new NotificationDTO
                    {
                        Name = "JavaScript Notification",
                        Title = title,
                        Message = message,
                        Icon = iconType,
                        Position = position,
                        ButtonType = buttonType,
                        DisplayDurationInSecond = duration,
                        IsDialog = isDialog
                    });
                }));

            engine.SetValue("runAction", new Func<string, string, Task<object>>(RunAction)); // Belirli bir action'ı tetiklemek için
            engine.SetValue("runAction", new Func<string, Task<object>>(RunAction)); // Belirli bir action'ı tetiklemek için

            engine.SetValue("executeSql", new Func<string, List<Dictionary<string, object>>>(ExecuteSqlCommand)); // SQL komutlarını çalıştırmak için

            engine.SetValue("httpGet", new Func<string, string>((url) =>
            {
                // Senkron işlemi Task.Run ile ayrı bir iş parçacığında çalıştırıyoruz
                return Task.Run(() =>
                {
                    var result = _httpService.SendAsync<string, string>(HttpMethod.Get, url).Result;
                    return result;
                }).Result;
            }));

            engine.SetValue("httpPost", new Func<string, string, string>((url, body) =>
            {
                return Task.Run(() =>
                {
                    var result = _httpService.SendAsync<string, string>(HttpMethod.Post, url, body).Result;
                    return result;
                }).Result;
            }));

            engine.SetValue("httpPut", new Func<string, string, string>((url, body) =>
            {
                return Task.Run(() =>
                {
                    var result = _httpService.SendAsync<string, string>(HttpMethod.Put, url, body).Result;
                    return result;
                }).Result;
            }));

            engine.SetValue("httpDelete", new Func<string, string>((url) =>
            {
                return Task.Run(() =>
                {
                    var result = _httpService.SendAsync<string, string>(HttpMethod.Delete, url).Result;
                    return result;
                }).Result;
            }));
            #endregion

            try
            {
                // JavaScript kodunu değerlendir ve sonucu al sonra sonucu .NET nesnesine dönüştür
                var result = engine.Evaluate(script).ToObject();
                return result ?? new object();
            }
            catch (JavaScriptException ex)
            {
                _customNotificationService.ShowNotification(new NotificationDTO
                {
                    Name = "JavaScript Error Notification",
                    Title = "JavaScript Error",
                    Message = ex.Message,
                    Icon = NotificationIcon.Error,
                    Position = NotificationPosition.Center,
                    ButtonType = NotificationButtonType.OK,
                    DisplayDurationInSecond = 0,
                    IsDialog = false,
                });

                return new object();
            }
        }

        // JavaScript'ten tetiklenecek olan action fonksiyonu
        private async Task<object> RunAction(string actionType, string actionParams)
        {
            try
            {
                if (Enum.TryParse(actionType, out ActionType actionEnum))
                {
                    // ActionFactory ile action'ı al ve execute et
                    var action = _actionFactory.GetAction(actionEnum);
                    if (action != null)
                    {
                        var result = await action?.Execute(actionParams);
                        return result;
                    }
                }
            }
            catch (JavaScriptException ex)
            {
                _customNotificationService.ShowNotification(new NotificationDTO
                {
                    Name = "JavaScript Error Notification",
                    Title = "JavaScript Error",
                    Message = ex.Message,
                    Icon = NotificationIcon.Error,
                    Position = NotificationPosition.Center,
                    ButtonType = NotificationButtonType.OK,
                    DisplayDurationInSecond = 0,
                    IsDialog = false,
                });
            }

            return new object();
        }

        // JavaScript'ten tetiklenecek olan action fonksiyonu
        private async Task<object> RunAction(string actionName)
        {
            try
            {
                var appAction = await _appActionService.GetAppActionAsync(x => x.Name == actionName);
                if (appAction != null)
                {
                    var action = _actionFactory.GetAction(appAction.ActionTypeId);
                    if (action != null)
                    {
                        var result = await action?.Execute(appAction.Properties);
                        return result;
                    }
                }
            }
            catch (JavaScriptException ex)
            {
                _customNotificationService.ShowNotification(new NotificationDTO
                {
                    Name = "JavaScript Error Notification",
                    Title = "JavaScript Error",
                    Message = ex.Message,
                    Icon = NotificationIcon.Error,
                    Position = NotificationPosition.Center,
                    ButtonType = NotificationButtonType.OK,
                    DisplayDurationInSecond = 0,
                    IsDialog = false,
                });
            }

            return new object();
        }

        // SQL sorgularını çalıştıran method ve veri döndüren yeni method
        private List<Dictionary<string, object>> ExecuteSqlCommand(string sqlQuery)
        {
            try
            {
                var result = new List<Dictionary<string, object>>();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    _context.Database.OpenConnection();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }

                            result.Add(row);
                        }
                    }

                    _context.Database.CloseConnection();
                }

                return result;
            }
            catch (JavaScriptException ex)
            {
                _customNotificationService.ShowNotification(new NotificationDTO
                {
                    Name = "JavaScript Error Notification",
                    Title = "JavaScript Error",
                    Message = ex.Message,
                    Icon = NotificationIcon.Error,
                    Position = NotificationPosition.Center,
                    ButtonType = NotificationButtonType.OK,
                    DisplayDurationInSecond = 0,
                    IsDialog = false,
                });

                return default;
            }
        }
    }
}
