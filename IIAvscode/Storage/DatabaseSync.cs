using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IIABackend
{
    /// <summary>
    /// Membership Application
    /// </summary>
    public static class DatabaseSync
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("DatabaseSync")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("DatabaseSync");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            try
            {
                HttpClient client = new HttpClient();
                var values = new Dictionary<string, string>
                {
                    { "status", "1" },
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("http://www.iiaonline.in/api/Member/getMembersForPublic", content);
                string responseString = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(responseString);
                List<IIAOldMembershipProfile> list = jsonArray.ToObject<List<IIAOldMembershipProfile>>();
                list.RemoveAll(r => !CheckPhoneNumber(r.Mobile));
                foreach (IIAOldMembershipProfile test in list)
                {
                        UserProfile userProfile = new UserProfile(int.MinValue, test.Mobile.Substring(0, 10), test.MemberId, int.Parse(test.Admissionfee.Contains(".") ? test.Admissionfee.Substring(0, test.Admissionfee.IndexOf(".")) : test.Admissionfee), int.Parse(test.Annualsubscription.Contains(".") ? test.Annualsubscription.Substring(0, test.Annualsubscription.IndexOf(".")) : test.Annualsubscription), DateTime.ParseExact(test.Expirydate, "dd/MM/yyyy", null).Year, DateTime.ParseExact(test.MemberJoinDate, "dd/MM/yyyy", null), DateTime.ParseExact(test.Lastrenewaldate, "dd/MM/yyyy", null), DateTime.ParseExact(test.Expirydate, "dd/MM/yyyy", null).Year.ToString(), GetprofileStatus(DateTime.ParseExact(test.Expirydate, "dd/MM/yyyy", null)), int.Parse(test.ChapterId), test.ChapterName, test.UnitName, test.GSTIN, string.Empty, test.IndustryStatus, test.Address, test.District, test.City, test.State, "India", test.Pincode, test.WebsiteUrl, test.ProductCategory, test.ProductSubCategory, test.MajorProducts, test.AnnualTurnOver, test.EnterpriseType, test.Exporter, test.Classification, test.Owner.Trim().Contains(" ") ? test.Owner.Trim().Split(" ")[0] : test.Owner.Trim(), test.Owner.Trim().Contains(" ") ? test.Owner.Trim().Substring(test.Owner.Trim().IndexOf(" ")) : string.Empty, test.Email, string.Empty, string.Empty, string.Empty, -2, -2, DateTime.Now, DateTime.Now);
                        int memberJoinYear = DateTime.ParseExact(test.MemberJoinDate, "dd/MM/yyyy", null).Year;
                        int memberExpiryYear = DateTime.ParseExact(test.Expirydate, "dd/MM/yyyy", null).Year;
                        if (memberExpiryYear != 1900)
                        {
                            string expiryYears = (userProfile.MembershipJoinDate.Month > 3 ? userProfile.MembershipJoinDate.AddYears(1).Year : userProfile.MembershipJoinDate.Year).ToString();
                            for (int i = int.Parse(expiryYears) + 1; i <= memberExpiryYear; i++)
                            {
                                expiryYears = expiryYears + "," + i.ToString();
                            }

                            userProfile.MembershipExpiryYears = expiryYears;
                        }

                        Database.InsertUpdateUserProfile(userProfile, -2);
                    }

                return new OkObjectResult("Sync Completed");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new BaseResponse(token, e.Message));
            }
        }

        /// <summary>
        /// Checks if Mobile is valid
        /// </summary>
        /// <param name="mobile">mobile</param>
        /// <returns>boolean</returns>
        private static bool CheckPhoneNumber(string mobile)
        {
            Match match = Regex.Match(mobile, @"\d{10}");
            return match.Success;
        }

        /// <summary>
        /// GetprofileStatus
        /// </summary>
        /// <param name="expirydate">expirydate</param>
        /// <returns>profile status</returns>
        private static int GetprofileStatus(DateTime expirydate)
        {
            if (DateTime.Now.Month < 4)
            {
                if (expirydate.Year - DateTime.Now.Year >= 0)
                {
                    return (int)UserProfileStatusEnum.UserProfileStatus.ActiveMembership;
                }
                else if (expirydate.Year - DateTime.Now.Year == -1)
                {
                    return (int)UserProfileStatusEnum.UserProfileStatus.ActiveGraceMembership;
                }
                else
                {
                    return (int)UserProfileStatusEnum.UserProfileStatus.ExpiredMembership;
                }
            }
            else
            {
                if (expirydate.Year - DateTime.Now.Year > 0)
                {
                    return (int)UserProfileStatusEnum.UserProfileStatus.ActiveMembership;
                }
                else if (expirydate.Year - DateTime.Now.Year == 0)
                {
                    return (int)UserProfileStatusEnum.UserProfileStatus.ActiveGraceMembership;
                }
                else
                {
                    return (int)UserProfileStatusEnum.UserProfileStatus.ExpiredMembership;
                }
            }
        }
    }
}