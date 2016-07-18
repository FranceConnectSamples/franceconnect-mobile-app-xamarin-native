using Mobile_App_Xamarin_Native.FranceConnect.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.UWP.ViewModels
{
    public class CustomResourcePageViewModel : BaseViewModel
    {
        private CustomResource customResource;
        public CustomResource CustomResource
        {
            get { return customResource; }
            set
            {
                if (value != customResource)
                {
                    customResource = value;
                    RaisePropertyChange(nameof(CustomResource));
                }
            }
        }
    }
}
