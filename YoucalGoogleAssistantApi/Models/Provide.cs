using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoucalGoogleAssistantApi.Models
{
    public class Provide
    {
        public int ProvideID { get; set; }
        public int CompanyID { get; set; }
        public int ServiceID { get; set; }

        public virtual Company Company { get; set; }
        public virtual Service Service { get; set; }
    }
}