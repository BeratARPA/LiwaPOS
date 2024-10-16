using Jint;
using Jint.Runtime;
using LiwaPOS.BLL.Factories;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Context;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LiwaPOS.BLL.Services
{
    public class JavaScriptEngineService
    {
        private readonly DataContext _context;
        private readonly ActionFactory _actionFactory;
        private readonly ICustomNotificationService _customNotificationService;

        public JavaScriptEngineService(DataContext context, ActionFactory actionFactory, ICustomNotificationService customNotificationService)
        {
            _context = context;
            _actionFactory = actionFactory;
            _customNotificationService = customNotificationService;
        }

        // JavaScript kodunu çalıştırmak için kullanılan method
        public object ExecuteJavaScript(string script)
        {
            // Jint JavaScript motorunu oluştur
            var engine = new Engine(cfg => cfg.AllowClr());

            #region .NET'te tanımlanan action'ları JavaScript içinde kullanmak için tanıtıyoruz         
            engine.SetValue("console", new
            {
                log = new Action<object>(Console.WriteLine),
                error = new Action<object>(msg => Console.Error.WriteLine($"Error: {msg}")),
                warn = new Action<object>(msg => Console.WriteLine($"Warning: {msg}"))
            });

            engine.SetValue("showMessage", new Action<string>(msg =>
            _customNotificationService.ShowNotification(new NotificationDTO
            {
                Title = "Notification",
                Message = msg,
                Icon = NotificationIcon.Information,
                Position = NotificationPosition.Center,
                ButtonType = NotificationButtonType.OK,
                DisplayDurationInSecond = 0
            })
            ));

            engine.SetValue("runAction", new Action<string, string>(RunAction)); // Belirli bir action'ı tetiklemek için

            engine.SetValue("executeSql", new Func<string, List<Dictionary<string, object>>>(ExecuteSqlCommand)); // SQL komutlarını çalıştırmak için
        
            //engine.SetValue("setTimeout", new Action<Action<int>, int>((action, delay) =>
            //{
            //    Task.Delay(delay).ContinueWith(_ => Application.Current.Dispatcher.Invoke(() => action(0)));
            //}));

            //// setInterval fonksiyonunu ekleyin (belirli bir aralıkta tekrarlanan işlem)
            //engine.SetValue("setInterval", new Action<Action<int>, int>((action, interval) =>
            //{
            //    Task.Run(async () =>
            //    {
            //        while (true)
            //        {
            //            await Task.Delay(interval);
            //            Application.Current.Dispatcher.Invoke(() => action(0));
            //        }
            //    });
            //}));
            #endregion

            // Verilen JavaScript kodunu çalıştır ve sonucu al
            try
            {
                var result = engine.Evaluate(script); // JavaScript kodunu değerlendir ve sonucu al
                return result.ToObject(); // Sonucu .NET nesnesine dönüştür
            }
            catch (JavaScriptException ex)
            {
                _customNotificationService.ShowNotification(new NotificationDTO
                {
                    Title = "JavaScript Error",
                    Message = ex.Message,
                    Icon = NotificationIcon.Error,
                    Position = NotificationPosition.Center,
                    ButtonType = NotificationButtonType.OK,
                    DisplayDurationInSecond = 0
                });
                return null;
            }
        }

        // JavaScript'ten tetiklenecek olan action fonksiyonu
        private void RunAction(string actionType, string actionParams)
        {
            if (Enum.TryParse(actionType, out ActionType actionEnum))
            {               
                // ActionFactory ile action'ı al ve execute et
                var action = _actionFactory.GetAction(actionEnum);
                action?.Execute(actionParams);
            }
        }

        // SQL sorgularını çalıştıran method ve veri döndüren yeni method
        private List<Dictionary<string, object>> ExecuteSqlCommand(string sqlQuery)
        {
            var result = new List<Dictionary<string, object>>();

            // Veritabanında sorguyu çalıştır ve sonucu al
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
    }
}
