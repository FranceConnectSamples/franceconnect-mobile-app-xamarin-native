using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.FranceConnect
{
    public class PivotIdentity
    {
        public string Sub { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public string Email { get; set; }
    }
}
