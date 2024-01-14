using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BarberShopMaui.Models
{
    public class BarberShop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Booking>? Bookings { get; set; }
    }
}
