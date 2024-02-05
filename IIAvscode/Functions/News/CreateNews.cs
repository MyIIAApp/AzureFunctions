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
    /// News Create function
    /// </summary>
    public static class CreateNews
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get rates request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var token = FunctionUtility.ValidateToken(req);
            if (token == null || !token.IsAdmin)
            {
                return new UnauthorizedResult();
            }

            string title = data?.title;
            string description = data?.description;
            string link = data?.link;
            string category = data?.category;
            string imagePath = data?.imagePath;
            int creatorAdminId = token.Id;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(link))
            {
                return new BadRequestObjectResult("Invalid Inputs");
            }

            Database.CreateNews(title, description, link, category, imagePath, creatorAdminId);
            SendNotification.sendNotification("Hlo","Hi","all");
            return new OkObjectResult(new BaseResponse(token, "News created"));
        }
    }
}
