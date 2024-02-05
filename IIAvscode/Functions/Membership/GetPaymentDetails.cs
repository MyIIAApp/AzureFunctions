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
    /// Get Payment details
    /// </summary>
    public static class GetPaymentDetails
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetPaymentDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetPaymentDetails Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            if (!token.IsAdmin)
            {
                return new BadRequestObjectResult("Not a Admin");
            }

            string userId;
            try
            {
                userId = data?.userId;
                UserProfile userProfile = Database.GetUserProfile(int.Parse(userId), null, null);
                PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile.ProfileStatus, userProfile.AnnualTurnOver, userProfile.GSTIN);
                return new OkObjectResult(paymentDetail);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Bad Request");
            }
        }
    }
}