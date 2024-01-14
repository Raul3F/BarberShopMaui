using BarberShopMaui.Models;

namespace BarberShopMaui;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var bslist = (BarberShop)BindingContext;
        Console.WriteLine(bslist.ID);
        Console.WriteLine(bslist.Name);
        Console.WriteLine(bslist.Address);
        Console.WriteLine(bslist.Start);
        Console.WriteLine(bslist.End);
        await App.Database.SaveBarberShopAsync(bslist);
		await Navigation.PopAsync();
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
        var bslist = (BarberShop)BindingContext;
        Console.WriteLine(bslist.ID);
        Console.WriteLine(bslist.Name);
        Console.WriteLine(bslist.Address);
        Console.WriteLine(bslist.Start);
        Console.WriteLine(bslist.End);
        await App.Database.DeleteBarberShopAsync(bslist);
		await Navigation.PopAsync();
    } 
}