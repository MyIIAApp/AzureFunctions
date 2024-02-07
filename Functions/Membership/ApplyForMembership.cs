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
    /// Membership Application
    /// </summary>
    public static class ApplyForMembership
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("ApplyForMembership")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ApplyForMembership Requst");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                MembershipProfile companyProfile = new MembershipProfile(data, token.IsAdmin ? -1 : token.Id);
                UserProfile existingProfile = Database.GetUserProfile(token.IsAdmin ? -1 : token.Id, token.IsAdmin ? companyProfile.PhoneNumber : null, null);
                try
                {
                    if (companyProfile.Id == -1)
                    {
                        companyProfile.ProfileStatus = (int)UserProfileStatusEnum.UserProfileStatus.SubmittedMembershipProfile;
                        Database.AdminInsertMembershipProfile(companyProfile, token.Id);
                        return new OkObjectResult(new BaseResponse(token, "Membership Profile is Added"));
                    }

                    if (existingProfile.Id == -1 || existingProfile.ProfileStatus < 4)
                    {
                        companyProfile.ProfileStatus = (int)UserProfileStatusEnum.UserProfileStatus.SubmittedMembershipProfile;
                        Database.UpdateMembershipProfile(companyProfile, token.IsAdmin ? token.Id : -1);
                        return new OkObjectResult(new BaseResponse(token, "Membership Profile is Added"));
                    }
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(new BaseResponse(token, e.Message));
                }

                return new BadRequestObjectResult(new BaseResponse(token, "Membership Profile Already Exists"));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new BaseResponse(token, e.Message));
            }
        }
    }
}