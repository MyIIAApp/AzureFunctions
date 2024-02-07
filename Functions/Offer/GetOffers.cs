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
    /// Get offers
    /// </summary>
    public static class GetOffers
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetOffers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get offers request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string category = data?.category;
            category = string.IsNullOrEmpty(category) ? "Others" : category;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            // var offers = Database.GetOffers(category);
            BinaryData response = BlobStorage.GetFile("offers", "offersexcel/IIA- Offers.xlsx");
            List<dynamic> offers = ExcelRead.GenericExcelReader(response);
            List<dynamic> filteredOffers = offers.Where(item => item["Category"].Equals(category)).ToList();
            return new OkObjectResult(new OfferResponse(filteredOffers, token, "Offers Sent Successfully"));
        }
    }
}
