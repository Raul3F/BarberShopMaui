using Microsoft.Maui.Controls;
using BarberShopMaui.Models;
using BarberShopMaui.Storage;

namespace BarberShopMaui;

public partial class LoginPage : ContentPage
{
    private bool IsUserAuthenticated { get; set; }
    private User AuthenticatedUser { get; set; }
    public string UserEmail = "mail";
    public string UserPassword = "parola";
    public string UserAuth = "isAuthenticated";
    public string UserId = "ID";
    public LoginPage()
	{
		InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }
 

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string userEmail = emailEntry.Text;
        string userPassword = passwordEntry.Text;

        AuthenticatedUser = await AuthenticateUser(userEmail, userPassword);

        Console.WriteLine("userId", AuthenticatedUser.ID);

        if (AuthenticatedUser != null)
        {
            IsUserAuthenticated = true;

            var id = AuthenticatedUser.ID.ToString();

            Preferences.Set(UserEmail, userEmail);
            Preferences.Set(UserPassword, userPassword);
            Preferences.Set(UserAuth, true);
            Preferences.Set(UserId, id);

            Console.WriteLine("userEmail", userEmail);
            Console.WriteLine("userPassword", userPassword);
            Console.WriteLine("userAuth", true);
            Console.WriteLine("userId", id);


            Application.Current.MainPage = new AppShell();
            await Navigation.PushAsync(new ShopEntryPage());
        }
        else
        {
            DisplayAlert("Error", "Invalid credentials", "OK");
        }
    }

    private async void OnShowCredentialsClicked(object sender, EventArgs e)
    {
        var mail = Preferences.Get(UserEmail, "DefaultValue");
        var pass = Preferences.Get(UserPassword, "DefaultValue");
        var stat = Preferences.Get(UserAuth, false);
        var id   = Preferences.Get(UserId, defaultValue: 0);
        Console.WriteLine("ID: ", id);

        // Display retrieved credentials
        statusLabel.Text = $"Email: {mail}, Password: {pass}, Status: {stat}, ID: {id}";
    }

    private async Task<User> AuthenticateUser(string email, string password)
    {
        return await App.Database.AuthenticateUserAsync(email, password);
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Remove(UserEmail);
        Preferences.Remove(UserPassword);
        Preferences.Remove(UserAuth);
        Preferences.Remove(UserId);
    }

}