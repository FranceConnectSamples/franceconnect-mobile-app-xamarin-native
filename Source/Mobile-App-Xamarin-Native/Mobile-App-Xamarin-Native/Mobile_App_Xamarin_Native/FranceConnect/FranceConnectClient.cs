using IdentityModel.Client;
using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.FranceConnect
{
    public class FranceConnectClient
    {
        public const string RedirectUri = "http://Mobile-App-Xamarin-Native/signin_oidc";
        public const string BaseConsentRedirectUri = "http://Mobile-App-Xamarin-Native/consent/callback_";
        private const string AuthorizationEndpoint = "https://fcp.integ01.dev-franceconnect.fr/api/v1/authorize";
        private const string TokenEndpoint = "https://fcp.integ01.dev-franceconnect.fr/api/v1/token";
        private const string UserInfoEndpoint = "https://fcp.integ01.dev-franceconnect.fr/api/v1/userinfo";
        private const string ClientId = "dd3d652f1be391d4ae3a055ecd6bce8e5ae7bf1c2fa80539a03c1b180cbc6b47";
        private const string ClientSecret = "e359cd837155fb12d0999ef23337113454532198dd26604a85db8c7b51544b9a";

        private string _state;
        private string _accessToken;

        public string CreateSignInUri()
        {
            _state = Guid.NewGuid().ToString("N");
            var scope = new List<string> { "openid", "profile", "email" };

            return CreateAuthorizeUri(scope, RedirectUri);
        }

        public string CreateConsentUri(Provider provider)
        {
            _state = Guid.NewGuid().ToString("N");
            var scope = new List<string> { "openid", "email" };
            scope.AddRange(provider.GetRequiredScope());

            return CreateAuthorizeUri(scope, provider.GetRedirectUri());
        }

        private string CreateAuthorizeUri(IEnumerable<string> scope, string redirectUri)
        {
            var authorizeRequest = new AuthorizeRequest(AuthorizationEndpoint);
            return authorizeRequest.CreateAuthorizeUrl(
                clientId: ClientId,
                responseType: "code",
                scope: string.Join(" ", scope),
                redirectUri: redirectUri,
                state: _state,
                nonce: Guid.NewGuid().ToString("N"));
        }

        public async Task AuthenticateAsync(string uri, string redirectUri)
        {
            var code = GetCode(uri);
            _accessToken = await GetAccessTokenAsync(code, redirectUri);
        }

        private string GetCode(string uri)
        {
            var authorizeResponse = new AuthorizeResponse(uri);

            var state = authorizeResponse.Values["state"];
            if (state != _state)
            {
                throw new FranceConnectClientException("Invalide state");
            }

            var code = authorizeResponse.Code;
            if (string.IsNullOrEmpty(code))
            {
                throw new FranceConnectClientException("No authorization code");
            }

            return code;
        }

        private async Task<string> GetAccessTokenAsync(string code, string redirectUri)
        {
            var tokenClient = new TokenClient(TokenEndpoint, ClientId, ClientSecret, AuthenticationStyle.PostValues);
            var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(code, redirectUri);
            if (tokenResponse.IsError || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new FranceConnectClientException("Unable to retrive access token");
            }
            return tokenResponse.AccessToken;
        }

        public async Task<PivotIdentity> GetUserInfoAsync()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new FranceConnectClientException("Access token is null");
            }

            var userInfoClient = new UserInfoClient(new Uri(UserInfoEndpoint), _accessToken);
            var userInfoResponse = await userInfoClient.GetAsync();
            if (userInfoResponse.IsError)
            {
                throw new FranceConnectClientException("Unable to retrive user informations");
            }

            return userInfoResponse.JsonObject.ToObject<PivotIdentity>();
        }

        public async Task<BaseResource> GetDataAsync(Provider provider)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new FranceConnectClientException("Access token is null");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                var response = await client.GetAsync(provider.GetEndpoint());
                if (response.IsSuccessStatusCode)
                {
                    var resource = await response.Content.ReadAsStringAsync();
                    return provider.ConvertResource(resource);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new FranceConnectClientException("La ressource demandée n'a pas été trouvée.");
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new FranceConnectClientException("Vous n'êtes pas autorisé à accéder cette ressource.");
                }
                else
                {
                    throw new FranceConnectClientException("Impossible de récupérer les données auprès du fournisseur choisi.");
                }
            }
        }

        public static Provider GetProviderFromRedirectUri(string redirectUri)
        {
            var truncateUri = redirectUri.Substring(BaseConsentRedirectUri.Length);
            if (truncateUri.StartsWith(Provider.DGFIP.ToString()))
            {
                return Provider.DGFIP;
            }
            else if (truncateUri.StartsWith(Provider.Custom.ToString()))
            {
                return Provider.Custom;
            }
            else
            {
                throw new FranceConnectClientException("Unable to retrive provider from the redirect uri");
            }
        }
    }
}
