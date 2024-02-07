using System;
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
    /// Get Leader Details function
    /// </summary>
    public static class GetLeaderDetails
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetLeaderDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Leader details request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            // var leader = Database.GetLeaderDetails(token.ChapterId);
            var holeaderList = new List<Dictionary<string, string>>();
            var chapterleaderList = new List<Dictionary<string, string>>();
            BinaryData response = BlobStorage.GetFile("myiia", "LeaderDetails/IIALeaders.xlsx");
            var leaderTemp = ExcelRead.GenericExcelReader(response);
            foreach (Dictionary<string, string> dictionary in leaderTemp)
            {
                if (dictionary["ChapterId"] == "82")
                {
                    holeaderList.Add(dictionary);
                }
                else if (dictionary["Name"] != string.Empty)
                {
                    chapterleaderList.Add(dictionary);
                }
            }

            Dictionary<string, List<Dictionary<string, string>>> leader = new Dictionary<string, List<Dictionary<string, string>>>();
            leader.Add("ho", holeaderList);
            leader.Add("chapter", chapterleaderList);
            return new OkObjectResult(leader);
        }
    }
}
