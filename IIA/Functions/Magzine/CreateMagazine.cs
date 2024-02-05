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
    /// Magazine create function
    /// </summary>
    public static class CreateMagazine
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateMagazine")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create Magazine Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var token = FunctionUtility.ValidateToken(req);
            if (token == null || !token.IsAdmin)
            {
                return new UnauthorizedResult();
            }

            string title = data?.title;
            string magazinePath = data?.magazinePath;
            int creatorAdminId = token.Id;
            string magazineMonth = data?.magazineMonth;
            string magazineYear = data?.magazineYear;
            string coverPhotoPath = data?.coverPhotoPath;
            string opration = data?.opration;

            if (opration == "delete")
            {
                try
                {
                    Database.DeleteMagazine(int.Parse(title));
                    return new OkObjectResult(new BaseResponse(token, "Magazine Deleted Successfully"));
                }
                catch (Exception e)
                {
                    return new BadRequestObjectResult(new BaseResponse(token, e.Message));
                }
            }

            if (string.IsNullOrEmpty(title))
            {
                return new BadRequestObjectResult("Invalid Inputs");
            }

            Database.CreateMagazine(title, magazinePath, creatorAdminId, magazineMonth, magazineYear, coverPhotoPath);
            SendNotification.SendNotifications(title, "A new magazine has been uploaded", "all");
            return new OkObjectResult(new BaseResponse(token, "Magazine created"));
        }
    }
}