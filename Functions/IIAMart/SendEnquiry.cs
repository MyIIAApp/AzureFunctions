using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// SendEnquiry
    /// </summary>
    public static class SendEnquiry
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("SendEnquiry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Send Enquiry request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string itemId = data?.itemId;
            string message = data?.message;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            Database.SendEnquiry(message, int.Parse(itemId), token.Id);
            string phoneNumber = Database.EnquiryGetPhoneNumberOfSeller(itemId);
            try
            {
                string messageSms = SmsHelper.SendSMS2(phoneNumber, string.Empty, (int)TypeOfMessage.Enquiry);
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }

            return new OkObjectResult(new BaseResponse(token, "Enquiry created"));
        }
    }
}
