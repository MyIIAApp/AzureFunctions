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
    /// PaymentAndActivationForMembership
    /// </summary>
    public static class GetMissingMembershipYears
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetMissingMembershipYears")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetMissingMembershipYears Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string memberId = data?.memberId;
            string phoneNumber = data?.phoneNumber;
            dynamic userId = data?.userId;
            LoginMetadata token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                List<FinancialYear> yearList = new List<FinancialYear>();
                UserProfile memberProfile = Database.GetUserProfile(userId, phoneNumber, memberId);
                if (memberProfile.Id == -1 || memberProfile.ProfileStatus < 4)
                {
                    return new OkObjectResult(new BaseResponse(token, "Not a Member"));
                }

                string[] expiryYears = memberProfile.MembershipExpiryYears != null && memberProfile.MembershipExpiryYears != string.Empty ? memberProfile.MembershipExpiryYears.Contains(",") ? memberProfile.MembershipExpiryYears.Split(",") : new string[] { memberProfile.MembershipExpiryYears } : new string[] { DateTime.Now.Month < 4 ? (DateTime.Now.Year - 1).ToString() : DateTime.Now.Year.ToString() };
                int initialmemberExpiryYear = 0;
                for (int i = 0; i < expiryYears.Length; i++)
                {
                    if (expiryYears[i] != string.Empty)
                    {
                        initialmemberExpiryYear = int.Parse(expiryYears[i]);
                        if (i == expiryYears.Length - 1)
                        {
                            int breakyears = DateTime.Now.Month < 4 ? DateTime.Now.Year - initialmemberExpiryYear : DateTime.Now.Year + 1 - initialmemberExpiryYear;
                            while (breakyears > 0)
                            {
                                yearList.Add(new FinancialYear(initialmemberExpiryYear, initialmemberExpiryYear + 1));
                                initialmemberExpiryYear += 1;
                                breakyears -= 1;
                            }
                        }
                        else
                        {
                            int breakyears = int.Parse(expiryYears[i + 1]) - initialmemberExpiryYear;
                            while (breakyears > 1)
                            {
                                yearList.Add(new FinancialYear(initialmemberExpiryYear, initialmemberExpiryYear + 1));
                                initialmemberExpiryYear += 1;
                                breakyears -= 1;
                            }
                        }
                    }
                }

                return new OkObjectResult(yearList);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new BaseResponse(token, e.Message));
            }
        }
    }
}
