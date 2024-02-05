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
    /// Get offer detail
    /// </summary>
    public static class UpdateEnquiryDetailsBySeller
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("UpdateEnquiryDetailsBySeller")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Update enquiry detail request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string itemId;

            try
            {
                itemId = data?.EnquiryId;
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Invalid Input");
            }

            Database.UpdateEnquiryDetailsBySeller(int.Parse(itemId));
            return new OkObjectResult(new BaseResponse(token, "enquiry updated"));
        }
    }
}
