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
    /// Gets Enquiries per seller
    /// </summary>
    public class GetB2BAdminEnquiries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetB2BAdminEnquiries"/> class.
        /// </summary>
        /// /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("GetB2BAdminEnquiries")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Get B2B Admin Enquiries Request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            Dictionary<string, List<B2BAdminEnquiry>> b2bAdminEnquiries = new Dictionary<string, List<B2BAdminEnquiry>>();
            b2bAdminEnquiries.Add("adminEnquiryList", Database.GetB2BAdminEnquiries());
            return new OkObjectResult(b2bAdminEnquiries);
        }
    }
}