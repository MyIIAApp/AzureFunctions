using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using NCrontab;

namespace IIABackend
{
    /// <summary>
    /// Get EventPopOver
    /// </summary>
    public static class AutoBirthdayAnniversaryMessage
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="myTimer">Timer</param>
        /// <param name="log">logger</param>
        [FunctionName("AutoBirthdayAnniversaryMessage")]
        public static void Run([TimerTrigger("%DailyMessageSchedule%")] TimerInfo myTimer, ILogger log)
        {
            if (myTimer.IsPastDue)
            {
                log.LogInformation("Timer is running late!");
            }

            var birthdayDetails = Database.GetPhoneNumberForBirthdayAnniversaryMessage("birthday");
            var anniversaryDetails = Database.GetPhoneNumberForBirthdayAnniversaryMessage("anniversary");
            foreach (var p in birthdayDetails)
            {
                try
                {
                    string[] value = { $"{p.Name.Trim()}" };
                    //SmsHelper.NewWhatsappMessage(p.PhoneNumber, string.Empty, "iia_birthday", string.Empty, value, true);
                    //log.LogInformation($"Birthday messsage sent to phone number {p.PhoneNumber} name {p.Name}");
                }
                catch (Exception e)
                {
                    log.LogInformation($"{e.Message} error in phone number {p.PhoneNumber} name {p.Name}");
                }
            }

            foreach (var p in anniversaryDetails)
            {
                try
                {
                    string[] value = { $"{p.Name.Trim()}" };
                    // SmsHelper.NewWhatsappMessage(p.PhoneNumber, string.Empty, "iia_anniversary", string.Empty, value, true);
                    // log.LogInformation($"Anniversary messsage sent to phone number {p.PhoneNumber} name {p.Name}");
                }
                catch (Exception e)
                {
                    log.LogInformation($"{e.Message} error in phone number {p.PhoneNumber} name {p.Name}");
                }
            }

            log.LogInformation($"AutoBirthdayAnniversaryMessage function executed at: {DateTime.Now}");
        }
    }
}
