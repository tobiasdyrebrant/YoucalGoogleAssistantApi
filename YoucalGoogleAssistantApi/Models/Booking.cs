using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoucalGoogleAssistantApi.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Service { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Price { get; set; }

        public virtual Company Company { get; set; }
    }
}