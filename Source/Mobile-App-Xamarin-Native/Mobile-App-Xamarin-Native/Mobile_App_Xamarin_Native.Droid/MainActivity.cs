using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Mobile_App_Xamarin_Native.Models;
using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using Android.Content.Res;

namespace Mobile_App_Xamarin_Native.Droid
{
    [Activity(Label = "FranceConnect Xamarin", Theme = "@android:style/Theme.Holo.Light", MainLauncher = true, Icon = "@drawable/ic_fc_logo")]
    public class MainActivity : Activity
    {
        private DrawerLayout mDrawerLayout;
        private ActionBarDrawerToggle mDrawerToggle;
        private Button mLoginLogoutButton;
        private Button mPivotIdentityButton;
        private Button mDgfipResourceButton;
        private Button mCustomResourceButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mLoginLogoutButton = FindViewById<Button>(Resource.Id.loginLogoutButton);
            mPivotIdentityButton = FindViewById<Button>(Resource.Id.pivotIdentityButton);
            mDgfipResourceButton = FindViewById<Button>(Resource.Id.dgfipResourceButton);
            mCustomResourceButton = FindViewById<Button>(Resource.Id.customResourceButton);

            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.ActionBar.SetHomeButtonEnabled(true);
            
            mDrawerToggle = new ActionBarDrawerToggle(this, mDrawerLayout,
                Resource.Drawable.ic_drawer,
                Resource.String.drawer_open,
                Resource.String.drawer_close);
            mDrawerLayout.SetDrawerListener(mDrawerToggle);

            ShowHideElements();

            mLoginLogoutButton.Click += LoginLogoutButton_Click;
            mPivotIdentityButton.Click += PivotIdentityButton_Click;
            mDgfipResourceButton.Click += DgfipResourceButton_Click;
            mCustomResourceButton.Click += CustomResourceButton_Click;
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (mDrawerToggle.OnOptionsItemSelected(item))
            {
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStart()
        {
            base.OnStart();
            ShowHideElements();
        }

        private void ShowHideElements()
        {
            if (ApplicationUser.IsAuthenticated)
            {
                mPivotIdentityButton.Visibility = ViewStates.Visible;
                mDgfipResourceButton.Visibility = ViewStates.Visible;
                mCustomResourceButton.Visibility = ViewStates.Visible;

                mLoginLogoutButton.Text = Resources.GetString(Resource.String.logout);
            }
            else
            {
                mPivotIdentityButton.Visibility = ViewStates.Gone;
                mDgfipResourceButton.Visibility = ViewStates.Gone;
                mCustomResourceButton.Visibility = ViewStates.Gone;

                mLoginLogoutButton.Text = Resources.GetString(Resource.String.login);
            }
        }

        private void LoginLogoutButton_Click(object sender, EventArgs e)
        {
            mDrawerLayout.CloseDrawers();
            if (ApplicationUser.IsAuthenticated)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Se déconnecter");
                alert.SetMessage("Voulez-vous vraiment vous déconnecter ?");
                alert.SetPositiveButton("Oui", (senderAlert, args) =>
                {
                    ApplicationUser.SignOut();
                });
                alert.SetNegativeButton("Non", (senderAlert, args) => { });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {
                var activity = new Intent(this, typeof(WebViewActivity));
                activity.PutExtra(WebViewActivity.JobExtra, (int)WebViewActivity.Job.SignIn);
                StartActivity(activity);
            }
        }

        private void PivotIdentityButton_Click(object sender, EventArgs e)
        {
            mDrawerLayout.CloseDrawers();
            var user = ApplicationUser.GetAuthenticatedUser();
            if (user != null)
            {
                var activity = new Intent(this, typeof(PivotIdentityActivity));
                activity.PutExtra(PivotIdentityActivity.IdExtra, user.Id);
                activity.PutExtra(PivotIdentityActivity.EmailExtra, user.Email);
                activity.PutExtra(PivotIdentityActivity.GenderExtra, user.Gender);
                activity.PutExtra(PivotIdentityActivity.BirthdateExtra, user.Birthdate.ToString("D"));
                activity.PutExtra(PivotIdentityActivity.FirstnameExtra, user.Firstname);
                activity.PutExtra(PivotIdentityActivity.LastnameExtra, user.Lastname);
                StartActivity(activity);
            }
        }

        private void DgfipResourceButton_Click(object sender, EventArgs e)
        {
            mDrawerLayout.CloseDrawers();
            var activity = new Intent(this, typeof(WebViewActivity));
            activity.PutExtra(WebViewActivity.JobExtra, (int)WebViewActivity.Job.Consent);
            activity.PutExtra(WebViewActivity.ResourceProviderExtra, (int)Provider.DGFIP);
            StartActivity(activity);
        }

        private void CustomResourceButton_Click(object sender, EventArgs e)
        {
            mDrawerLayout.CloseDrawers();
            var activity = new Intent(this, typeof(WebViewActivity));
            activity.PutExtra(WebViewActivity.JobExtra, (int)WebViewActivity.Job.Consent);
            activity.PutExtra(WebViewActivity.ResourceProviderExtra, (int)Provider.Custom);
            StartActivity(activity);
        }
    }
}
