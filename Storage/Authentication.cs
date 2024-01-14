using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin;

namespace BarberShopMaui.Storage
{
    public partial class Authentication : ContentPage
    {
        public string UserEmail = "mail";
        public string UserPassword = "parola";
        public string IsAuthenticatedStatus = "isAuthenticated";
        public void SaveCredentials(string userEmail, string userPassword, bool isAuthenticated)
        {
            Preferences.Set(UserEmail, userEmail);
            Preferences.Set(UserPassword, userPassword);
            Preferences.Set(IsAuthenticatedStatus, isAuthenticated);
            Console.WriteLine("The credentials has been saved!!!");
            if (isAuthenticated)
            {
                Console.WriteLine("The credentials have been saved!!!");
            }
            else
            {
                Console.WriteLine("Authentication failed. Credentials not saved.");
            }
        }

        public async Task<List<object>> GetCredentials()
        {
            var email = Preferences.Get(UserEmail, null);
            var pass = Preferences.Get(UserPassword, null);
            var auth = Preferences.Get(IsAuthenticatedStatus, null);

            return new List<object> { email, pass, auth };
        }
    }
}
