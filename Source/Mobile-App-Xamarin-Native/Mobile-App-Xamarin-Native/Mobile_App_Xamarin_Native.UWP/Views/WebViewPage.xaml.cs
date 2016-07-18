using Mobile_App_Xamarin_Native.FranceConnect;
using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using Mobile_App_Xamarin_Native.Models;
using Mobile_App_Xamarin_Native.UWP.Tools;
using Mobile_App_Xamarin_Native.UWP.ViewModels;
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
using static Mobile_App_Xamarin_Native.UWP.ViewModels.WebViewPageViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mobile_App_Xamarin_Native.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebViewPage : Page
    {
        private FranceConnectClient _client;
        public WebViewPageViewModel PageViewModel { get; set; }

        public WebViewPage()
        {
            this.InitializeComponent();
            _client = new FranceConnectClient();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var navigationParams = (NavigationParameters)e.Parameter;
            PageViewModel = new WebViewPageViewModel(navigationParams.PageJob);

            Uri uri;
            switch (navigationParams.PageJob)
            {
                case NavigationParameters.Job.SignIn:
                    uri = new Uri(_client.CreateSignInUri());
                    OidcWebView.ClearCookiesForUrl(uri);
                    break;
                case NavigationParameters.Job.Consent:
                    uri = new Uri(_client.CreateConsentUri(navigationParams.ResourceProvider));
                    break;
                default:
                    throw new NotImplementedException();
            }
            OidcWebView.Navigate(uri);

            base.OnNavigatedTo(e);
        }

        private async void OidcWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri.AbsoluteUri.StartsWith(FranceConnectClient.RedirectUri, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    await _client.AuthenticateAsync(args.Uri.AbsoluteUri, FranceConnectClient.RedirectUri);
                    ApplicationUser.AuthenticateUser(await _client.GetUserInfoAsync());
                }
                catch (FranceConnectClientException fce)
                {
                    var errorDialog = new MessageDialog(fce.Message, "Erreur");
                    await errorDialog.ShowAsync();
                }
                catch (Exception)
                {
                    var errorDialog = new MessageDialog("Une erreur est survenue", "Erreur");
                    await errorDialog.ShowAsync();
                }
                finally
                {
                    Frame.GoBack();
                }
            }
            else if (args.Uri.AbsoluteUri.StartsWith(FranceConnectClient.BaseConsentRedirectUri, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var provider = FranceConnectClient.GetProviderFromRedirectUri(args.Uri.AbsoluteUri);
                    await _client.AuthenticateAsync(args.Uri.AbsoluteUri, provider.GetRedirectUri());
                    var resource = await _client.GetDataAsync(provider);
                    switch (provider)
                    {
                        case Provider.DGFIP:
                            Frame.Navigate(typeof(DgfipResourcePage), resource);
                            break;
                        case Provider.Custom:
                            Frame.Navigate(typeof(CustomResourcePage), resource);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                catch (FranceConnectClientException fce)
                {
                    var errorDialog = new MessageDialog(fce.Message, "Erreur");
                    await errorDialog.ShowAsync();
                    Frame.GoBack();
                }
                catch (Exception)
                {
                    var errorDialog = new MessageDialog("Une erreur est survenue", "Erreur");
                    await errorDialog.ShowAsync();
                    Frame.GoBack();
                }
            }
        }

        public class NavigationParameters
        {
            public enum Job { SignIn, Consent }

            public Job PageJob { get; set; }
            public Provider ResourceProvider { get; set; }
        }
    }
}
