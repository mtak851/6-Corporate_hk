using System;
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
            var name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            log.Info("Start read json file.");

            try
            {
                // Return dummy json.
                //var file = System.IO.File.ReadAllText(@".\Models\Resource.json");
                var jsonStr =
                    "{\r\n  \"Resources\": [\r\n    {\r\n      \"id\": \"5c951c0a-3e91-436a-81ae-59ede453672c\",\r\n      \"createdDateTimeUTC\": \"2018-12-08T09:00:00.000Z\",\r\n      \"lastModifiedDateTimeUTC\": \"2018-12-08T09:00:05.000Z\",\r\n      \"title\": \"Hour of Code\",\r\n      \"subtitle\": \"Growing digitally in the garden\",\r\n      \"description\": \"The annual global 'Hour of Code' campaign and Computer Science Education Week is kicking off on in December and we want as many kids in New Zealand to get to take part as possible.\",\r\n      \"headerImageBase64Bytes\": \"\",\r\n      \"duration\": \"1 day\",\r\n      \"provider\": \"OMGTech\",\r\n      \"status\": \"draft\",\r\n      \"contentUrls\": [\r\n        \"url\",\r\n        \"url\"\r\n      ],\r\n      \"categoryTags\": {\r\n        \"resourceTypes\": [\r\n          \"people\",\r\n          \"resource\"\r\n        ],\r\n        \"languages\": [\r\n          \"TeReoMaori\",\r\n          \"English\"\r\n        ],\r\n        \"locations\": [\r\n          \"Auckland\"\r\n        ],\r\n        \"topics\": [\r\n          \"garden\",\r\n          \"robots\",\r\n          \"coding\"\r\n        ],\r\n        \"ages\": [\r\n          \"year12\",\r\n          \"year13\"\r\n        ]\r\n      },\r\n      \"qualityRatingPoints\": 150,\r\n      \"topicRatingPoints\": 45\r\n    }\r\n  ]\r\n}";
                var json = JsonConvert.DeserializeObject(jsonStr);
                return req.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception e)
            {
                log.Info("{0}", e.Message);
                return req.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
