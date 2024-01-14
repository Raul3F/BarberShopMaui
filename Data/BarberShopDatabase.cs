using BarberShopMaui.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShopMaui.Data
{
    public class BarberShopDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public BarberShopDatabase(string dbPath) 
        { 
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<BarberShop>().Wait();
            _database.CreateTableAsync<Booking>().Wait();
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<BarberShop>> GetBarberShopsAsync()
        {
            return _database.Table<BarberShop>().ToListAsync();
        }

        public Task<BarberShop> GetBarberShopAsync(int id)
        {
            return _database.Table<BarberShop>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveBarberShopAsync(BarberShop bshop) 
        {
            if (bshop.ID != 0) 
            {
                return _database.UpdateAsync(bshop);
            }
            else
            {
                return _database.InsertAsync(bshop);
            }
        }

        public Task<int> DeleteBarberShopAsync(BarberShop bshop)
        {
            return _database.DeleteAsync(bshop);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserAsync(int id)
        {
            return _database.Table<User>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<User> GetTypeUserAsync(string Username, string Password)
        {
            return _database.Table<User>()
                            .Where(u => u.Username == Username && u.Password == Password)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.ID != 0)
            {   
                var update = _database.UpdateAsync(user);
                Console.WriteLine("Booking? ", user.Bookings?.Count);
                return update;
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }


        public Task<User> AuthenticateUserAsync(string email, string password)
        {
            return _database.Table<User>()
                            .Where(u => u.Email == email && u.Password == password)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveBookingAsync(Booking booking)
        {
            Console.WriteLine("Merge oare din Database?");
            Console.WriteLine(booking.ID);
            Console.WriteLine(booking.User);
            Console.WriteLine(booking.BarberShop);
            if (booking.ID != 0)
            {
                return _database.UpdateAsync(booking);
            }
            else
            {
                return _database.InsertAsync(booking);
            }
        }

        public Task<List<Booking>> GetBookingsAsync()
        {
            return _database.Table<Booking>()
                            .ToListAsync();
        }

        public Task<Booking> GetBookingAsync(int id)
        {
            return _database.Table<Booking>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> DeleteBookingAsync(Booking booking)
        {
            return _database.DeleteAsync(booking);
        }

        public Task<User> GetUserIdAsync(string email)
        {
            return _database.Table<User>()
                            .Where(i => i.Email == email)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Booking>> GetUserBookingsAsync(int userId)
        {
            // Assuming you have a User table with a foreign key relationship to the Booking table
            return await _database.Table<Booking>().Where(b => b.UserId == userId).ToListAsync();
        }


    }
}
