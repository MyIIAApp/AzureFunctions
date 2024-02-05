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
    /// Get Ticket Summary For All Chapters function
    /// </summary>
    public static class GetSummaryForAllChapters
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetSummaryForAllChapters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Ticket Summary For All Chapters request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            var ticket = Database.GetSummaryForAllChapters();
            var ticketiia = new List<Tickets>();
            var ticketmember = new List<Tickets>();
            var ticketclosed = new List<Tickets>();
            for (int i = 0; i < ticket.Count; i++)
            {
                if (ticket[i].Status == "Pending on IIA")
                {
                    ticketiia.Add(ticket[i]);
                }
                else if (ticket[i].Status == "Pending on Member")
                {
                    ticketmember.Add(ticket[i]);
                }
                else if (ticket[i].Status == "Closed")
                {
                    ticketclosed.Add(ticket[i]);
                }
            }

            return new OkObjectResult(new TicketResponse(ticketiia, ticketmember, ticketclosed, token, "Get Ticket Details For All raised"));
        }
    }
}
