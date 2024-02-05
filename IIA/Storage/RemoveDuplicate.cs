using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

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
        /// <param name="myTimer"> timer</param>
        /// <param name="log">logger</param>
        [FunctionName("RemoveDuplicate")]
        public static void Run(
            [TimerTrigger("0 0 1 * * *")] TimerInfo myTimer,
            ILogger log)
        {
/*            log.LogInformation("Remove Duplicates from Table");
            Database.RemoveDuplicate();
            log.LogInformation("Query Executed Successfully");*/
        }
    }
}