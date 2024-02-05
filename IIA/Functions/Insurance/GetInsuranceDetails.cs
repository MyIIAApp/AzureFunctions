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
    /// Get Insurance Details funtion
    /// </summary>
    public static class GetInsuranceDetails
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetInsuranceDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Insurance request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string resultContent = string.Empty;
            using (var client = new HttpClient())
            {
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("mobile", token.PhoneNumber);
                string head = JsonConvert.SerializeObject(headers);
                var content = new StringContent(head, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://gemsapi.sana.insure:9603/users/getAllUserRecord", content);
                resultContent = await result.Content.ReadAsStringAsync();
            }

            dynamic details = JsonConvert.DeserializeObject(resultContent);
            if (details?.success == true /*&& details?.response?.primary?.PolicyNumber != string.Empty*/)
            {
                details.success = true;
            }
            else
            {
                details.success = false;
            }

            return new OkObjectResult(details);
        }
    }
}
