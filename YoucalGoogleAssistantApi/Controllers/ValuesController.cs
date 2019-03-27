﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using System.IO;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Newtonsoft.Json;


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
        public WebhookResponse Post([FromBody]WebhookRequest value)
        {
            var intentName = value.QueryResult.Intent.DisplayName; //hämtar ut specifik intent som callar post
            var actualQuestion = value.QueryResult.QueryText; //hämtar ut specifik fråga användaren ställer
            var testAnswer = $"Dialogflow Request for intent '{intentName}' and question '{actualQuestion}'"; //testsvar för att se om vi kan få ut namn på intent och den frågan som ställts
            
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
                                    TextToSpeech = testAnswer
                                }
                            }
                        }
                    }
                },
                Source = "Dialogflow" //skriver ut vart sourcen kommer ifrån
            };

            return r; //returnerar webhookresponsen
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [Route("Test")]
        [HttpPost]
        public async Task<IHttpActionResult> Index() {
            return Ok("hej igen");
        }
    }
}
