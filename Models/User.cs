using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BarberShopMaui.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(80), Unique]
        public string Username{ get; set; }

        public string Phone { get; set; }

        [MaxLength(80), Unique]

        public string Email { get; set; }
        public string Password { get; set; }
        public string Type {  get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Booking> Bookings { get; set; }
    }

    public enum Type { User, Admin}
    
}
