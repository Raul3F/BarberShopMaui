using BarberShopMaui.Models;
using Plugin.LocalNotification;
namespace BarberShopMaui;

public partial class BookingEntryPage : ContentPage
{
    private bool IsUserAuthenticated { get; set; }
    public string UserId = "ID";
    public BookingEntryPage()
	{
		InitializeComponent();
        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var id = Preferences.Get(UserId, defaultValue: 0);
        collectionView.ItemsSource = await App.Database.GetUserBookingsAsync(id);   
    }

    async void OnBookingAddedClicked(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        var stat = Preferences.Get("isAuthenticated", false);
        if (stat)
        {
            await Navigation.PushAsync(new BookingPage()
            {
                BindingContext = new Booking()
            });
        }
        else
        {
            DisplayAlert("Error", "User not authenticated", "OK");
        }
    }

    async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection[0];
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {

            if (selected != null)
            {
                await Navigation.PushAsync(new BookingPage
                {
                    BindingContext = selected as Booking
                });
            }

        }
    }

    async void OnSwipeItemClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is Booking selectedBooking)
        {
            // Your logic to navigate to the shop or perform other actions with the selected shop
            await Navigation.PushAsync(new BookingPage
            {
                BindingContext = selectedBooking as Booking
            });
        }
    }

    async void DisplayNotification(object sender, EventArgs e)
    {
        var t = "Test";
        var m = "Testare";
        var notification = new NotificationRequest
        {
            NotificationId = 1000,
            Title = t,
            Description = m,
            Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(3), NotifyRepeatInterval = TimeSpan.FromDays(1), }
        };
        await LocalNotificationCenter.Current.Show(notification);

    }
}