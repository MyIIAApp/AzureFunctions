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
    /// Upload File Function
    /// </summary>
    public static class UploadFile
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("UploadFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get rates request!");

            var formdata = await req.ReadFormAsync();
            var file = req.Form.Files["file"];
            var fileDirectory = formdata["fileDirectory"];
            var fileName = formdata["fileName"];

            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            var path = BlobStorage.UploadFile(file, fileDirectory, fileName);
            return new OkObjectResult(new FileResponse(path, token, "News created"));
        }
    }
}
