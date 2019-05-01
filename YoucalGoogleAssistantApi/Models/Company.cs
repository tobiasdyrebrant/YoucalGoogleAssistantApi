using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoucalGoogleAssistantApi.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}