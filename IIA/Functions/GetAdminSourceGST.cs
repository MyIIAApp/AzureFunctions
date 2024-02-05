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
    /// Get admin source GST function
    /// </summary>
    public class GetAdminSourceGST
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetAdminSourceGST")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Admin Source GST");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            int adminId = -1;

            if (token.IsAdmin)
            {
                adminId = token.Id;
            }

            string adminName = Database.GetAdminSourceGST(adminId);

            return new OkObjectResult(new AdminNameResponse(adminName));
        }
    }
}
