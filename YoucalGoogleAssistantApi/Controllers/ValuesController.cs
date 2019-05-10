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
using System.Collections;

//using System.Web.Mvc;

namespace YoucalGoogleAssistantApi.Controllers
{
    [RoutePrefix("Api")]
    public class ValuesController : ApiController
    {
        public YoucalContext db = new YoucalContext();

        
        // POST lokaldb
        //[HttpPost]
        //[Route("post")]
        //public dynamic Post([FromBody]WebhookRequest dialogflowRequest)
        //{
        //    //Gäller Azure, går via dialogflow
        //    //var intentName = dialogflowRequest.QueryResult.Intent.DisplayName; //hämtar ut specifik intent som callar post
        //    //var actualQuestion = dialogflowRequest.QueryResult.QueryText; //hämtar ut specifik fråga användaren ställer
        //    //var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'"; //testsvar för att se om vi kan få ut namn på intent och den frågan som ställts
        //    //var parameters = dialogflowRequest.QueryResult.Parameters;

        //    //Gäller local, kör via localDb
        //    var foundcompanies = db.Companies.ToList();
        //    var antalBolag = foundcompanies.Count();
        //    var testar = "hello this is a message going out to all my fellow Americans. Haikyuu might have been too powerful for us to comprehend the extent of the aftermath it would wield us at 12/12";
           
        //    //vad som krävs för en bokning, ett företag och ett datum
        //    var company = "";
        //    var cID = 0;
        //    var hasDate = false;
        //    var cost = 0;
        //    var realEndTime = new DateTime(2019, 12, 1);
        //    try { 
        //        for (int i = 0; i < antalBolag; i++)
        //        {
        //            var companyNameIndex = foundcompanies[i].Name;
                
        //            if (testar.Contains(companyNameIndex)) //Byt ut testar mot actualQuestion för att få det i live versionen
        //            {
        //                company = companyNameIndex;
        //                cID = foundcompanies[i].CompanyID;
        //                break;
        //            }
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }

        //    var whiteSpace = " ";
        //    DateTime dateTime = new DateTime();
        //    string[] inputText = testar.Split(whiteSpace.ToCharArray());//split on a whitespace

