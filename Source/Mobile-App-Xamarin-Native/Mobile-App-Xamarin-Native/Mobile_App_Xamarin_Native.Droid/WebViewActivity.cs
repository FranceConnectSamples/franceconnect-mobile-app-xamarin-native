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
using Android.Webkit;
using Mobile_App_Xamarin_Native.FranceConnect;
using Mobile_App_Xamarin_Native.Models;
using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.Droid
{
    [Activity(Theme = "@android:style/Theme.Holo.Light", Icon = "@drawable/ic_fc_logo", NoHistory = true)]
    public class WebViewActivity : Activity
    {
        public enum Job { SignIn, Consent }

        public const string JobExtra = "Job";
        public const string ResourceProviderExtra = "ResourceProvider";

        private WebView mOidcWebView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_web_view);

            var job = (Job)Intent.GetIntExtra(JobExtra, (int)Job.SignIn);
            var provider = (Provider)Intent.GetIntExtra(ResourceProviderExtra, (int)Provider.DGFIP);

            switch (job)
            {
                case Job.SignIn:
                    this.Title = Resources.GetString(Resource.String.login);
                    var cookieManager = CookieManager.Instance;
                    cookieManager.RemoveAllCookie();
                    break;
                case Job.Consent:
                    this.Title = Resources.GetString(Resource.String.consent);
                    break;
            }

            OidcWebViewClient client = new OidcWebViewClient(this, job, provider);
            mOidcWebView = FindViewById<WebView>(Resource.Id.oidcWebView);
            mOidcWebView.Settings.JavaScriptEnabled = true;
            mOidcWebView.SetWebViewClient(client);
            mOidcWebView.LoadUrl(client.GetStartUrl());
        }

        private class OidcWebViewClient : WebViewClient
        {
            private FranceConnectClient _client;
            private Job _job;
            private Provider _provider;
            private Activity _activity;

            public OidcWebViewClient(Activity activity, Job job, Provider provider)
            {
                _activity = activity;
                _client = new FranceConnectClient();
                _job = job;
                _provider = provider;
            }

            public string GetStartUrl()
            {
                switch (_job)
                {
                    case Job.SignIn:
                        return _client.CreateSignInUri();
                    case Job.Consent:
                        return _client.CreateConsentUri(_provider);
                    default:
                        throw new NotImplementedException();
                }
            }
            
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                if (url.StartsWith(FranceConnectClient.RedirectUri, StringComparison.OrdinalIgnoreCase))
                {
                    view.Visibility = ViewStates.Gone;
                    var task = Task.Factory.StartNew(async () =>
                    {
                        await _client.AuthenticateAsync(url, FranceConnectClient.RedirectUri);
                        ApplicationUser.AuthenticateUser(await _client.GetUserInfoAsync());
                    });
                    task.Unwrap().ContinueWith((antecedent) =>
                        {
                            _activity.Finish();
                        },
                        TaskContinuationOptions.OnlyOnRanToCompletion);
                    task.Unwrap().ContinueWith((antecedent) =>
                        {
                            _activity.RunOnUiThread(() =>
                            {
                                AlertDialog.Builder alert = new AlertDialog.Builder(_activity);
                                alert.SetTitle("Erreur");
                                alert.SetMessage(antecedent.Exception.Message);
                                alert.SetPositiveButton("Fermer", (senderAlert, args) =>
                                {
                                    _activity.Finish();
                                });

                                Dialog dialog = alert.Create();
                                dialog.Show();
                            });
                        },
                        TaskContinuationOptions.OnlyOnFaulted);
                }
                else if (url.StartsWith(FranceConnectClient.BaseConsentRedirectUri, StringComparison.OrdinalIgnoreCase))
                {
                    view.Visibility = ViewStates.Gone;
                    var task = Task.Factory.StartNew(async () =>
                    {
                        var provider = FranceConnectClient.GetProviderFromRedirectUri(url);
                        await _client.AuthenticateAsync(url, provider.GetRedirectUri());
                        var resource = await _client.GetDataAsync(provider);
                        Intent resourceActivity;
                        switch (provider)
                        {
                            case Provider.DGFIP:
                                resourceActivity = new Intent(_activity, typeof(DgfipResourceActivity));
                                resourceActivity.PutExtra(DgfipResourceActivity.RfrExtra, ((DgfipResource)resource).Rfr.ToString("C"));
                                resourceActivity.PutExtra(DgfipResourceActivity.SitfamExtra, ((DgfipResource)resource).SitFam.ToString());
                                resourceActivity.PutExtra(DgfipResourceActivity.NbpartExtra, ((DgfipResource)resource).NbPart.ToString());
                                resourceActivity.PutExtra(DgfipResourceActivity.NbpacExtra, ((DgfipResource)resource).Pac.NbPac.ToString());
                                _activity.StartActivity(resourceActivity);
                                break;
                            case Provider.Custom:
                                resourceActivity = new Intent(_activity, typeof(CustomResourceActivity));
                                resourceActivity.PutExtra(CustomResourceActivity.ValueOneExtra, ((CustomResource)resource).ValueOne);
                                resourceActivity.PutExtra(CustomResourceActivity.ValueTwoExtra, ((CustomResource)resource).ValueTwo);
                                _activity.StartActivity(resourceActivity);
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    });
                    task.ContinueWith(t =>
                        {
                            _activity.RunOnUiThread(() =>
                            {
                                AlertDialog.Builder alert = new AlertDialog.Builder(_activity);
                                alert.SetTitle("Erreur");
                                alert.SetMessage(t.Exception.Message);
                                alert.SetPositiveButton("Fermer", (senderAlert, args) =>
                                {
                                    _activity.Finish();
                                });

                                Dialog dialog = alert.Create();
                                dialog.Show();
                            });
                        },
                        TaskContinuationOptions.OnlyOnFaulted);
                }

                return base.ShouldOverrideUrlLoading(view, url);
            }
        }
    }
}