using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Nancy.Json;

namespace IIABackend
{
    /// <summary>
    /// index of type of message
    /// </summary>
    public enum TypeOfMessage
    {
        /// <summary>
        /// Membership Approval message
        /// </summary>
        MembershipApproval,

        /// <summary>
        /// Payment message
        /// </summary>
        Payment,

        /// <summary>
        /// Ticket Reply  message
        /// </summary>
        Ticket,

        /// <summary>
        /// Enquiry message
        /// </summary>
        Enquiry,
    }

    /// <summary>
    /// Sms helper for sms and whatsapp messages
    /// </summary>
    public static class SmsHelper
    {
        /// <summary>
        /// Different SMS Sending function
        /// </summary>
        /// <param name="phoneNumber">phoneNumber</param>
        /// <param name="msg">msg</param>
        /// <param name="idNum">id number</param>
        /// <returns>Token</returns>
        public static string SendSMS2(string phoneNumber, string msg, int idNum)
        {
            string url = string.Empty;
            string[] tId = { "1707165219847147843", "1707165219771409485", "1707165600317111649", "1707165600319173019" };
            string[] value = { $"{msg}" };
            if (idNum == 0)
            {
                url = "https://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=" + Environment.GetEnvironmentVariable("APIKey") + "&senderid=" + Environment.GetEnvironmentVariable("senderid") + "&channel=" + Environment.GetEnvironmentVariable("channel") + "&DCS=" + Environment.GetEnvironmentVariable("DCS") + "&flashsms=" + Environment.GetEnvironmentVariable("flashsms") + "&number=" + phoneNumber + "&text=Your profile has been approved. You can pay online through My IIA App or to your local IIA representative to start membership." + msg + "&route=" + Environment.GetEnvironmentVariable("route") + "&EntityId=" + Environment.GetEnvironmentVariable("EntityId") + "&dlttemplateid=" + tId[idNum];
                //SmsHelper.NewWhatsappMessage(phoneNumber, string.Empty, "iia_membershipapproval", string.Empty, value, false);
            }
            else if (idNum == 1)
            {
                url = "https://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=" + Environment.GetEnvironmentVariable("APIKey") + "&senderid=" + Environment.GetEnvironmentVariable("senderid") + "&channel=" + Environment.GetEnvironmentVariable("channel") + "&DCS=" + Environment.GetEnvironmentVariable("DCS") + "&flashsms=" + Environment.GetEnvironmentVariable("flashsms") + "&number=" + phoneNumber + "&text=Thank your payment of " + msg + " to IIA. You can now download your invoice from Payment section in My IIA App." + "&route=" + Environment.GetEnvironmentVariable("route") + "&EntityId=" + Environment.GetEnvironmentVariable("EntityId") + "&dlttemplateid=" + tId[idNum];
                //SmsHelper.NewWhatsappMessage(phoneNumber, string.Empty, "iia_payments", string.Empty, value, true);
            }
            else if (idNum == 2)
            {
                url = "https://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=" + Environment.GetEnvironmentVariable("APIKey") + "&senderid=" + Environment.GetEnvironmentVariable("senderid") + "&channel=" + Environment.GetEnvironmentVariable("channel") + "&DCS=" + Environment.GetEnvironmentVariable("DCS") + "&flashsms=" + Environment.GetEnvironmentVariable("flashsms") + "&number=" + phoneNumber + "&text=You have received a new reply from IIA on the ticket " + msg + ". Please check Issues and Problems section in My IIA App for details." + "&route=" + Environment.GetEnvironmentVariable("route") + "&EntityId=" + Environment.GetEnvironmentVariable("EntityId") + "&dlttemplateid=" + tId[idNum];
                //SmsHelper.NewWhatsappMessage(phoneNumber, string.Empty, "iia_tickets", string.Empty, value, true);
            }
            else
            {
                url = "https://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=" + Environment.GetEnvironmentVariable("APIKey") + "&senderid=" + Environment.GetEnvironmentVariable("senderid") + "&channel=" + Environment.GetEnvironmentVariable("channel") + "&DCS=" + Environment.GetEnvironmentVariable("DCS") + "&flashsms=" + Environment.GetEnvironmentVariable("flashsms") + "&number=" + phoneNumber + "&text=You have received a new enquiry on your product.Please check Manage IIA Mart section in My IIA App for details." + msg + "&route=" + Environment.GetEnvironmentVariable("route") + "&EntityId=" + Environment.GetEnvironmentVariable("EntityId") + "&dlttemplateid=" + tId[idNum];
                //SmsHelper.NewWhatsappMessage(phoneNumber, string.Empty, "iia_enquiries", string.Empty, value, false);
            }

            string strResponce = JWTTokenBuilder.GetResponse(url);
            return strResponce;
        }

        /// <summary>
        /// Custom Whatsapp Message
        /// </summary>
        /// <param name="number">Receiver</param>
        /// <param name="type">messsage type</param>
        /// <param name="message">message</param>
        /// <param name="phoneId">id</param>
        /// <param name="value">Variable value of custom messsage</param>
        /// <param name="isVar">Is messsage a variable</param>
        public static void NewWhatsappMessage(string number, string type, string message, string phoneId, string[] value, bool isVar)
        {
            HttpClient client = new HttpClient();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Console.WriteLine("Wa ChatBot Testing");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ft-wabot.azurewebsites.net/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            var result = string.Empty;
            TemplateMessage values = new TemplateMessage() { To = "91" + number, Type = "template", Mssg = message, Phone_id = "112055028222439", Value = value, IsVar = isVar };
            var json = serializer.Serialize(values);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Send Whatsapp message to chairmen
        /// </summary><param name="ticketNumber">ticket Number</param>
        /// <param name="committeeId">committe id</param>
        /// <param name="log">logger</param>
        public static void SendMessageToChairmen(int ticketNumber, int committeeId, ILogger log)
        {
            try
            {
                var chairmanDetails = Database.GetPhoneNumberOfAlottedChairmen(committeeId);
                string committeeName = Database.GetCommitteeOfAlottedChairmen(committeeId);
                string[] value = { $"{ticketNumber}", $"{committeeName}" };
                foreach (var p in chairmanDetails)
                {
                    try
                    {
                        //SmsHelper.NewWhatsappMessage(p.PhoneNumber, string.Empty, "iia_ticket_reply_for_office", string.Empty, value, true);
                        //log.LogInformation($"messsage sent to phone number {p.PhoneNumber} name {p.Name}");
                    }
                    catch (Exception e)
                    {
                        log.LogInformation($"{e.Message} error in phone number {p.PhoneNumber} name {p.Name}");
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
        }

        /// <summary>
        /// WhatsApp Message Template for json
        /// </summary>
        public class TemplateMessage
        {
            /// <summary>
            /// Gets or sets receiver number
            /// </summary>
            public string To { get; set; }

            /// <summary>
            /// Gets or sets Type of messsage
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets message category
            /// </summary>
            public string Mssg { get; set; }

            /// <summary>
            /// Gets or sets id number of sender
            /// </summary>
            public string Phone_id { get; set; }

            /// <summary>
            /// Gets or sets variable value in message
            /// </summary>
            public string[] Value { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether variable value is present
            /// </summary>
            public bool IsVar { get; set; }
        }
    }
}