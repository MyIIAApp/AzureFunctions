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
    /// Create Invoice For Membership Payment
    /// </summary>
    public static class CreateInvoiceForMembershipPayment
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateInvoiceForMembershipPayment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateInvoiceForMembershipPayment Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            /*if (token == null)
            {
                return new UnauthorizedResult();
            }

            if (!token.IsAdmin)
            {
                return new BadRequestObjectResult("Not a Admin");
            }*/

            string userId;
            string paymentMode;
            string paymentMade;
            string chequeNumber;
            DateTime curr = DateTime.Now;
            string invoiceNumber = "IIA-" + curr.ToString("yyyyMMddHHmmss");
            try
            {
                userId = data?.userId;
                paymentMode = data?.paymentMode;
                paymentMade = data?.paymentMade;
                chequeNumber = data?.chequeNumber;
                Dictionary<string, dynamic> invoicePath = new Dictionary<string, dynamic>();
                UserProfile userProfile = Database.GetUserProfile(int.Parse(userId), null, null);
                PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile.ProfileStatus, userProfile.AnnualTurnOver, userProfile.GSTIN);
                try
                {
                    Database.StorePayment(int.Parse(userId), token.Id, paymentDetail.MembershipFee + paymentDetail.AdmissionFee, 9, paymentDetail.Sgst == 0 ? 0 : 9, paymentDetail.Igst == 0 ? 0 : 9, paymentDetail.Igst, paymentDetail.Sgst, paymentDetail.Cgst, "Membership Renewal Fees", paymentMode, chequeNumber == null ? chequeNumber.Split("$%#")[0] : string.Empty, invoiceNumber, paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst, curr);
                    invoicePath.Add("paymentSuccess", true);
                }
                catch (Exception ex)
                {
                    invoicePath.Add("paymentSuccess", false);
                    invoicePath.Add("errorMessage", "Payment Failed: " + ex.Message);
                    return new OkObjectResult(invoicePath);
                }

                try
                {
                    string url = CreatePDF.CreateInvoice(invoiceNumber, DateTime.Now.ToString(), "Membership Renewal Fees", (paymentDetail.MembershipFee + paymentDetail.AdmissionFee).ToString(), "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "9", paymentDetail.Cgst.ToString(), paymentDetail.Sgst.ToString(), paymentDetail.Igst.ToString(), (paymentDetail.MembershipFee + paymentDetail.AdmissionFee).ToString(), (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString(), paymentMade, paymentMode, userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName);
                    Database.UpdateInvoicePath(int.Parse(userId), token.Id, url, invoiceNumber);
                    invoicePath.Add("invoiceGenerated", true);
                    invoicePath.Add("InvoicePath", url);
                    return new OkObjectResult(invoicePath);
                }
                catch (Exception ex)
                {
                    invoicePath.Add("invoiceGenerated", false);
                    invoicePath.Add("errorMessage", "Invoice Generation Failed: " + ex.Message);
                    return new OkObjectResult(invoicePath);
                }
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Bad Request");
            }
        }
    }
}