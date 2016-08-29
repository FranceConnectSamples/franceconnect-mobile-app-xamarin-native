using System;
using Mobile_App_Xamarin_Native.Models;
using UIKit;

namespace Mobile_App_Xamarin_Native.iOS
{
	public partial class HomeViewController : UITabBarController
	{
		public HomeViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			NavigationItem.HidesBackButton = true;
			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Stop, (sender, args) =>
			{
				var alert = UIAlertController.Create("Se déconnecter", "Voulez vous vraiment vous déconnecter ?", UIAlertControllerStyle.Alert);
				alert.AddAction(UIAlertAction.Create("Non", UIAlertActionStyle.Default, null));
				alert.AddAction(UIAlertAction.Create("Oui", UIAlertActionStyle.Destructive, p =>
				{
					ApplicationUser.SignOut();
					NavigationController.PopToRootViewController(true);
				}));
				PresentViewController(alert, animated: true, completionHandler: null);
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
