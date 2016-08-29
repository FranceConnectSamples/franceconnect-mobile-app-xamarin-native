using System;
using Mobile_App_Xamarin_Native.Models;
using UIKit;

namespace Mobile_App_Xamarin_Native.iOS
{
	public partial class StartViewController : UIViewController
	{
		public StartViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


