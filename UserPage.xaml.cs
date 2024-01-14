using BarberShopMaui.Models;
namespace BarberShopMaui;

public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var users = (User)BindingContext;
        Console.WriteLine(users.ID);
        Console.WriteLine(users.Username);
        Console.WriteLine(users.Email);
        Console.WriteLine(users.Phone);
        Console.WriteLine(users.Password);
        await App.Database.SaveUserAsync(users);
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var users = (User)BindingContext;
        Console.WriteLine(users.ID);
        Console.WriteLine(users.Username);
        Console.WriteLine(users.Email);
        Console.WriteLine(users.Phone);
        Console.WriteLine(users.Password);
        await App.Database.DeleteUserAsync(users);
        await Navigation.PopAsync();
    }
}