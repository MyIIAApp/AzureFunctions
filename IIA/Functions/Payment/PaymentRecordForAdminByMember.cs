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
    /// Payment Record
    /// <summary>
    public class PaymentRecordForAdminByMember
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("PaymentRecordForAdminByMember")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Get Payment Record values request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string userId = data?.userId;
            try
            {
                var paymentRecordeValue = Database.GetPaymentRecordValue(int.Parse(userId), false);
                var output = new Dictionary<string, dynamic>();
                output.Add("paymentRecord", paymentRecordeValue);
                UserProfile membership = Database.GetUserProfile(int.Parse(userId), null)[0];
                string expiryYears = membership.MembershipExpiryYears;
                DateTime joinDate = membership.MembershipJoinDate;
                foreach (var i in paymentRecordeValue)
                {
                    if (i.ExpiryYear != string.Empty)
                    {
                        expiryYears = expiryYears.Replace("," + i.ExpiryYear, string.Empty);
                        expiryYears = expiryYears.Replace(i.ExpiryYear, string.Empty);
                    }
                }

                output.Add("expiryYears", expiryYears);
                output.Add("joinDate", joinDate.ToString("dd-MM-yyyy"));
                return new OkObjectResult(output);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}