using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.FranceConnect.DataProviders
{
    public class DgfipResource : BaseResource
    {
        public decimal Rfr { get; set; }
        public char SitFam { get; set; }
        public int NbPart { get; set; }
        public Pac Pac { get; set; }
    }

    public class Pac
    {
        public int NbPac { get; set; }
    }
}
