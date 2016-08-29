using System;
using Foundation;

namespace Mobile_App_Xamarin_Native.iOS
{
	public static class CookieHelper
	{
		public static void ClearCookies()
		{
			var storage = NSHttpCookieStorage.SharedStorage;
			foreach (var cookie in storage.Cookies)
			{
				storage.DeleteCookie(cookie);
			}
		}
	}
}

