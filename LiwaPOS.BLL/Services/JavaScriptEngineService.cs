using Jint;
using LiwaPOS.BLL.Factories;
using LiwaPOS.DAL.Context;
using LiwaPOS.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace LiwaPOS.BLL.Services
{
    public class JavaScriptEngineService
    {
        private readonly DataContext _context;
        private readonly ActionFactory _actionFactory;

        public JavaScriptEngineService(DataContext context, ActionFactory actionFactory)
        {
            _context = context;
            _actionFactory = actionFactory;
        }

        // JavaScript kodunu çalıştırmak için kullanılan method
        public void ExecuteJavaScript(string script)
        {
            // Jint JavaScript motorunu oluştur
            var engine = new Engine(cfg => cfg.AllowClr());

            // .NET'te tanımlanan action'ları JavaScript içinde kullanmak için tanıtıyoruz
            engine.SetValue("runAction", new Action<string, string>(RunAction)); // Belirli bir action'ı tetiklemek için
            engine.SetValue("executeSql", new Action<string>(ExecuteSqlCommand)); // SQL komutlarını çalıştırmak için

            // Verilen JavaScript kodunu çalıştır
            engine.Execute(script);
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

        // SQL sorgularını çalıştıran method
        private void ExecuteSqlCommand(string sqlQuery)
        {
            // SQL sorgusunu veritabanında çalıştır
            _context.Database.ExecuteSqlRaw(sqlQuery);
        }
    }
}
