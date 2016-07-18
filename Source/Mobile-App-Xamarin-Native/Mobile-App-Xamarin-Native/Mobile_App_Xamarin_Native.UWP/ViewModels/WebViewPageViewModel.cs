using Mobile_App_Xamarin_Native.UWP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.UWP.ViewModels
{
    public class WebViewPageViewModel : BaseViewModel
    {
        private WebViewPage.NavigationParameters.Job _pageJob;

        public WebViewPageViewModel(WebViewPage.NavigationParameters.Job pageJob)
        {
            _pageJob = pageJob;
        }

        public string Title
        {
            get
            {
                switch (_pageJob)
                {
                    case WebViewPage.NavigationParameters.Job.SignIn: return "Connexion";
                    case WebViewPage.NavigationParameters.Job.Consent: return "Consentement";
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
