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
    /// Adding a comment function
    /// </summary>
    public static class AddComment
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("AddComment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Adding a comment");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string ticketnumber = data?.ticketnumber;
            string comments = data?.comments;

            if (string.IsNullOrEmpty(comments))
            {
                return new BadRequestObjectResult("Invalid Inputs");
            }

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
            
            Database.AddComment(ticketnumber, comments, userId, adminId);

            if(token.IsAdmin){
                SendNotification.sendNotification("Message","You received new message",token.Id.ToString());
            }
            return new OkObjectResult(new BaseResponse(token, "Comment Added"));
        }
    }
}
