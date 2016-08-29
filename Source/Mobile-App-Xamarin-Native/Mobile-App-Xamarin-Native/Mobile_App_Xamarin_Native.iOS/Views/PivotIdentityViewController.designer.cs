// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Mobile_App_Xamarin_Native.iOS
{
    [Register ("PivotIdentityViewController")]
    partial class PivotIdentityViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BirthdateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel EmailLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FirstnameLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel GenderLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LastnameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BirthdateLabel != null) {
                BirthdateLabel.Dispose ();
                BirthdateLabel = null;
            }

            if (EmailLabel != null) {
                EmailLabel.Dispose ();
                EmailLabel = null;
            }

            if (FirstnameLabel != null) {
                FirstnameLabel.Dispose ();
                FirstnameLabel = null;
            }

            if (GenderLabel != null) {
                GenderLabel.Dispose ();
                GenderLabel = null;
            }

            if (IdLabel != null) {
                IdLabel.Dispose ();
                IdLabel = null;
            }

            if (LastnameLabel != null) {
                LastnameLabel.Dispose ();
                LastnameLabel = null;
            }
        }
    }
}