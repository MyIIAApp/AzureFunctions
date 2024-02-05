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
    /// Send an OTP
    /// </summary>
    public static class SendOTP
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("SendOTP")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Login request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string phoneNumber = data?.phoneNumber;
            bool isAdmin = data?.isAdmin != null ? (bool)data.isAdmin : false;

            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 10)
            {
                return new BadRequestObjectResult("Invalid Phone Number");
            }

            if (isAdmin)
            {
                var hasAccess = Database.CheckIfAdminExistsOrNot(phoneNumber) > 0;
                if (!hasAccess)
                {
                    return new UnauthorizedResult();
                }
            }

            // Send SMS OTP
            var tokenString = JWTTokenBuilder.GenerateOTPJwtToken(phoneNumber);
            if(tokenString=="error"){
                return new BadRequestObjectResult("OTP NOT SENT");
            }
            // Store OTP in temp cache
            return new OkObjectResult(new OtpResponse(tokenString, phoneNumber));
        }
        
    }
}
