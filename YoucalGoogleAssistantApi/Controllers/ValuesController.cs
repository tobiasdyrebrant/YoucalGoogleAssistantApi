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

            


            //var foundcompanies = db.Companies.ToList();

            //var antalBolag = foundcompanies.Count();


            var testar = "hello this is a message going out to all my fellow Americans. Haikyuu might have been too powerful for us to comprehend the extent of the aftermath it would wield us at 12/12";

            //for (int i = 0; i <= antalBolag; i++)
            //{

            //    var companyNameIndex = foundcompanies[i].Name;
            //    if (testar.Contains(companyNameIndex)) //Byt ut testar mot actualQuestion för att få det i live versionen
            //    {
            //        var test = new Booking
            //        {
            //            Company = null,
            //            Email = "a@a.a",
            //            EndTime = Convert.ToDateTime("2019-12-13"),
            //            StartTime = Convert.ToDateTime("2019-12-13"),
            //            Phone = 999,
            //            Price = 1000,
            //            Username = "Ben10"
            //        }; //Tas bort i framtiden till när vi kan få ut all data vi behöver till dialogflow, new booking ska ske på annat ställe.

            //        db.Bookings.Add(test); // I framtiden spara till fält(?)
            //        db.SaveChanges(); // I framtiden ta bort

            //    }

            //}

            var hasDate = false;
            var whiteSpace = " ";
            DateTime dateTime = new DateTime();
            string[] inputText = testar.Split( whiteSpace.ToCharArray() );//split on a whitespace

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

                fulfillmentText = testAnswer + " Is this for real?!?!?!",
                source = "Visual Studio",


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
