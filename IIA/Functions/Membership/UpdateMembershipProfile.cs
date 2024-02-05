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
    /// Update Company Profile for Membership Application
    /// </summary>
    public static class UpdateMembershipProfile
    {
        /// <summary>
        /// UpdateCompanyProfile Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("UpdateMembershipProfile")]
        public static async Task<IActionResult> RunUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("UpdateMembershipProfile Request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string memberId = data?.memberId;
            string phoneNumber = data?.phoneNumber;
            int userId = data?.id;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                MembershipProfile companyProfile = new MembershipProfile(data, userId);
                Database.UpdateMembershipProfile(companyProfile, token.IsAdmin ? token.Id : -1);
                return new OkObjectResult(new BaseResponse(token, "User Membership Profile Updated"));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new BaseResponse(token, e.Message));
            }
        }
    }
}