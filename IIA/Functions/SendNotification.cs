using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Send Notification Function
    /// </summary>
    public static class SendNotification
    {
        /// <summary>
        /// Send Notification
        /// </summary>
        /// <param name="title">Title of notification</param>
        /// <param name="text">Text of the notification</param>
        /// <param name="user">Users to receive notifications</param>
        public static async void SendNotifications(string title, string text, string user)
        {
            string resultContent;
            using var client = new HttpClient();
            Dictionary<string, dynamic> headers = new Dictionary<string, dynamic>();

            List<string> users = new List<string>();
            if (user == "all")
            {
                users.Add("Active Users");
                headers.Add("included_segments", users);
            }
            else
            {
                users.Add(user);
                headers.Add("include_external_user_ids", users);
            }

            headers.Add("app_id", Environment.GetEnvironmentVariable("oneSignalAPPId"));
            headers.Add("contents", new
            {
                en = text,
            });
            headers.Add("headings", new
            {
                en = title,
            });
            headers.Add("chrome_web_image", "https://i.imgur.com/7NrqrFX.jpg");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + Environment.GetEnvironmentVariable("oneSignalAPIKey"));
            string head = JsonConvert.SerializeObject(headers);
            var content = new StringContent(head, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
            resultContent = await result.Content.ReadAsStringAsync();

            string iOSresultContent;
            using var iOSclient = new HttpClient();
            Dictionary<string, dynamic> iOSheaders = new Dictionary<string, dynamic>();

            List<string> iOSusers = new List<string>();
            if (user == "all")
            {
                iOSusers.Add("Active Users");
                iOSheaders.Add("included_segments", users);
            }
            else
            {
                iOSusers.Add(user);
                iOSheaders.Add("include_external_user_ids", users);
            }

            iOSheaders.Add("app_id", Environment.GetEnvironmentVariable("IOSoneSignalAPPId"));
            iOSheaders.Add("contents", new
            {
                en = text,
            });
            iOSheaders.Add("headings", new
            {
                en = title,
            });
            iOSheaders.Add("chrome_web_image", "https://i.imgur.com/7NrqrFX.jpg");
            iOSclient.DefaultRequestHeaders.Add("Authorization", "Basic " + Environment.GetEnvironmentVariable("IOSoneSignalAPIKey"));
            string iOShead = JsonConvert.SerializeObject(headers);
            var iOScontent = new StringContent(head, Encoding.UTF8, "application/json");
            var iOSresult = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
            iOSresultContent = await result.Content.ReadAsStringAsync();
        }
    }
}