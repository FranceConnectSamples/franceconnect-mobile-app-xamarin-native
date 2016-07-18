using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Mobile_App_Xamarin_Native.Droid
{
    [Activity(Label = "DGFIP", Theme = "@android:style/Theme.Holo.Light", Icon = "@drawable/ic_fc_logo")]
    public class DgfipResourceActivity : Activity
    {
        public const string RfrExtra = "rfr";
        public const string SitfamExtra = "sitfam";
        public const string NbpartExtra = "nbpart";
        public const string NbpacExtra = "nbpac";

        private TextView mRfrTextView;
        private TextView mSitfamTextView;
        private TextView mNbpartTextView;
        private TextView mNbpacTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_dgfip_resource);

            mRfrTextView = FindViewById<TextView>(Resource.Id.rfrTextView);
            mSitfamTextView = FindViewById<TextView>(Resource.Id.sitfamTextView);
            mNbpartTextView = FindViewById<TextView>(Resource.Id.nbpartTextView);
            mNbpacTextView = FindViewById<TextView>(Resource.Id.nbpacTextView);

            mRfrTextView.Text = Intent.GetStringExtra(RfrExtra);
            mSitfamTextView.Text = Intent.GetStringExtra(SitfamExtra);
            mNbpartTextView.Text = Intent.GetStringExtra(NbpartExtra);
            mNbpacTextView.Text = Intent.GetStringExtra(NbpacExtra);
        }
    }
}