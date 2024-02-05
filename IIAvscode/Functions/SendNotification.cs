using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
namespace IIABackend
{
    /// <summary>
    /// News Create function
    /// </summary>
    public static class SendNotification
    {
        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="req">HTTP request</param>
        /// <returns>Tokenobject</returns

        public static async void sendNotification(string title,string text,string user){
            string resultContent = string.Empty;
            using (var client = new HttpClient())
            {
                Dictionary<string, dynamic> headers = new Dictionary<string, dynamic>();
                
                List<string> users=new List<string>();
                if(user=="all"){
                    users.Add("Active Users");
                    headers.Add("included_segments",users);
                }
                else{
                    users.Add(user);
                    headers.Add("include_external_user_ids",users);
                }
                
                headers.Add("app_id",Environment.GetEnvironmentVariable("APP_KEY"));
                headers.Add("contents",new
                {
                    en= text
                });
                headers.Add("headings",new
                {
                    en= title
                });
                headers.Add("chrome_web_image","https://i.imgur.com/7NrqrFX.jpg");
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Environment.GetEnvironmentVariable("APIKEY"));
                string head = JsonConvert.SerializeObject(headers);
                var content = new StringContent(head, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
                resultContent = await result.Content.ReadAsStringAsync();
            }
        }
        
    }
}
