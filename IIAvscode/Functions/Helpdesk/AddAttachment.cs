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
    /// Adding an attachment function
    /// </summary>
    public static class AddAttachment
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("AddAttachment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Adding an attachment");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string ticketnumber = data?.ticketnumber;
            string attachmenturl = data?.attachmenturl;

            int userId = -1;
            int adminId = -1;

            if (token.IsAdmin)
            {
                adminId = token.Id;
            }
            else
            {
                userId = token.Id;
            }

            Database.AddAttachment(ticketnumber, userId, adminId, attachmenturl);
            return new OkObjectResult(new BaseResponse(token, "Attachment Added"));
        }
    }
}
