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
            string committeeId = data?.committeeId;

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

            int returnedUserId = Database.AddComment(ticketnumber, comments, userId, adminId);

            if (token.IsAdmin && returnedUserId != 0)
            {
                SendNotification.SendNotifications("IIA issues and Problems", "You received a new message on ticket number " + ticketnumber, returnedUserId.ToString());
                string phoneNumber = Database.AddCommentGetPhoneNumber(ticketnumber);
                try
                {
                    string message = SmsHelper.SendSMS2(phoneNumber, ticketnumber.ToString(), (int)TypeOfMessage.Ticket);
                }
                catch (Exception e)
                {
                    log.LogInformation(e.Message);
                }
            }
            else if (!token.IsAdmin && returnedUserId != 0)
            {
                if (!string.IsNullOrEmpty(committeeId))
                {
                    SmsHelper.SendMessageToChairmen(int.Parse(ticketnumber), int.Parse(committeeId), log);
                }
            }

            return new OkObjectResult(new BaseResponse(token, "Comment Added"));
        }
    }
}