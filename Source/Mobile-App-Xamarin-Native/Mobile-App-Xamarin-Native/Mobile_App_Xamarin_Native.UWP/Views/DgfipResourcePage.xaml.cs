using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using Mobile_App_Xamarin_Native.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Mobile_App_Xamarin_Native.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DgfipResourcePage : Page
    {
        public DgfipResourcePageViewModel PageViewModel { get; set; }

        public DgfipResourcePage()
        {
            this.InitializeComponent();
            PageViewModel = new DgfipResourcePageViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PageViewModel.DgfipResource = (DgfipResource)e.Parameter;

            if (Frame.CanGoBack)
            {
                PageStackEntry lastPage = Frame.BackStack[Frame.BackStackDepth - 1];
                if (lastPage.SourcePageType == typeof(WebViewPage))
                {
                    Frame.BackStack.Remove(lastPage);
                }
            }

            base.OnNavigatedTo(e);
        }
    }
}
