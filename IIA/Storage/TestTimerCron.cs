using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Membership Application
    /// </summary>
    public static class TestTimerCron
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="myTimer"> timer</param>
        /// <param name="log">logger</param>
        [FunctionName("TestTimerCron")]
        public static async void Run(
            [TimerTrigger("0 0 0 * * *")] TimerInfo myTimer,
            ILogger log)
        {
            log.LogInformation("TimerCronsStarted");
            List<List<dynamic>> paymentList = new List<List<dynamic>>();
            paymentList = Database.GetPaymentDetail();
            DateTime curr = DateTime.Now;
            int expiryYear = curr.Year;
            foreach (List<dynamic> payment in paymentList)
            {
                HttpClient client = new HttpClient();
                var values = new Dictionary<string, string>
                {
                    { "key", Environment.GetEnvironmentVariable("PaymentGatewayKey") },
                    { "command", "verify_payment" },
                    { "var1", payment[2].ToString() },
                    { "hash", FunctionUtility.ComputeSha256Hash(Environment.GetEnvironmentVariable("PaymentGatewayKey") + "|verify_payment|" + payment[2].ToString() + "|" + Environment.GetEnvironmentVariable("PaymentGatewaySalt")) },
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://info.payu.in/merchant/postservice.php?form=2", content);
                string responseString = await response.Content.ReadAsStringAsync();
                dynamic verifyPaymentData = JsonConvert.DeserializeObject<dynamic>(responseString);
                if (verifyPaymentData?.status == 1 && verifyPaymentData?.transaction_details?[payment[2].ToString()].status == "success")
                {
                    if (payment[3] != 1)
                    {
                        string mihPayId = verifyPaymentData?.transaction_details?[payment[2].ToString()]?.mihpayid;
                        Database.UpdatePaymentStatus(payment[0], 1, mihPayId);
                        UserProfile userProfile = Database.GetUserProfile(payment[1], null, null);
                        PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile);
                        string membershipId = userProfile.MembershipId != null && userProfile.MembershipId != string.Empty ? userProfile.MembershipId : (20000 + userProfile.Id).ToString();
                        DateTime membershipJoinDate = userProfile.MembershipId != null && userProfile.MembershipId != string.Empty ? userProfile.MembershipJoinDate : curr;
                        expiryYear = userProfile.MembershipCurrentExpiryYear != 0 ? userProfile.MembershipCurrentExpiryYear + 1 : curr.Month < 4 ? (curr.Year + 1) : curr.Year;
                        string membershipExpiryYears = userProfile.MembershipExpiryYears != null && userProfile.MembershipExpiryYears != string.Empty ? userProfile.MembershipExpiryYears + "," + expiryYear.ToString() : expiryYear.ToString();
                        UserProfile updatedUserProfile = new UserProfile(userProfile.Id, userProfile.PhoneNumber, membershipId, userProfile.MembershipAdmissionfee != 0 ? userProfile.MembershipAdmissionfee : paymentDetail.AdmissionFee, paymentDetail.MembershipFee, expiryYear, membershipJoinDate, curr, membershipExpiryYears, 5, userProfile.ChapterId, userProfile.ChapterName, userProfile.UnitName, userProfile.GSTIN, userProfile.GSTcertpath, userProfile.IndustryStatus, userProfile.Address, userProfile.District, userProfile.City, userProfile.State, userProfile.Country, userProfile.Pincode, userProfile.WebsiteUrl, userProfile.ProductCategory, userProfile.ProductSubCategory, userProfile.MajorProducts, userProfile.AnnualTurnOver, userProfile.EnterpriseType, userProfile.Exporter, userProfile.Classification, userProfile.FirstName, userProfile.LastName, userProfile.Email, userProfile.DateOfBirth, userProfile.DateOfMarriage, userProfile.ProfileImagePath, userProfile.FinancialProofPath, userProfile.SignaturePath, userProfile.CreatedBy, -2, userProfile.CreatedDate, curr);
                        Database.InsertUpdateUserProfile(updatedUserProfile, userProfile.UpdatedBy);
                        string sourceGst = Database.GetSourceGST(int.Parse(payment[0]));
                        string url = CreatePDF.CreateInvoice("I-" + curr.ToString("yyyy") + "-" + payment[0], DateTime.Now.ToString(), paymentDetail.AdmissionFee == 0 ? "Membership Renewal Fees" : "Membership Fees", "999599", paymentDetail.Cgst == 0 ? "0" : "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "18", paymentDetail.Cgst.ToString(), paymentDetail.Sgst.ToString(), paymentDetail.Igst.ToString(), paymentDetail.MembershipFee.ToString(), (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString(), "paid", "online payment", userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, updatedUserProfile.MembershipId, userProfile.Address, ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN) != string.Empty ? ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN)[3..] : string.Empty, userProfile.GSTIN, "Membership Renewal Fees", curr, (expiryYear - 1).ToString(), expiryYear.ToString(), string.Empty, paymentDetail.AdmissionFee == 0 ? string.Empty : paymentDetail.AdmissionFee.ToString(), sourceGst);
                        Database.UpdateInvoicePath(payment[1], -2, url, payment[0].ToString());
                    }
                    else
                    {
                        UserProfile userProfile = Database.GetUserProfile(payment[1], null, null);
                        PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile);
                        expiryYear = userProfile.MembershipCurrentExpiryYear;
                        string sourceGst = Database.GetSourceGST(int.Parse(payment[0]));
                        string url = CreatePDF.CreateInvoice("I-" + curr.ToString("yyyy") + "-" + payment[0], DateTime.Now.ToString(),  paymentDetail.AdmissionFee == 0 ? "Membership Renewal Fees" : "Membership Fees", "999599", paymentDetail.Cgst == 0 ? "0" : "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "18", paymentDetail.Cgst.ToString(), paymentDetail.Sgst.ToString(), paymentDetail.Igst.ToString(), paymentDetail.MembershipFee.ToString(), (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString(), "paid", "online payment", userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, userProfile.MembershipId, userProfile.Address, ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN) != string.Empty ? ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN)[3..] : string.Empty, userProfile.GSTIN, "Membership Renewal Fees", curr, (expiryYear - 1).ToString(), expiryYear.ToString(), string.Empty, paymentDetail.AdmissionFee == 0 ? string.Empty : paymentDetail.AdmissionFee.ToString(), sourceGst);
                        Database.UpdateInvoicePath(payment[1], -2, url, payment[0].ToString());
                    }
                }
                else if (verifyPaymentData?.status == 1 && verifyPaymentData?.transaction_details?[payment[2].ToString()].status == "failure")
                {
                    string mihPayId = verifyPaymentData?.transaction_details?[payment[2].ToString()]?.mihpayid;
                    Database.UpdatePaymentStatus(payment[0], 0, mihPayId);
                }

                log.LogInformation("TimerCronsSuccess");
            }
        }
    }
}