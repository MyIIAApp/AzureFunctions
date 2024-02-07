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
    /// Update Member Profile Picture
    /// </summary>
    public static class UpdateMemberProfilePicture
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("UpdateMemberProfilePicture")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Update Member Profile Picture");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string photoPath = data?.photoPath;
            int id;
            try
            {
                LoginMetadata token = FunctionUtility.ValidateToken(req);
                id = token.Id;
                Database.UpdateMemberProfilePicture(id, photoPath);
                return new OkObjectResult(new BaseResponse(token, photoPath));
            }
            catch (Exception)
            {
                    return new UnauthorizedResult();
            }
        }
    }
}
