using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    /// Tickets in Excel
    /// </summary>
    public static class GetClickedNumberTableContent
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetClickedNumberTableContent")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Clicked Number Table Content Request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string dataType = data?.dataType;
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            try
            {
                result.Add("data", Database.GetClickedValueData(dataType, token.ChapterId));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            /*  return new OkObjectResult("success");*/
            return new OkObjectResult(result);
        }
    }
}
