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
    public static class DownloadMemberDetails
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("downloadMembersDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Payments in Excel Request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var token = FunctionUtility.ValidateToken(req);
            string path;
            try
            {
                path = ExcelWriteForMembers.GenericExcelWriterForMembers();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            /*  return new OkObjectResult("success");*/
            return new OkObjectResult(new FileResponse(path, token, "Excel Created"));
        }
    }
}
