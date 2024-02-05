using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IIABackend
{
    /// <summary>
    /// Create Ticket function
    /// </summary>
    public static class UpdateTicket
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("UpdateTicket")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Update Ticket request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                int ticketNumber = data?.ticketNumber;
                int committeeId = data?.committeeId;
                var resp = new
                {
                    ID = $"{committeeId}",
                    ChangedTicket = $"{ticketNumber}",
                };

                Database.UpdateTicket(ticketNumber, committeeId);
                SmsHelper.SendMessageToChairmen(ticketNumber, committeeId, log);
                return new OkObjectResult(resp);
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
                return new OkObjectResult(e.Message);
            }
        }
    }
}
