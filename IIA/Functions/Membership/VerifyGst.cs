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
    /// Verify gst number
    /// </summary>
    public static class VerifyGst
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("VerifyGstNumber")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Verify Gst In request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            /*if (token == null)
            {
                return new UnauthorizedResult();
            }*/

            string resultContent = string.Empty;
            string gstNo = data?.gstNo;
            using (var client = new HttpClient())
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("gstNo", gstNo);
                headers.Add("key_secret", "bBgqj6dj6DbrNX1nu1b9wH5BJ4l1");
                /*string gstIn = JsonConvert.SerializeObject(headers);
                var content = new StringContent(gstIn, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://appyflow.in/api/verifyGST", content);
                resultContent = await result.Content.ReadAsStringAsync();*/
                var content = new FormUrlEncodedContent(headers);
                var response = await client.PostAsync("https://appyflow.in/api/verifyGST", content);
                resultContent = await response.Content.ReadAsStringAsync();
            }

            return new OkObjectResult(JsonConvert.DeserializeObject(resultContent));
        }
    }
}
