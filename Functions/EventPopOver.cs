using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Get EventPopOver
    /// </summary>
    public static class EventPopOver
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("EventPopOver")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Pop URL Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string url = Environment.GetEnvironmentVariable("EventPopURL");
            string redirectingUrl = Environment.GetEnvironmentVariable("EventRidirectingURL");
            string expiryTime = Environment.GetEnvironmentVariable("EventPopShowDifference");
            string showImage = Environment.GetEnvironmentVariable("EventPopShowImage");
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("url", url != null ? url : string.Empty);
            response.Add("redirectingUrl", redirectingUrl ?? string.Empty);
            response.Add("expiryTime", expiryTime ?? "24");
            response.Add("showImage", showImage ?? "0");
            return new OkObjectResult(response);
        }
    }
}
