using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoucalGoogleAssistantApi.Models
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceID { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Provide> Provides { get; set; }
    }
}