using System;
using Mobile_App_Xamarin_Native.Models;
using UIKit;

namespace Mobile_App_Xamarin_Native.iOS
{
	public partial class PivotIdentityViewController : UIViewController
	{
		public PivotIdentityViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var user = ApplicationUser.GetAuthenticatedUser();
			IdLabel.Text = user.Id;
			GenderLabel.Text = user.Gender;
			EmailLabel.Text = user.Email;
			BirthdateLabel.Text = string.Format("{0:D}", user.Birthdate);
			FirstnameLabel.Text = user.Firstname;
			LastnameLabel.Text = user.Lastname;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


