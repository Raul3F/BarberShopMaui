using BarberShopMaui.Models;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace BarberShopMaui;

public partial class ShopEntryPage : ContentPage
{
	public ShopEntryPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        collectionView.ItemsSource = await App.Database.GetBarberShopsAsync();
    }

    async void OnShopAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopPage
        {
            BindingContext = new BarberShop()
        });
    }

    async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection[0];
        Debug.WriteLine($"Selected BarberShop: {selected}");
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            Debug.WriteLine($"Selected BarberShop: {selected}");

            if (selected != null)
            {
                await Navigation.PushAsync(new ShopPage
                {
                    BindingContext = selected as BarberShop
                });
            }

        }
    }

    async void OnSwipeItemClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is BarberShop selectedShop)
        {
            await Navigation.PushAsync(new ShopPage
            {
                BindingContext = selectedShop as BarberShop
            });
        }
    }
}