using System;

namespace Mobile_App_Xamarin_Native.FranceConnect
{
    public class FranceConnectClientException : Exception
    {
        public FranceConnectClientException()
        {
        }

        public FranceConnectClientException(string message) : base (message)
        {
        }
    }
}
