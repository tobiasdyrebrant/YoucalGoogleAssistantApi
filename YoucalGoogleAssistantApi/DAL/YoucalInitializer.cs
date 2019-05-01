using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoucalGoogleAssistantApi.Models;
using System.Data.Entity;

namespace YoucalGoogleAssistantApi.DAL
{
    public class YoucalInitializer : DropCreateDatabaseIfModelChanges<YoucalContext>
    {
        protected override void Seed(YoucalContext context)
        {
            var companies = new List<Company>
            {
            new Company{Name="Haikyuu",Phone= 112,Address= "benisgatan 14"},
            new Company{Name="One china piece",Phone= 113,Address= "benisgatan 13"},
            new Company{Name="Toys' 'r' men",Phone= 111,Address= "benisgatan 12"},
            new Company{Name="Chocolate factory",Phone= 110,Address= "benisgatan 11"},
            };

            companies.ForEach(s => context.Companies.Add(s));
            context.SaveChanges();
            var services = new List<Service>
            {
            new Service{ServiceID=1,Type="Webdevelopment",},
            new Service{ServiceID=2,Type="Marine",},
            new Service{ServiceID=3,Type="Hisoka",},
            new Service{ServiceID=4,Type="Sewerline",},
            };
            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();
            var provides = new List<Provide>
            {
            new Provide{CompanyID=1,ServiceID=1},
            new Provide{CompanyID=1,ServiceID=2},
            new Provide{CompanyID=2,ServiceID=2},
            new Provide{CompanyID=3,ServiceID=3},
            new Provide{CompanyID=4,ServiceID=4},
            };
            provides.ForEach(s => context.Provides.Add(s));
            context.SaveChanges();
        }
    }
}