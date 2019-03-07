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
            dynamic test = "{'responseId':'c1712184-8057-4a29-878b-b79c767e7797'," +
                           " 'session': 'projects/resturant-search-agent-e0dd1/agent/sessions/62761e81-b964-c682-9fd5-cd400641b0c2'," +
                           "'queryResult':{'queryText':'Find carwash close by?','parameters':'param':'param value'},"+
                           "'allRequiredParamsPresent':true,"+
                           "'fulfillmentText':'Hej'"+
                           "'fulfillmentMessages':'[{'text': {'text': ['Text defined in Dialogflow's console for the intent that was matched']}]'"+
                           "'outputContexts': [{'name': 'projects/resturant-search-agent-e0dd1/agent/sessions/62761e81-b964-c682-9fd5-cd400641b0c2/contexts/generic'," +
                           "'intent': {'name': 'projects/resturant-search-agent-e0dd1/agent/intents/9fc700cf-bb61-47b0-a85a-6eed23d5817c','displayName': 'Matched Intent Name'}'," +
                           "'intentDetectionConfidence': 1,'diagnosticInfo': { },'languageCode': 'en'},'originalDetectIntentRequest': {}";
            return Ok(test);
        }
    }
}
