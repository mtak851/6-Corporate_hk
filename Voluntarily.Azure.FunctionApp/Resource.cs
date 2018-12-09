using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Voluntarily.Azure.FunctionApp
{
    public static class Resource
    {
        [FunctionName("resource")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Parse query parameter.
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Return dummy json from a file.
            string file = System.IO.File.ReadAllText(@".\Models\Resource.json");
            object json = JsonConvert.DeserializeObject(file);

            return req.CreateResponse(HttpStatusCode.OK, json);
        }
    }
}
