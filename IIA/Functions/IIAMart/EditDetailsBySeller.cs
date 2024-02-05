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
    public static class EditDetailsBySeller
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("EditDetailsBySeller")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Edit Details request!");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name, description, photoPath, category, subCategory;
            string price;
            int sellerId;
            bool editOrNew;
            string id;
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                name = data?.name;
                description = data?.description;
                photoPath = data?.photoPath;
                category = data?.category;
                subCategory = data?.subCategory;
                price = data?.price;
                editOrNew = data?.editOrNew;
                sellerId = token.Id;
                id = data?.id;
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Invalid Input");
            }

            Database.EditDetailsBySeller(name, description, photoPath, category, subCategory, float.Parse(price), sellerId, editOrNew, int.Parse(id));
            return new OkObjectResult(new BaseResponse(token, "List updated"));
        }
    }
}
