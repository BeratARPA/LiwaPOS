using LiwaPOS.BLL.Interfaces;

namespace LiwaPOS.BLL.Actions
{
    public class ShowPopupAction : IAction
    {
        private readonly INotificationService _notificationService;

        public ShowPopupAction(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Execute(string properties)
        {
            _notificationService.ShowMessage(properties);
        }
    }

}
