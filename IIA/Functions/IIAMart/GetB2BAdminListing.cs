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
    /// B2B Admin Listing
    /// </summary>
    public class GetB2BAdminListing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetB2BAdminListings"/> class.
        /// </summary>
        /// /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetB2BAdminListing")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Get B2B Admin Listing Request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            Dictionary<string, List<B2BAdminListing>> b2bAdminListing = new Dictionary<string, List<B2BAdminListing>>();
            b2bAdminListing.Add("adminListingsList", Database.GetB2BAdminListing());
            return new OkObjectResult(b2bAdminListing);
        }
    }
}
