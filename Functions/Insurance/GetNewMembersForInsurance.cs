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
    /// Get New Members For Insurance
    /// </summary>
    public static class GetNewMembersForInsurance
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetNewMembersForInsurance")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get New Members For Insurance!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string starting = data?.startingDate;
            string ending = data?.endingDate;
            DateTime startingDate;
            DateTime endingDate;
            try
            {
                startingDate = DateTime.Parse(starting);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            try
            {
                endingDate = DateTime.Parse(ending);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            TimeSpan fixDays = new TimeSpan(7, 0, 0, 0);
            if (endingDate - startingDate <= fixDays && endingDate >= startingDate)
            {
                List<UserProfileForInsurance> memberDetails = Database.GetNewMembersForInsurance(startingDate, endingDate);
                return new OkObjectResult(memberDetails);
            }
            else
            {
                return new BadRequestObjectResult("Interval must be Less than or Equal to 7 Days");
            }
        }
    }
}
