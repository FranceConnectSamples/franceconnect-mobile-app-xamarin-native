using System;
using System.Threading.Tasks;
using Foundation;
using Mobile_App_Xamarin_Native.FranceConnect;
using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using UIKit;

namespace Mobile_App_Xamarin_Native.iOS
{
	public partial class DgfipResourceViewController : UIViewController
	{
		private FranceConnectClient _client;

		public DgfipResourceViewController(IntPtr handle) : base(handle)
		{
			_client = new FranceConnectClient();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var consentWebView = new UIWebView(View.Bounds);
			View.AddSubview(consentWebView);

			var url = _client.CreateConsentUri(Provider.DGFIP);
			consentWebView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
			consentWebView.ShouldStartLoad += (webView, request, navigationType) =>
			{
				if (request.Url.AbsoluteString.StartsWith(FranceConnectClient.BaseConsentRedirectUri, StringComparison.OrdinalIgnoreCase))
				{
					var task = Task.Factory.StartNew(async () =>
					{
						await _client.AuthenticateAsync(request.Url.AbsoluteString, Provider.DGFIP.GetRedirectUri());
						var resource = await _client.GetDataAsync(Provider.DGFIP) as DgfipResource;
						InvokeOnMainThread(() =>
						{
							RfrLabel.Text = string.Format("{0:C}", resource.Rfr);
							SitFamLabel.Text = resource.SitFam.ToString();
							NbPartLabel.Text = resource.NbPart.ToString();
							NbPacLabel.Text = resource.Pac.NbPac.ToString();
							consentWebView.RemoveFromSuperview();
						});
					});
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

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
