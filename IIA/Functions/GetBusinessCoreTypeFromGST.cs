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
    /// Get business core type function
    /// </summary>
    public class GetBusinessCoreTypeFromGST
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetBusinessCoreTypeFromGST")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Admin Name");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            List<string> gstList = Database.GetListOfGST();

            foreach (string gst in gstList)
            {
                string resultContent = string.Empty;
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("gstNo", gst);
                    headers.Add("key_secret", "bBgqj6dj6DbrNX1nu1b9wH5BJ4l1");
                    var content = new FormUrlEncodedContent(headers);
                    var response = await client.PostAsync("https://appyflow.in/api/verifyGST", content);
                    resultContent = await response.Content.ReadAsStringAsync();
                }

                Database.InsertInGSTBusiness(gst, resultContent);
            }

            return new OkObjectResult(true);
        }
    }
}

