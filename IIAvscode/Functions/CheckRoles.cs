// using System;
// using System.IO;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Azure.WebJobs;
// using Microsoft.Azure.WebJobs.Extensions.Http;
// using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;

// namespace IIABackend
// {
//    /// <summary>
//    /// Check Admin Roles function
//    /// </summary>
//    public static class CheckRoles
//    {
//        /// <summary>
//        /// Function
//        /// </summary>
//        /// <param name="req">request body</param>
//        /// <param name="log">logger</param>
//        /// <returns>HTTP result</returns>
//        [FunctionName("CheckRoles")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            log.LogInformation("Admin Roles request!");
//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            var token = FunctionUtility.ValidateToken(req);
//            if (token == null)
//            {
//                return new UnauthorizedResult();
//            }
//            int id = token.Id;
//            //var rolelist = Database.CheckRoles(id);
//            return new OkObjectResult(new RolesResponse(rolelist, token, "Admin Roles Request  Success"));
//        }
//    }
// }
