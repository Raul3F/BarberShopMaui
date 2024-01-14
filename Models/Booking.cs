using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BarberShopMaui.Models
{
    public class Booking
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Status { get; set; }
        [Unique]
        public DateTime BookingTime { get; set; }

        [ForeignKey(typeof(BarberShop))]
        public int ShopId { get; set; }

        public string BarberShop { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }

        public string User { get; set; }

        public string Category { get; set; }
    }

}
