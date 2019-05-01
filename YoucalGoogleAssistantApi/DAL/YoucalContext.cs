using System;
using YoucalGoogleAssistantApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace YoucalGoogleAssistantApi.DAL
{
    public class YoucalContext : DbContext
    {
        public YoucalContext() : base("YoucalContext")
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Provide> Provides { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}