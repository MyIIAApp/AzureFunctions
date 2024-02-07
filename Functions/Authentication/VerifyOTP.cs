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
    /// Verfity OTP function
    /// </summary>
    public static class VerifyOTP
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("VerifyOTP")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("OTP verification request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string phoneNumber = data?.phoneNumber;
            string otp = data?.otp;
            bool isAdmin = data?.isAdmin != null ? (bool)data.isAdmin : false;

            if (string.IsNullOrEmpty(otp) || otp.Length != 6)
            {
                return new BadRequestObjectResult("Invalid OTP");
            }

            // Magic OTP
            if (!FunctionUtility.ValidateOTPToken(req, phoneNumber, otp))
            {
                return new BadRequestObjectResult("Invalid OTP");
            }

            int id;
            List<string> unitNameList = new List<string>();
            if (!isAdmin)
            {
                id = Database.CreateUserIfNotExists(phoneNumber);
                List<UserProfile> userProfile = Database.GetUserProfile(id, phoneNumber);
                List<LoginMetadata> tokenList = new List<LoginMetadata>();
                userProfile.ForEach(
                    profile =>
                    {
                        var token = JWTTokenBuilder.GenerateJwtToken(phoneNumber, profile.Id.ToString(), isAdmin, profile.ProfileStatus.ToString(), profile.ChapterId, profile.ChapterName);
                        tokenList.Add(token);
                        unitNameList.Add(profile.UnitName);
                    });

                return new OkObjectResult(new BaseResponse(tokenList, unitNameList, "Verified!"));
            }
            else
            {
                id = Database.CheckIfAdminExistsOrNot(phoneNumber);
                Chapter res = Database.GetChapter(1, id);
                List<LoginMetadata> tokenList = new List<LoginMetadata>();
                var token = JWTTokenBuilder.GenerateJwtToken(phoneNumber, id.ToString(), isAdmin, "0", res.Id, res.Name);
                tokenList.Add(token);
                return new OkObjectResult(new BaseResponse(tokenList, unitNameList, "Verified!"));
            }
        }
    }
}
