using Mobile_App_Xamarin_Native.Models;
using Mobile_App_Xamarin_Native.UWP.ViewModels;
using Mobile_App_Xamarin_Native.UWP.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mobile_App_Xamarin_Native.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel PageViewModel { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
            PageViewModel = new MainPageViewModel();
        }

        private async void LoginLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ApplicationUser.IsAuthenticated)
            {
                PageViewModel.IsPaneOpen = false;
                Frame.Navigate(typeof(WebViewPage),
                    new WebViewPage.NavigationParameters
                    {
                        PageJob = WebViewPage.NavigationParameters.Job.SignIn
                    });
            }
            else
            {
                var dialog = new MessageDialog("Voulez-vous vraiment vous déconnecter ?", "Se déconnecter");
                dialog.Commands.Add(new UICommand("Oui") { Id = 0 });
                dialog.Commands.Add(new UICommand("Non") { Id = 1 });
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;
                var result = await dialog.ShowAsync();
                if ((int)result.Id == 0)
                {
                    ApplicationUser.SignOut();
                    PageViewModel.Refresh();
                }
            }
        }

        private void PivotIdentityButton_Click(object sender, RoutedEventArgs e)
        {
            PageViewModel.IsPaneOpen = false;
            Frame.Navigate(typeof(UserPage));
        }

        private void DgfipResourceButton_Click(object sender, RoutedEventArgs e)
        {
            PageViewModel.IsPaneOpen = false;
            Frame.Navigate(typeof(WebViewPage),
                new WebViewPage.NavigationParameters
                {
                    PageJob = WebViewPage.NavigationParameters.Job.Consent,
                    ResourceProvider = FranceConnect.DataProviders.Provider.DGFIP
                });
        }

        private void CustomResourceButton_Click(object sender, RoutedEventArgs e)
        {
            PageViewModel.IsPaneOpen = false;
            Frame.Navigate(typeof(WebViewPage),
                new WebViewPage.NavigationParameters
                {
                    PageJob = WebViewPage.NavigationParameters.Job.Consent,
                    ResourceProvider = FranceConnect.DataProviders.Provider.Custom
                });
        }
    }
}
