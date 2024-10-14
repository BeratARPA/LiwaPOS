using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Interfaces;

namespace LiwaPOS.WpfAppUI.Services
{
    public class ApplicationStateService : IApplicationStateService
    {
		private UserDTO _currentLoggedInUser;
		public UserDTO CurrentLoggedInUser
        {
			get { return _currentLoggedInUser; }
			set { _currentLoggedInUser = value; }
		}

		private bool _isLocked;
		public bool IsLocked
        {
			get { return _isLocked; }
			set { _isLocked = value; }
		}
	}
}
