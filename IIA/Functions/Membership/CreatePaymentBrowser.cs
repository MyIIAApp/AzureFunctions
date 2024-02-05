using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace IIABackend
{
    /// <summary>
    /// Create Payment URL
    /// </summary>
    public static class CreatePaymentBrowser
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreatePaymentBrowser")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateInvoiceForMembershipPayment Request!");
            var token = FunctionUtility.ValidateToken(req.Query["token"]);
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
                number = Database.StorePayment(userId, -2, paymentDetail.MembershipFee + paymentDetail.AdmissionFee, paymentDetail.Cgst == 0 ? 0 : 9, paymentDetail.Sgst == 0 ? 0 : 9, paymentDetail.Igst == 0 ? 0 : 18, paymentDetail.Igst, paymentDetail.Sgst, paymentDetail.Cgst, "Membership Renewal Fees", "online", string.Empty, txnId, paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst, curr, -2, string.Empty, userProfile.MembershipCurrentExpiryYear == 0 ? curr.Month < 4 ? curr.Year : curr.Year + 1 : userProfile.MembershipCurrentExpiryYear + 1);
                Database.UpdateFullInvoiceNum("I-" + "2324" + "-" + (int.Parse(number) - 8512).ToString(), number);
            }
            catch (Exception)
            {
                resultUrl.Add("url", string.Empty);
                resultUrl.Add("errorMessage", "Payment Failed.");
                return new OkObjectResult(resultUrl);
            }

            string payURawData = Environment.GetEnvironmentVariable("PaymentGatewayKey") + "|" + txnId + "|" + (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst).ToString() + ".00|membershipRenewalFees|" + FunctionUtility.StringEncoder(userProfile.UnitName) + "|" + FunctionUtility.StringEncoder(userProfile.Email) + "|" + FunctionUtility.StringEncoder(userProfile.FirstName) + "|" + FunctionUtility.StringEncoder(userProfile.Id.ToString()) + "|" + FunctionUtility.StringEncoder(number) + "|" + FunctionUtility.StringEncoder(userProfile.District) + "|" + FunctionUtility.StringEncoder(userProfile.State) + "||||||" + Environment.GetEnvironmentVariable("PaymentGatewaySalt");
            string hash = FunctionUtility.ComputeSha256Hash(payURawData);
            /*HttpClient client = new HttpClient();
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
                };*/

            string returnHtmlString = "<ion-view>   <ion-content >      <div class=\"full-width\">       <h1>Please Wait.., Your transaction is processing...</h1>       <img src=\"../img/loader.gif\" />     </div>      <form name=\"sendParam\" id=\"sendParam\" method=\"post\" action=\"https://secure.payu.in/_payment\">       <input type=\"hidden\" name=\"key\"  id=\"key\" value=\"" + Environment.GetEnvironmentVariable("PaymentGatewayKey") + "\">       <input type=\"hidden\" name=\"productinfo\" id=\"productinfo\" value=\"membershipRenewalFees\">       <input type=\"hidden\" name=\"hash\" id=\"hash\" value=\"" + hash + "\">       <input type=\"hidden\" name=\"txnid\" id=\"txnid\" value=\"" + txnId + "\">       <input  type=\"hidden\" name=\"amount\" id=\"amount\" value=\"" + (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst).ToString() + ".00" + "\">       <input  type=\"hidden\" name=\"firstname\" id=\"firstname\" value=\"" + FunctionUtility.StringEncoder(userProfile.UnitName) + "\">       <input  type=\"hidden\" name=\"email\" id=\"email\" value=\"" + FunctionUtility.StringEncoder(userProfile.Email) + "\">       <input  name=\"phone\"  type=\"hidden\" id=\"phone\" value=\"" + FunctionUtility.StringEncoder(userProfile.PhoneNumber) + "\">       <input  type=\"hidden\" name=\"surl\" id=\"surl\" value=\"" + Environment.GetEnvironmentVariable("MakePaymentURL") + "\" size=\"64\">       <input  type=\"hidden\" name=\"furl\" id=\"furl\" value=\"" + Environment.GetEnvironmentVariable("MakePaymentURL") + "\" size=\"64\">       <input  type=\"hidden\" name=\"service_provider\" id=\"service_provider\" value=\"\">       <input  type=\"hidden\" name=\"udf1\" id=\"udf1\" value=\"" + FunctionUtility.StringEncoder(userProfile.FirstName) + "\">       <input  type=\"hidden\" name=\"udf2\" id=\"udf2\" value=\"" + FunctionUtility.StringEncoder(userProfile.Id.ToString()) + "\">       <input  type=\"hidden\" name=\"udf3\" id=\"udf3\" value=\"" + FunctionUtility.StringEncoder(number) + "\">       <input  type=\"hidden\" name=\"udf4\" id=\"udf4\" value=\"" + FunctionUtility.StringEncoder(userProfile.District) + "\">       <input  type=\"hidden\" name=\"udf5\" id=\"udf5\" value=\"" + FunctionUtility.StringEncoder(userProfile.State) + "\">        <input type=\"submit\" value=\"enter\" style=\"position: absolute; left: -9999px\"/>     </form >     <style type=\"text/css\">       .full-width { width: 100%; float: left; text-align: center;  }       h1 { font-size: 35px; width: 50%; margin-top: 30px; margin: 100px auto 0 auto;  }       img { margin-top: 30px;  }     </style>         <script type=\"text/javascript\">           function submitForm() {     document.sendParam.submit();  }   submitForm();         </script>     <script>       //document.sendParam.submit();     </script>   </ion-content> </ion-view>";
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = returnHtmlString,
            };
        }
    }
}