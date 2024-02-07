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
    /// B2B Admin Block Unblock Listing
    /// </summary>
    public class B2BAdminBlockUnblockListing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="B2BAdminBlockUnblockListing"/> class.
        /// </summary>
        /// /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("B2BAdminBlockUnblockListing")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("B2B Admin Block/Unblock Listing Request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string update = data?.update;
            string itemId = data?.itemId;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                Database.B2BAdminBlockUnblockListing(update, int.Parse(itemId));
                return new OkObjectResult(new BaseResponse(token, "Successfully status updated"));
            }
            catch (Exception)
            {
                return new OkObjectResult(new BaseResponse(token, "Failed to update status"));
            }
        }
    }
}
