using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// SendEnquiry
    /// </summary>
    public static class DeleteItemDetailsBySeller
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("DeleteItemDetailsBySeller")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Edit Details request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string itemId;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            bool toggle = data?.toggle;
            string active = data?.active;
            itemId = data?.id;
            try
            {
                Database.DeleteItemDetailsBySeller(itemId, toggle, 1 - int.Parse(active));
                return new OkObjectResult(new BaseResponse(token, "item deleted"));
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Invalid Input");
            }
        }
    }
}
