using System;
using System.Threading.Tasks;
using Foundation;
using Mobile_App_Xamarin_Native.FranceConnect;
using Mobile_App_Xamarin_Native.Models;
using UIKit;

namespace Mobile_App_Xamarin_Native.iOS
{
	public partial class WebViewController : UIViewController
	{
		private FranceConnectClient _client;
		const string AuthorizeToHomeSegue = "AuthorizeToHome";

		public WebViewController(IntPtr handle) : base(handle)
		{
			_client = new FranceConnectClient();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var uri = new NSUrl(_client.CreateSignInUri());
			CookieHelper.ClearCookies();
			WebView.LoadRequest(new NSUrlRequest(uri));
			WebView.ShouldStartLoad += (webView, request, navigationType) =>
			{
				if (request.Url.AbsoluteString.StartsWith(FranceConnectClient.RedirectUri, StringComparison.OrdinalIgnoreCase))
				{
					var task = Task.Factory.StartNew(async () =>
					{
						await _client.AuthenticateAsync(request.Url.AbsoluteString, FranceConnectClient.RedirectUri);
						ApplicationUser.AuthenticateUser(await _client.GetUserInfoAsync());
					});
					task.Unwrap().ContinueWith((antecedent) =>
					{
						InvokeOnMainThread(() =>
						{
							PerformSegue(AuthorizeToHomeSegue, this);
						});
					},
					TaskContinuationOptions.OnlyOnRanToCompletion);
					task.Unwrap().ContinueWith((antecedent) =>
					{
						InvokeOnMainThread(() =>
						{
							var alert = UIAlertController.Create("Erreur", "Une erreur est survenue", UIAlertControllerStyle.Alert);
							alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
							PresentViewController(alert, animated: true, completionHandler: null);
						});
					},
					TaskContinuationOptions.OnlyOnFaulted);
					return false;
				}
				return true;
			};
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
