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
    /// Offer Create function
    /// </summary>
    public static class CreateOffer
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateOffer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get rates request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var token = FunctionUtility.ValidateToken(req);
            if (token == null || !token.IsAdmin)
            {
                return new UnauthorizedResult();
            }

            string categoryId = data?.categoryId;
            string organisationName = data?.organisationName;
            string title = data?.title;
            string percentageDiscount = data?.percentageDiscount;
            string fixedDiscount = data?.fixedDiscount;
            string organisationAddress = data?.organisationAddress;
            string city = data?.city;
            string email = data?.email;
            string phoneNumber = data?.phoneNumber;
            string nationalValidity = data?.nationalValidity;
            string startDate = data?.startDate;
            string expiryDate = data?.expiryDate;
            string imagePath = data?.imagePath;
            string description = data?.description;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                return new BadRequestObjectResult("Invalid Inputs");
            }

            Database.CreateOffer(int.Parse(categoryId), organisationName, title, int.Parse(percentageDiscount), int.Parse(fixedDiscount), organisationAddress, city, email, phoneNumber, bool.Parse(nationalValidity), DateTime.Parse(startDate), DateTime.Parse(expiryDate), imagePath, description);
            return new OkObjectResult(new BaseResponse(token, "Offer created"));
        }
    }
}
