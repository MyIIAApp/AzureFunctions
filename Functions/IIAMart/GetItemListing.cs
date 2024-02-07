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
    /// Get ItemsListing
    /// </summary>
    public static class GetItemListing
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetItemListing")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Item Listing request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string category = data?.category;
            string subCategory = data?.subCategory;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            int buyerId = token.Id;
            var itemListing = Database.GetItemListing(category, subCategory, buyerId);
            return new OkObjectResult(new ItemListingResponse(itemListing, token, "Items List Sent Successfully"));
        }
    }
}
