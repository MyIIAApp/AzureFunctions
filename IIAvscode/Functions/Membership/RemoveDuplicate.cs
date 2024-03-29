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
    /// Remove Duplicate From Table
    /// <summary>
    public class RemoveDuplicate
{
        /// <summary>
        /// Function
        /// </summary>
[FunctionName("RemoveDuplicate")]
public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route =null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Remove Duplicates from Table");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
    
            Database.RemoveDuplicate();
            return new OkObjectResult("Query Executed Successfully");
        }
    }
}