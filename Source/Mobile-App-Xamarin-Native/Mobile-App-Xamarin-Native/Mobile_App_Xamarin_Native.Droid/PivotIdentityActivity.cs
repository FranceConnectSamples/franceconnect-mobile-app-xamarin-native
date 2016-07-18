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
using Mobile_App_Xamarin_Native.FranceConnect;

namespace Mobile_App_Xamarin_Native.Droid
{
    [Activity(Label = "Identité pivot", Theme = "@android:style/Theme.Holo.Light", Icon = "@drawable/ic_fc_logo")]
    public class PivotIdentityActivity : Activity
    {
        public const string IdExtra = "Sub";
        public const string EmailExtra = "Email";
        public const string GenderExtra = "Gender";
        public const string BirthdateExtra = "Birthdate";
        public const string FirstnameExtra = "Firstname";
        public const string LastnameExtra = "Lastname";

        private TextView mIdTextView;
        private TextView mEmailTextView;
        private TextView mGenderTextView;
        private TextView mBirthdateTextView;
        private TextView mFirstnameTextView;
        private TextView mLastnameTextView;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pivot_identity);

            mIdTextView = FindViewById<TextView>(Resource.Id.idTextView);
            mEmailTextView = FindViewById<TextView>(Resource.Id.emailTextView);
            mGenderTextView = FindViewById<TextView>(Resource.Id.genderTextView);
            mBirthdateTextView = FindViewById<TextView>(Resource.Id.birthdateTextView);
            mFirstnameTextView = FindViewById<TextView>(Resource.Id.firstnameTextView);
            mLastnameTextView = FindViewById<TextView>(Resource.Id.lastnameTextView);

            mIdTextView.Text = Intent.GetStringExtra(IdExtra);
            mEmailTextView.Text = Intent.GetStringExtra(EmailExtra);
            mGenderTextView.Text = Intent.GetStringExtra(GenderExtra);
            mBirthdateTextView.Text = Intent.GetStringExtra(BirthdateExtra);
            mFirstnameTextView.Text = Intent.GetStringExtra(FirstnameExtra);
            mLastnameTextView.Text = Intent.GetStringExtra(LastnameExtra);
        }
    }
}