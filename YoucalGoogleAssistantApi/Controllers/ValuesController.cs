using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System.IO;
using System.Threading.Tasks;
using System.Web.Script;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Newtonsoft.Json;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;
using YoucalGoogleAssistantApi.DAL;
using YoucalGoogleAssistantApi.Models;

//using System.Web.Mvc;

namespace YoucalGoogleAssistantApi.Controllers
{
    [RoutePrefix("Api")]
    public class ValuesController : ApiController
    {
        public YoucalContext db = new YoucalContext();

        
        // POST api/values
        [HttpPost]
        [Route("post")]
        public dynamic Post([FromBody]WebhookRequest dialogflowRequest)
        {
            //Gäller Azure, går via dialogflow
            var intentName = dialogflowRequest.QueryResult.Intent.DisplayName; //hämtar ut specifik intent som callar post
            var actualQuestion = dialogflowRequest.QueryResult.QueryText; //hämtar ut specifik fråga användaren ställer
            var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'"; //testsvar för att se om vi kan få ut namn på intent och den frågan som ställts
            var parameters = dialogflowRequest.QueryResult.Parameters;

            //Gäller local, kör via localDb
            var foundcompanies = db.Companies.ToList();
            var antalBolag = foundcompanies.Count();
            var testar = "hello this is a message going out to all my fellow Americans. Haikyuu might have been too powerful for us to comprehend the extent of the aftermath it would wield us at 12/12";

            var company = "";
            for (int i = 0; i <= antalBolag; i++)
            {
                var companyNameIndex = foundcompanies[i].Name;
                if (testar.Contains(companyNameIndex)) //Byt ut testar mot actualQuestion för att få det i live versionen
                {
                    var test = new Booking
                    {
                        Company = null,
                        Email = "a@a.a",
                        EndTime = Convert.ToDateTime("2019-12-13"),
                        StartTime = Convert.ToDateTime("2019-12-13"),
                        Phone = 999,
                        Price = 1000,
                        Username = "Ben10"
                    }; //Tas bort i framtiden till när vi kan få ut all data vi behöver till dialogflow, new booking ska ske på annat ställe.

                    db.Bookings.Add(test); // I framtiden spara till fält(?)
                    db.SaveChanges(); // I framtiden ta bort --- byt mellan connectionString beroende på om det gäller local eller Azure

                }

            }

            var hasDate = false;
            var whiteSpace = " ";
            DateTime dateTime = new DateTime();
            string[] inputText = testar.Split(whiteSpace.ToCharArray());//split on a whitespace

            foreach (string text in inputText)
            {
                //Use the Parse() method
                try
                {
                    dateTime = DateTime.Parse(text);
                    hasDate = true;
                    break;//no need to execute/loop further if you have your date
                }
                catch (Exception ex)
                {

                }
            }
            
            

            return Json(new
            {

                fulfillmentText = testAnswer + " Is this for real?!?!?!",
                source = "Visual Studio",


            });
        }

        [Route("Test")]
        [HttpPost]
        public async Task<IHttpActionResult> Index()
        {
            return Ok("Json github push");

        }
    }
}
