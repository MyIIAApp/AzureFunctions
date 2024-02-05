using System;
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
    /// Payment Record
    /// <summary>
    public class PaymentRecord
{
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
[FunctionName("PaymentRecord")]
public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route =null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Get Payment Record values request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            // if (token == null)
            // {
            //     return new UnauthorizedResult();
            // }

            int UserId = data?.UserId;
            var PaymentRecordeValue = Database.GetPaymentRecordValue(UserId);
            return new OkObjectResult(new PaymentRecordResponse(PaymentRecordeValue,token,"Successful"));
        }
    }
}