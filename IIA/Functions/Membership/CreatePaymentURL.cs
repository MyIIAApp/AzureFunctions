using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
    /// Create Payment URL
    /// </summary>
    public static class CreatePaymentURL
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreatePaymentURL")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateInvoiceForMembershipPayment Request!");
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            int userId = token.Id;
            UserProfile userProfile;
            PaymentDetail paymentDetail;
            try
            {
                userProfile = Database.GetUserProfile(userId, null, null);
                paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            string number;
            Random generator = new Random();
            int r = generator.Next(1000, 10000);
            string txnId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() + r.ToString();
            DateTime curr = DateTime.Now;
            Dictionary<string, string> resultUrl = new Dictionary<string, string>();
            try
            {
                number = Database.StorePayment(userId, -2, paymentDetail.MembershipFee + paymentDetail.AdmissionFee, paymentDetail.Cgst == 0 ? 0 : 9, paymentDetail.Sgst == 0 ? 0 : 9, paymentDetail.Igst == 0 ? 0 : 18, paymentDetail.Igst, paymentDetail.Sgst, paymentDetail.Cgst, "Membership Renewal Fees", "online", string.Empty, txnId, paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst, curr, -2, string.Empty, userProfile.MembershipCurrentExpiryYear + 1);
            }
            catch (Exception)
            {
                resultUrl.Add("url", string.Empty);
                resultUrl.Add("errorMessage", "Payment Failed.");
                return new OkObjectResult(resultUrl);
            }

            string payURawData = Environment.GetEnvironmentVariable("PaymentGatewayKey") + "|" + txnId + "|" + (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst).ToString() + ".00|membershipRenewalFees|" + FunctionUtility.StringEncoder(userProfile.UnitName) + "|" + FunctionUtility.StringEncoder(userProfile.Email) + "|" + FunctionUtility.StringEncoder(userProfile.FirstName) + "|" + FunctionUtility.StringEncoder(userProfile.Id.ToString()) + "|" + FunctionUtility.StringEncoder(number) + "|" + FunctionUtility.StringEncoder(userProfile.District) + "|" + FunctionUtility.StringEncoder(userProfile.State) + "||||||" + Environment.GetEnvironmentVariable("PaymentGatewaySalt");
            string hash = FunctionUtility.ComputeSha256Hash(payURawData);
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
                {
                    { "key", Environment.GetEnvironmentVariable("PaymentGatewayKey") },
                    { "productinfo", "membershipRenewalFees" },
                    { "hash", hash },
                    { "txnid", txnId },
                    { "amount", (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst).ToString() + ".00" },
                    { "firstname", FunctionUtility.StringEncoder(userProfile.UnitName) },
                    { "email", FunctionUtility.StringEncoder(userProfile.Email) },
                    { "phone", FunctionUtility.StringEncoder(userProfile.PhoneNumber) },
                    { "surl", Environment.GetEnvironmentVariable("MakePaymentURL") },
                    { "furl", Environment.GetEnvironmentVariable("MakePaymentURL") },
                    { "service_provider", string.Empty },
                    { "udf1", FunctionUtility.StringEncoder(userProfile.FirstName) },
                    { "udf2", FunctionUtility.StringEncoder(userProfile.Id.ToString()) },
                    { "udf3", FunctionUtility.StringEncoder(number) },
                    { "udf4", FunctionUtility.StringEncoder(userProfile.District) },
                    { "udf5", FunctionUtility.StringEncoder(userProfile.State) },
                };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://test.payu.in/_payment", content);
            string temp;
            string responseString = await response.Content.ReadAsStringAsync();
            try
            {
                temp = responseString.Split(new[] { "txtid=" }, StringSplitOptions.None)[1];
            }
            catch (Exception)
            {
                resultUrl.Add("url", string.Empty);
                resultUrl.Add("errorMessage", responseString);
                return new OkObjectResult(resultUrl);
            }

            string txtId = string.Empty;
            foreach (char i in temp)
            {
                if (!i.Equals('"'))
                {
                    txtId += i;
                }
                else
                {
                    break;
                }
            }

            string url = "https://test.payu.in/_payment_options?mihpayid=" + txtId + "&userToken=";
            resultUrl.Add("url", url);

            return new OkObjectResult(resultUrl);
        }
    }
}