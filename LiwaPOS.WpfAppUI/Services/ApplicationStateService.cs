using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Models.Entities;
using LiwaPOS.WpfAppUI.Converters;
using LiwaPOS.WpfAppUI.Helpers;
using System.Windows;

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

        private AppScreenType _activeAppScreen;
        public AppScreenType ActiveAppScreen
        {
            get { return _activeAppScreen; }
            set { _activeAppScreen = value; }
        }

        public void SetTextBlockUsername()
        {                   
            GlobalVariables.Shell.TextBlockUsername.Text = CurrentLoggedInUser == null ? "-" : CurrentLoggedInUser.Name ?? "-"; ;
        }

        public void SetGridBottomBarVisibility(VisibilityState visibilityState)
        {
            Visibility visibility = VisibilityStateToVisibilityConverter.Convert(visibilityState);
            GlobalVariables.Shell.GridBottomBar.Visibility = visibility;
        }
    }
}
