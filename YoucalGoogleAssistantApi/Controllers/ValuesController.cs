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

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("post")]
        public dynamic Post([FromBody]WebhookRequest dialogflowRequest)
        {
            var intentName = dialogflowRequest.QueryResult.Intent.DisplayName; //hämtar ut specifik intent som callar post
            var actualQuestion = dialogflowRequest.QueryResult.QueryText; //hämtar ut specifik fråga användaren ställer
            var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'"; //testsvar för att se om vi kan få ut namn på intent och den frågan som ställts
            var parameters = dialogflowRequest.QueryResult.Parameters;



            Booking booking = new Booking();
            

            var foundcompanies = db.Companies.ToList();
            

            

            //var hittatSpecifiktBolag = db.Companies.Where(x => x.Name == actualQuestion).Single();

            var antalBolag = foundcompanies.Count();

          


            for (int i = 0; i<= antalBolag; i++)
            {
                if (actualQuestion.Contains(foundcompanies[i].ToString()))
                {
                    booking.Company = foundcompanies[i];
                    
                    
                    db.Bookings.Add(booking);
                    db.SaveChanges();

                }
                
            }
            
            


          
            //WebhookResponse r = new WebhookResponse //skapar en ny webhookrespons med tillhörande textsvar, 
            //{
            //    FulfillmentText = testAnswer,
            //    FulfillmentMessages =
            //    {
            //        new Intent.Types.Message
            //        {
            //            SimpleResponses = new Intent.Types.Message.Types.SimpleResponses
            //            {
            //                SimpleResponses_=
            //                {
            //                    new Intent.Types.Message.Types.SimpleResponse
            //                    {
            //                        DisplayText = testAnswer,
            //                        TextToSpeech = testAnswer,
            //                    }
            //                }
            //            }
            //        }
            //    },
            //    Source = "Dialogflow", //skriver ut vart sourcen kommer ifrån

            //};
            //var obj = r.ToString();

            //var message = new Intent.Types.Message
            //{
            //    Card = new Intent.Types.Message.Types.Card
            //    {
            //        Title = "Hej",

            //    }
            //};
            //var JsonSerializerSettings = new JsonSerializerSettings
            //{
            //    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            //};
            //var res = new WebhookResponse
            //{
            //    FulfillmentMessages =
            //    {
            //        message
            //    },
            //    FulfillmentText = value.QueryResult.FulfillmentText,
            //    Source = ""


            //};

            //var json = JsonConvert.SerializeObject(res);
            //var interimObject = JsonConvert.DeserializeObject<WebhookResponse>(json);
            //var myJsonOutput = JsonConvert.SerializeObject(interimObject, JsonSerializerSettings);
            //res.OutputContexts = new StringContent(myJsonOutput, )
            var dialogflowResponse = new WebhookResponse
            {
                FulfillmentText = "hejsan",
                FulfillmentMessages =
                {
                    new Intent.Types.Message
                    {
                        SimpleResponses = new Intent.Types.Message.Types.SimpleResponses
                        {
                            SimpleResponses_=
                            {
                                new Intent.Types.Message.Types.SimpleResponse
                                {
                                    DisplayText = "hejsan",
                                    TextToSpeech = "hej",

                                }
                            }
                        }
                    }
                },
                Source = "Dialogflow",
                //Payload =
                //{
                //    Fields =
                //    {
                //        Google =
                //        {
                //            ExpectUserResponse = true
                //        }
                //    }
                //},
                //OutputContexts =
                //{
                //   new Google.Cloud.Dialogflow.V2.Context
                //   {
                //       Name ="projects/${PROJECT_ID}/agent/sessions/${ SESSION_ID }/ contexts / context name",
                //       LifespanCount = 4,
                //       Parameters = value.QueryResult.Parameters
                //   }
                //},
                //FollowupEventInput =
                //{
                //    Name = null,
                //    LanguageCode = "en-US",
                //    Parameters = null
                //}


            };

            var jsonResponse = dialogflowResponse.ToString();
            jsonResponse.ToLower();

            return Json(new
            {

                fulfillmentText = "Is this for real?!?!?!",
                source = "Dialogflow",


            });
            /*return new Microsoft.AspNetCore.Mvc.ContentResult { Content = jsonResponse, ContentType = "application/json"};*/ //returnerar webhookresponsen
        }
        

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}

        //[Route("Test")]
        //[HttpPost]
        //public async Task<IHttpActionResult> Index() {
        //    return Ok("Json github push");
        //
    }
}
