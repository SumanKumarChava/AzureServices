using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctionsProject.Models;

namespace SampleFuncApp
{
    public static class WriteToQueue
    {
        [FunctionName("WriteToQueue")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("SalesRequestInBound", Connection ="AzureWebJobsStorage")] IAsyncCollector<SalesRequest> salesRequestQueue,
            ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<SalesRequest>(body);
            log.LogInformation("Sales request received for user " + data.Name);
            await salesRequestQueue.AddAsync(data);

            string responseMessage = "Sales request is written in to the queue for user : " + data.Name;
            return new OkObjectResult(responseMessage);
        }
    }
}
