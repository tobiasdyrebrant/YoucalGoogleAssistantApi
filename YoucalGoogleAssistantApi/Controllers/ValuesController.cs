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
        public void Post([FromBody]string value)
        {
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

            dynamic test = "{\"fulfillmentText\": null,\"fulfillmentMessages\": [{\"card\": null,\"text\": {\"text\": [\"There is no weather in Denver\"]}}],\"source\": null,\"payload\": null,\"outputContexts\": null,\"followupEventInput\": null}";
            string json = JsonConvert.SerializeObject(test);

            return Ok(json);
        }
    }
}
