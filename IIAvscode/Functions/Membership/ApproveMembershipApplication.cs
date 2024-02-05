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
    /// UserComapnyProfile
    /// </summary>
    public static class ApproveMembershipApplication
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("ApproveMembershipApplication")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ApproveMembershipApplication Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                int userId = data?.userId;
                int status = data?.status;
                bool isAdmin = (bool)token?.IsAdmin;
                if (isAdmin)
                {
                    UserProfile userProfile = Database.GetUserProfile(userId, null, null);
                    if (userProfile.Id == -1)
                    {
                        return new BadRequestObjectResult(new BaseResponse(token, "Not a Valid User"));
                    }

                    if (userProfile.ProfileStatus == (int)UserProfileStatusEnum.UserProfileStatus.SubmittedMembershipProfile)
                    {
                        if (status == (int)UserProfileStatusEnum.UserProfileStatus.ApprovedMembershipProfile || status == (int)UserProfileStatusEnum.UserProfileStatus.RejectedMembershipProfile)
                        {
                            userProfile.ProfileStatus = status;
                            Database.InsertUpdateUserProfile(userProfile, token.Id);
                            return new OkObjectResult(new BaseResponse(token, "Membership Profile Status Updated"));
                        }
                        else
                        {
                            return new BadRequestObjectResult(new BaseResponse(token, "Invalid status input"));
                        }
                    }
                    else
                    {
                        return new BadRequestObjectResult(new BaseResponse(token, "MembershipProfile is not submitted yet!!"));
                    }
                }
                else
                {
                    return new BadRequestObjectResult(new BaseResponse(token, "Not a valid Admin"));
                }
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new BaseResponse(token, e.Message));
            }
        }
    }
}
