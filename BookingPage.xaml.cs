using BarberShopMaui.Models;
using Plugin.LocalNotification;
using System.Diagnostics;
using System.Net;

using Xamarin;
namespace BarberShopMaui;

public partial class BookingPage : ContentPage
{
    private User AuthenticatedUser { get; set; }
    public string UserEmail = "mail";
    public string UserPassword = "parola";
    public string UserAuth = "isAuthenticated";
    public BookingPage()
    {
        InitializeComponent();
        PopulateBarberShopsPicker();
    }

    private async void PopulateBarberShopsPicker()
    {
        try
        {
            var barberShops = await App.Database.GetBarberShopsAsync();
            Console.WriteLine(barberShops.ToString());
            Debug.WriteLine(barberShops);

            // Check if the list is not empty before populating the Picker
            if (barberShops != null && barberShops.Count > 0)
            {
                Console.WriteLine(barberShops.ToString());
                foreach (var shop in barberShops)
                {
                    Console.WriteLine(shop.Name);
                    barberShopPicker.ItemsSource = barberShops;
                    barberShopPicker.SelectedIndex = 0; // Set a default selection if needed
                }

            }
            else
            {
                DisplayAlert("Error", "No barber shops available", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to retrieve barber shops: {ex.Message}", "OK");
        }
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var book = (Booking)BindingContext;
        BarberShop selectedBarberShop = (BarberShop)barberShopPicker.SelectedItem;
        DateTime bookingDateTime = bookingDatePicker.Date + bookingTimePicker.Time;
        string status = "Pending";

        var mail = Preferences.Get(UserEmail, "DefaultValue");
        var pass = Preferences.Get(UserPassword, "DefaultValue");
        var stat = Preferences.Get(UserAuth, false);

        if (stat)
        {
            AuthenticatedUser = await AuthenticateUser(mail, pass);
        }
        else
        {
            DisplayAlert("Error", "No user authenticated, please log out then try again", "OK");
        }

        // Get the selected category from the picker
        string selectedCategory = categoryPicker.SelectedItem?.ToString();

        Console.WriteLine("De ce nu mergi?", selectedCategory);

        User user = await App.Database.GetUserAsync(AuthenticatedUser.ID);


        Booking newBooking = new Booking
        {
            ID = book.ID,
            ShopId = selectedBarberShop.ID,
            BarberShop = selectedBarberShop.Name,
            User = AuthenticatedUser.Username,
            UserId = AuthenticatedUser.ID,
            BookingTime = bookingDateTime,
            Status = status,
            Category = selectedCategory
        };

        if (book != null && book.Equals(newBooking))
        {
            await App.Database.SaveBookingAsync(book);

            Console.WriteLine("Current booking starting");
            Console.WriteLine(book.ID);
            Console.WriteLine(book.Status);
            Console.WriteLine(book.BookingTime);
            Console.WriteLine(book.ShopId);
            Console.WriteLine(book.BarberShop);
            Console.WriteLine(book.UserId);
            Console.WriteLine(book.User);
            Console.WriteLine(book.Category);
            Console.WriteLine("Current booking ended");
        }
        else
        {
            await App.Database.SaveBookingAsync(newBooking);
        }
        
        Console.WriteLine("newBooking: ", newBooking.ID);

        if (user != null)
        {
            // Add the booking to the user's list of bookings
            if (user.Bookings == null)
            {
                user.Bookings = new List<Booking>();
            }
            await App.Database.SaveBookingAsync(newBooking);

            DateTime currentTime = DateTime.Now;
            TimeSpan remainingTime = bookingDateTime - currentTime;
            Console.WriteLine("remaining: ", remainingTime);

            if (remainingTime > TimeSpan.Zero)
            {
                var t = remainingTime.ToString();
                Console.WriteLine("Notification: ", t);
                var m = "Testare";
                var notification = new NotificationRequest
                {
                    NotificationId = 1000,
                    Title = $"Booking placed succesfully at {selectedBarberShop?.Name}",
                    Subtitle = "Time remaining until booking time: ",
                    Description = $"{t} Zile:Ore:Minute:Secunde.Milisecunde",
                    Schedule = new NotificationRequestSchedule { NotifyTime = DateTime.Now.AddSeconds(3), NotifyRepeatInterval = TimeSpan.FromDays(1), }
                };
                await LocalNotificationCenter.Current.Show(notification);

            }

            // Update the user in the database
            await App.Database.SaveUserAsync(user);
        }
        Console.WriteLine("NewBooking below");
        Console.WriteLine(newBooking.ID);
        Console.WriteLine(newBooking.Status);
        Console.WriteLine(newBooking.BookingTime);
        Console.WriteLine(newBooking.ShopId);
        Console.WriteLine(newBooking.BarberShop);
        Console.WriteLine(newBooking.UserId);
        Console.WriteLine(newBooking.User);
        Console.WriteLine(newBooking.Category);
        Console.WriteLine("NewBooking ended");
        

        // Navigate back to the previous page
        await Navigation.PopAsync();
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var bk = (Booking)BindingContext;
        Console.WriteLine(bk.ID);
        await App.Database.DeleteBookingAsync(bk);
        await Navigation.PopAsync();
    }
    private async Task<User> AuthenticateUser(string email, string password)
    {
        return await App.Database.AuthenticateUserAsync(email, password);
    }

}