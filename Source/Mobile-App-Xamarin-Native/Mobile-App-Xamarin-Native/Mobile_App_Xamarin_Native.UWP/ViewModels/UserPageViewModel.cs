using Mobile_App_Xamarin_Native.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.UWP.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        private ApplicationUser user;

        public ApplicationUser User
        {
            get { return user; }
            set
            {
                if (value != user)
                {
                    user = value;
                    RaisePropertyChange(nameof(User));
                }
            }
        }
    }
}
