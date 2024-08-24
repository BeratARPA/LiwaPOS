using LiwaPOS.BLL.Actions;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Entities.Enums;

namespace LiwaPOS.BLL.Factories
{
    public class ActionFactory
    {
        private readonly Dictionary<ActionType, Type> _actions = new Dictionary<ActionType, Type>
        {
            { ActionType.ShowPopup, typeof(ShowPopupAction) },
            { ActionType.LoginUser, typeof(LoginUserAction) },
            { ActionType.POSPageOpen, typeof(OpenPOSPageAction) },
            { ActionType.CloseTheApplication, typeof(CloseTheApplicationAction) },
            { ActionType.SendEmail, typeof(SendEmailAction) },
            { ActionType.TelsamSendSMS, typeof(TelsamSendSmsAction) }
        };

        private readonly IServiceProvider _serviceProvider;

        public ActionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAction GetAction(ActionType actionType)
        {
            if (_actions.TryGetValue(actionType, out var actionTypeInstance))
            {
                return _serviceProvider.GetService(actionTypeInstance) as IAction;
            }

            throw new NotImplementedException($"Action type {actionType} is not implemented.");
        }
    }
}
