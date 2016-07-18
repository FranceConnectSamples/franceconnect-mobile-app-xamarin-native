using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.FranceConnect.DataProviders
{
    public enum Provider { DGFIP, Custom }

    public static class ProviderExtensions
    {
        public static List<string> GetRequiredScope(this Provider provider)
        {
            switch (provider)
            {
                case Provider.DGFIP: return new List<string> { "dgfip_rfr", "dgfip_sitfam", "dgfip_nbpac", "dgfip_nbpart" };
                case Provider.Custom: return new List<string> { "value1", "value2" };
                default: throw new NotImplementedException();
            }
        }

        public static Uri GetEndpoint(this Provider provider)
        {
            switch (provider)
            {
                case Provider.DGFIP: return new Uri("https://fdp.integ01.dev-franceconnect.fr/courtier/apiuniverselle/2014");
                case Provider.Custom: return new Uri("http://franceconnect-data-provider-dotnet-webapi-aspnetcore.azurewebsites.net/api/values");
                default: throw new NotImplementedException();
            }
        }

        public static BaseResource ConvertResource(this Provider provider, string json)
        {
            switch (provider)
            {
                case Provider.DGFIP: return JsonConvert.DeserializeObject<DgfipResource>(json);
                case Provider.Custom: return JsonConvert.DeserializeObject<CustomResource>(json);
                default: throw new NotImplementedException();
            }
        }

        public static string GetRedirectUri(this Provider provider)
        {
            return FranceConnectClient.BaseConsentRedirectUri + provider.ToString();
        }
    }
}
