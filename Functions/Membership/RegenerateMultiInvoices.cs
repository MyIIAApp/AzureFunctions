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
    /// Create Multiple Invoice For Membership Payment
    /// </summary>
    public static class RegenerateMultiInvoices
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("RegenerateMultiInvoices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateMultipleInvoiceForMembershipPayment Request!");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            int startId = data?.startId;
            int endId = data?.endId;
            List<string> invoices;
            invoices = Database.GetInvoiceList(startId, endId);
            foreach (string invoice in invoices)
            {
                string operation = "update";
                string invoiceNumber = invoice;
                DateTime curr = DateTime.Now;
                List<dynamic> payments;
                Dictionary<string, string> result = new Dictionary<string, string>();
                try
                {
                    payments = Database.GetPaymentDetailsForExcel(invoiceNumber, operation);
                    if (payments.Count == 0)
                    {
                        if (operation == "update")
                        {
                            result.Add("message", "Invalid Invoice Number");
                            return new OkObjectResult(result);
                        }
                        else if (operation == "delete")
                        {
                            result.Add("message", "Invoice Deleted Successfully");
                        }
                    }
                }
                catch (Exception e)
                {
                    log.LogInformation(e.Message);
                    result.Add("message", "Invalid Invoice Number");
                    return new OkObjectResult(result);
                }

                foreach (dynamic pay in payments)
                {
                    try
                    {
                        UserProfile userProfile = Database.GetUserProfile(pay["UserId"], null, null);

                        try
                        {
                            string url = CreatePDF.CreateInvoice(invoiceNumber, pay["CreateDateTimeStamp"], pay["PaymentReason"], "999599", pay["CGSTPercent"], pay["SGSTPercent"], pay["IGSTPercent"], pay["CGSTValue"], pay["SGSTValue"], pay["IGSTValue"], userProfile.MembershipCurrentExpiryYear.ToString() == userProfile.MembershipExpiryYears ? (double.Parse(pay["SubTotal"]) - userProfile.MembershipAdmissionfee).ToString() : pay["SubTotal"], pay["Total"], "paid", pay["PaymentMode"], userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, userProfile.MembershipId, userProfile.Address, userProfile.State != string.Empty ? userProfile.State : string.Empty, userProfile.GSTIN, pay["PaymentReason"], DateTime.Parse(pay["CreateDateTimeStamp"]), (int.Parse(pay["expiryYear"]) - 1).ToString(), pay["expiryYear"].ToString(), pay["ChequeNumber"].Split("$%#")[0], userProfile.MembershipCurrentExpiryYear.ToString() == userProfile.MembershipExpiryYears ? userProfile.MembershipAdmissionfee.ToString() : string.Empty, pay["sourceGST"]);
                            Database.UpdateInvoicePath(userProfile.Id, int.Parse(pay["AdminId"]), url, pay["InvoiceId"]);
                            result.Add("message", "Invoice Generated Succeesfully");
                            result.Add("URL", url);
                        }
                        catch (Exception e)
                        {
                            log.LogInformation(e.Message);
                            result.Add("message", "Invoice Generation Failed.Try Again");
                            return new OkObjectResult(result);
                        }
                    }
                    catch (Exception)
                    {
                        result.Add("message", "Failed. Please Try Again");
                        return new OkObjectResult(result);
                    }
                }
            }

            return new OkObjectResult(true);
        }
    }
}
