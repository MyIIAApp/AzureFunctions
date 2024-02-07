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
    public static class GetUserProfileDetails
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetUserProfile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetUseProfile Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string memberId = data?.memberId;
            string phoneNumber = data?.phoneNumber;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                UserProfile userProfile = Database.GetUserProfile(token.Id, phoneNumber, memberId);
                return new OkObjectResult(userProfile);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new BaseResponse(token, e.Message));
            }
        }
    }
}
