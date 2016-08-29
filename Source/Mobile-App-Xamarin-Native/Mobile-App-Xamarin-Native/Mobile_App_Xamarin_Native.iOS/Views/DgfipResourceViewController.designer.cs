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
    [Register ("DgfipResourceViewController")]
    partial class DgfipResourceViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NbPacLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel NbPartLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RfrLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SitFamLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NbPacLabel != null) {
                NbPacLabel.Dispose ();
                NbPacLabel = null;
            }

            if (NbPartLabel != null) {
                NbPartLabel.Dispose ();
                NbPartLabel = null;
            }

            if (RfrLabel != null) {
                RfrLabel.Dispose ();
                RfrLabel = null;
            }

            if (SitFamLabel != null) {
                SitFamLabel.Dispose ();
                SitFamLabel = null;
            }
        }
    }
}