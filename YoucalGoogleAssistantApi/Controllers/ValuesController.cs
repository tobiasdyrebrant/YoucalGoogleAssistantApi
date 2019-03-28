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
//using System.Web.Mvc;

namespace YoucalGoogleAssistantApi.Controllers
{
    [RoutePrefix("Api")]
    public class ValuesController : ApiController
    {
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
        public dynamic Post([System.Web.Http.FromBody]WebhookRequest value)
        {
            var intentName = value.QueryResult.Intent.DisplayName; //hämtar ut specifik intent som callar post
            var actualQuestion = value.QueryResult.QueryText; //hämtar ut specifik fråga användaren ställer
            var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'"; //testsvar för att se om vi kan få ut namn på intent och den frågan som ställts
            var parameters = value.QueryResult.Parameters;
            
            WebhookResponse r = new WebhookResponse //skapar en ny webhookrespons med tillhörande textsvar, 
            {
                FulfillmentText = testAnswer,
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
                                    DisplayText = testAnswer,
                                    TextToSpeech = testAnswer,
                                }
                            }
                        }
                    }
                },
                Source = "Dialogflow" //skriver ut vart sourcen kommer ifrån
            };
            var obj = r.ToString();

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
            return new System.Web.Mvc.ContentResult { Content = obj, ContentType = "application/json" }; //returnerar webhookresponsen
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
        //}
    }
}
