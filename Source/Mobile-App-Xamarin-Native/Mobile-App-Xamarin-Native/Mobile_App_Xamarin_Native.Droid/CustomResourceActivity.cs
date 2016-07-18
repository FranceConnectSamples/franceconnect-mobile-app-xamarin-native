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
    [Activity(Label = "Custom", Theme = "@android:style/Theme.Holo.Light", Icon = "@drawable/ic_fc_logo")]
    public class CustomResourceActivity : Activity
    {
        public const string ValueOneExtra = "value_one";
        public const string ValueTwoExtra = "value_two";

        private TextView mValueOneTextView;
        private TextView mValueTwoTextView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_custom_resource);

            mValueOneTextView = FindViewById<TextView>(Resource.Id.valueOneTextView);
            mValueTwoTextView = FindViewById<TextView>(Resource.Id.valueTwoTextView);

            mValueOneTextView.Text = Intent.GetStringExtra(ValueOneExtra);
            mValueTwoTextView.Text = Intent.GetStringExtra(ValueTwoExtra);
        }
    }
}