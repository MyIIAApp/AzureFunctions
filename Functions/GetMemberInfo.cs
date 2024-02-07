using System.Collections.Generic;
using System.IO;
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
    /// Get member info
    /// </summary>
    public class GetMemberInfo
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetMemberInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Member Info");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            List<Dictionary<string, dynamic>> memberInfo = new List<Dictionary<string, dynamic>>();
            string startId = data?.StartId;
            string endId = data?.EndId;

            if (int.Parse(endId) - int.Parse(startId) <= 99)
            {
                memberInfo = Database.GetMemberInfo(startId, endId);
            }

            return new OkObjectResult(memberInfo);
        }
    }
}
