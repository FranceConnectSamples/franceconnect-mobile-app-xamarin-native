using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http.Filters;

namespace Mobile_App_Xamarin_Native.UWP.Tools
{
    public static class WebViewExtensions
    {
        public static void ClearCookiesForUrl(this WebView webView, Uri uri)
        {
            var filter = new HttpBaseProtocolFilter();
            var cookies = filter.CookieManager.GetCookies(uri);
            foreach (var cookie in cookies)
            {
                filter.CookieManager.DeleteCookie(cookie);
            }
        }
    }
}