        //    foreach (string text in inputText)
        //    {
        //        //Use the Parse() method
        //        try
        //        {
        //            dateTime = DateTime.Parse(text) + new TimeSpan(8, 0, 0);
        //            hasDate = true;
        //            break;//no need to execute/loop further if you have your date
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }


        //    if (!company.IsNullOrWhiteSpace() && hasDate == true)
        //    {
        //        var companyId = db.Companies.Single(x => x.Name == company);
        //        var serviceId = db.Provides.Single(x => x.CompanyID == companyId.CompanyID);
        //        var endtime = new TimeSpan();
        //        if (serviceId.Service.Type.Equals("Webdevelopment"))
        //        {
        //            endtime = new TimeSpan(12, 0, 0);
        //            cost = 6000;
        //        }
        //        else if (serviceId.Service.Type.Equals("Painting"))
        //        {
        //            endtime = new TimeSpan(6, 0, 0);
        //            cost = 600;
        //        }
        //        else if (serviceId.Service.Type.Equals("Hairy"))
        //        {
        //            endtime = new TimeSpan(0, 45, 0);
        //            cost = 450;
        //        }
        //        else if (serviceId.Service.Type.Equals("Sewerline"))
        //        {
        //            endtime = new TimeSpan(0, 15, 0);
        //            cost = 1000000;
        //        }

        //        realEndTime = dateTime.Add(endtime);

        //        var test = new Booking
        //        {
        //            Company = companyId,
        //            Email = "a@a.a",
        //            EndTime = realEndTime,
        //            StartTime = dateTime,
        //            Phone = 999,
        //            Service = serviceId.Service.Type,
        //            Price = cost,
        //            Username = "Ben10"
        //        }; //Tas bort i framtiden till när vi kan få ut all data vi behöver till dialogflow, new booking ska ske på annat ställe.

        //        db.Bookings.Add(test); // I framtiden spara till fält(?)
        //        db.SaveChanges(); // I framtiden ta bort --- byt mellan connectionString beroende på om det gäller local eller Azure

        //    }
        //    else if (!company.IsNullOrWhiteSpace() && hasDate == false)
        //    {
        //        return Json(new
        //        {
        //            fulfillmentText = "Ops, seems like you're missing a valid date there. Mind doing that again but this time include the date as well?",
        //            source = "Visual Studio",
        //        });
        //    }
        //    else if (company.IsNullOrWhiteSpace() && hasDate == false)
        //    {
        //        return Json(new
        //        {
        //            fulfillmentText = "Ops, seems like you're missing not only a valid date, but also a valid company name. Mind doing that again but this time include the date and company as well?",
        //            source = "Visual Studio",
        //        });
        //    }
        //    else if (company.IsNullOrWhiteSpace() && hasDate == true)
        //    {
        //        return Json(new
        //        {
        //            fulfillmentText = "Ops, seems like you're missing a valid company name. Mind doing that again but this time include the company as well?",
        //            source = "Visual Studio",
        //        });
        //    }

        //    return Json(new
        //    {
        //        fulfillmentText = "Your booking at " + company + " is set to start at " + dateTime + " and end at " + realEndTime + ". Is there anything else you would like to book?",
        //        source = "Visual Studio",


        //    });
        //}

        // POST dialogflow
        [HttpPost]
        [Route("post")]
        public dynamic Post([FromBody]WebhookRequest dialogflowRequest)
        {
            var intentName = dialogflowRequest.QueryResult.Intent.DisplayName; //hämtar ut specifik intent som callar post
            var actualQuestion = dialogflowRequest.QueryResult.QueryText; //hämtar ut specifik fråga användaren ställer
            var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'"; //testsvar för att se om vi kan få ut namn på intent och den frågan som ställts
            var parameters = dialogflowRequest.QueryResult.Parameters;

            //Gäller local, kör via localDb
            var foundcompanies = new ArrayList
            {
                "Peppes hairdresser",
                "Mikes foodtruck",
                "The repairman",
                "MML delivery"
            };
            var antalBolag = foundcompanies.Count;
            
            //vad som krävs för en bokning, ett företag och ett datum
            var company = "";
            var hasDate = false;
            var cost = 0;
            var realEndTime = new DateTime();
            try
            {
                for (int i = 0; i < antalBolag; i++)
                {
                    if (actualQuestion.Contains(foundcompanies[i].ToString())) //Byt ut testar mot actualQuestion för att få det i live versionen
                    {
                        company = foundcompanies[i].ToString();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var whiteSpace = " ";
            DateTime dateTime = new DateTime();
            string[] inputText = actualQuestion.Split(whiteSpace.ToCharArray());//split on a whitespace

            foreach (string text in inputText)
            {
                //Use the Parse() method
                try
                {
                    dateTime = DateTime.Parse(text) + new TimeSpan(8, 0, 0);
                    hasDate = true;
                    break;//no need to execute/loop further if you have your date
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            if (!company.IsNullOrWhiteSpace() && hasDate == true)
            {
                var endtime = new TimeSpan();
                if (company.Equals("Peppes hairdresser"))
                {
                    endtime = new TimeSpan(12, 0, 0);
                    cost = 6000;
                }
                else if (company.Equals("Mikes foodtruck"))
                {
                    endtime = new TimeSpan(6, 0, 0);
                    cost = 600;
                }
                else if (company.Equals("The repairman"))
                {
                    endtime = new TimeSpan(0, 45, 0);
                    cost = 450;
                }
                else if (company.Equals("MML delivery"))
                {
                    endtime = new TimeSpan(0, 15, 0);
                    cost = 1000000;
                }

                realEndTime = dateTime.Add(endtime);
            }
            else if (!company.IsNullOrWhiteSpace() && hasDate == false)
            {
                return Json(new
                {
                    fulfillmentText = "Ops, seems like you're missing a valid date there. Mind doing that again but this time include the date as well?",
                    source = "Visual Studio",
                });
            }
            else if (company.IsNullOrWhiteSpace() && hasDate == false)
            {
                return Json(new
                {
                    fulfillmentText = "Ops, seems like you're missing not only a valid date, but also a valid company name. Mind doing that again but this time include the date and company as well?",
                    source = "Visual Studio",
                });
            }
            else if (company.IsNullOrWhiteSpace() && hasDate == true)
            {
                return Json(new
                {
                    fulfillmentText = "Ops, seems like you're missing a valid company name. Mind doing that again but this time include the company as well?",
                    source = "Visual Studio",
                });
            }

            return Json(new
            {
                fulfillmentText = "Your booking at " + company + " is set to start at " + dateTime + " and end at " + realEndTime + ". Is there anything else you would like to book?",
                source = "Visual Studio",


            });
        }

        //[Route("Test")]
        //[HttpPost]
        //public async Task<IHttpActionResult> Index()
        //{
        //    return Ok("Json github push");

        //}
    }
}
