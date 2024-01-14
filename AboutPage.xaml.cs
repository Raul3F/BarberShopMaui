using System.Diagnostics;

namespace BarberShopMaui;

public partial class AboutPage : ContentPage
{
    public string UserEmail = "mail";
    public string UserPassword = "parola";
    public string UserAuth = "isAuthenticated";
    public string UserId = "ID";
    public AboutPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }

    private async void OnShowCredentialsClicked(object sender, EventArgs e)
    {
        var mail = Preferences.Get(UserEmail, "DefaultValue");
        var pass = Preferences.Get(UserPassword, "DefaultValue");
        var stat = Preferences.Get(UserAuth, false);
        var id = Preferences.Get(UserId, defaultValue: 0);
        Console.WriteLine("ID: ", id);

        // Display retrieved credentials
        statusLabel.Text = $"Email: {mail}, Password: {pass}, Status: {stat}, ID: {id}";
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        // Remove data from Preferences
        Preferences.Remove(UserEmail);
        Preferences.Remove(UserPassword);
        Preferences.Remove(UserAuth);
        Preferences.Remove(UserId);
    }
}