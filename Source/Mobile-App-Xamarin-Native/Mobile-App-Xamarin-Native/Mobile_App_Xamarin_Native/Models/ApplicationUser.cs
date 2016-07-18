using Mobile_App_Xamarin_Native.FranceConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App_Xamarin_Native.Models
{
    public class ApplicationUser
    {
        private static ApplicationUser authenticatedUser;

        public string Id { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public static ApplicationUser AuthenticateUser(PivotIdentity pivotIdentity)
        {
            authenticatedUser = new ApplicationUser
            {
                Id = pivotIdentity.Sub,
                Email = pivotIdentity.Email,
                Gender = pivotIdentity.Gender,
                Birthdate = pivotIdentity.Birthdate,
                Firstname = pivotIdentity.Given_name,
                Lastname = pivotIdentity.Family_name
            };

            return authenticatedUser;
        }

        public static bool IsAuthenticated
        {
            get { return authenticatedUser != null; }
        }

        public static ApplicationUser GetAuthenticatedUser()
        {
            if (!IsAuthenticated)
            {
                throw new NullReferenceException("No authenticated user");
            }

            return authenticatedUser;
        }

        public static void SignOut()
        {
            authenticatedUser = null;
        }
    }
}
