using Mobile_App_Xamarin_Native.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Mobile_App_Xamarin_Native.UWP.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public Visibility IsAuthenticated
        {
            get { return ApplicationUser.IsAuthenticated ? Visibility.Visible : Visibility.Collapsed; }
        }

        private bool isPaneOpen;
        public bool IsPaneOpen
        {
            get { return isPaneOpen; }
            set
            {
                if (value != isPaneOpen)
                {
                    isPaneOpen = value;
                    RaisePropertyChange(nameof(IsHamburgerChecked));
                    RaisePropertyChange(nameof(IsPaneOpen));
                }
            }
        }
        public bool? IsHamburgerChecked
        {
            get { return isPaneOpen; }
            set
            {
                if (value.HasValue && value != isPaneOpen)
                {
                    isPaneOpen = value.Value;
                    RaisePropertyChange(nameof(IsHamburgerChecked));
                    RaisePropertyChange(nameof(IsPaneOpen));
                }
            }
        }

        public string LoginLogoutLabel
        {
            get { return ApplicationUser.IsAuthenticated ? "Se déconnecter" : "Connexion"; }
        }

        public void Refresh()
        {
            RaisePropertyChange(nameof(IsAuthenticated));
            RaisePropertyChange(nameof(LoginLogoutLabel));
        }
    }
}
