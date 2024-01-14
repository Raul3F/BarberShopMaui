using BarberShopMaui.Data;
using System;
using System.IO;
using BarberShopMaui.Models;

namespace BarberShopMaui
{
    public partial class App : Application
    {
        static BarberShopDatabase database;
        public static BarberShopDatabase Database 
        { 
            get 
            { 
                if (database == null) 
                {
                    database = new BarberShopDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db3"));
                } 
                    return database; 
            } 
        }
        public App()
        {
            InitializeComponent();

            var stat = Preferences.Get("isAuthenticated", false);
            if (stat)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
