using System;
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
    /// Create Ticket function
    /// </summary>
    public static class CreateTicket
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateTicket")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Raise Ticket request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string title = data?.title;
            string description = data?.description;
            string category = data?.category;
            string attachmenturl = data?.attachmenturl;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                return new BadRequestObjectResult("Invalid Inputs");
            }

            if (!Tickets.Categories.Contains(category))
            {
                return new BadRequestObjectResult("Invalid Category");
            }

            var ticketNumber = Database.CreateTicket(title, description, category, token.Id, attachmenturl);
            return new OkObjectResult(new CreateTicketResponse(ticketNumber, token, "Ticket raised"));
        }
    }
}
