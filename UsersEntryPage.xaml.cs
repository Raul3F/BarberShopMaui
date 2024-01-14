using BarberShopMaui.Models;
namespace BarberShopMaui;

public partial class UsersEntryPage : ContentPage
{
	public UsersEntryPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetUsersAsync();
    }

    async void OnUserAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserPage
        {
            BindingContext = new User()
        });
    }

    async void OnViewSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new UserPage
            {
                BindingContext = e.SelectedItem as User
            });
        }
    }
   
}