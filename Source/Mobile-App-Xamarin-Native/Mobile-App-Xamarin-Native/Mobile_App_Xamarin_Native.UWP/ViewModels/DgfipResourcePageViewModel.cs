using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.UWP.ViewModels
{
    public class DgfipResourcePageViewModel : BaseViewModel
    {
        private DgfipResource dgfipResource;
        public DgfipResource DgfipResource
        {
            get { return dgfipResource; }
            set
            {
                if (value != dgfipResource)
                {
                    dgfipResource = value;
                    RaisePropertyChange(nameof(DgfipResource));
                }
            }
        }
    }
}
