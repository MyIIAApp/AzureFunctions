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
    /// Membership Dashboard
    /// <summary>
    public class MembershipDashboard
{
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
[FunctionName("MembershipDashboard")]
public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route =null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Get Membership dashboard values request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            int chapterId = token.ChapterId;
            var membershipDashboardValues = Database.GetMembershipDashboardValues(chapterId);
            return new OkObjectResult(membershipDashboardValues);
        }
    }
}