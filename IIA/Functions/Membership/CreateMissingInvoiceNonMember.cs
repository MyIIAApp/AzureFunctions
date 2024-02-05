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
using Newtonsoft.Json.Linq;

namespace IIABackend
{
    /// <summary>
    /// Create Invoice For NonMembership Payment
    /// </summary>
    public static class CreateMissingInvoiceNonMember
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateMissingInvoiceNonMember")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateInvoiceForNonMembershipPayment Request!");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string operation = data?.operation;
            string invoiceNumber = data?.invoiceId;
            DateTime curr = DateTime.Now;
            int number = int.Parse(invoiceNumber.Split("-")[2]);
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<dynamic> payments;
            List<dynamic> items;
            try
            {
                int num = number;

                if (invoiceNumber.Split("-")[1] == "2324")
                {
                    num = number + 711;
                }
                else
                {
                    num = number;
                }

                payments = Database.GetPaymentDetailsForNonMemberExcel(num, operation);
                items = Database.GetItemsForNonMemberExcel(num);
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
                result.Add(e.Message, "Invalid Invoice Number");
                return new OkObjectResult(result);
            }

            foreach (dynamic pay in payments)
            {
                try
                {
                   /* UserProfile userProfile = Database.GetUserProfile(pay["UserId"], null, null);*/
                    /*PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile.ProfileStatus, userProfile.AnnualTurnOver, userProfile.GSTIN);
                    try
                    {
                        number = Database.StorePayment(userProfile.Id, token.Id, int.Parse(pay["Key.SubTotal), 9, int.Parse(pay["Key.SGSTPercent), int.Parse(pay["Key.IGSTPercent), int.Parse(pay["Key.IGSTValue), int.Parse(pay["Key.SGSTValue), int.Parse(pay["Key.SGSTValue), pay["Key.PaymentReason, pay["Key.PaymentMode, pay["Key.ChequeNumber.Split("$%#")[0], pay["Key.InvoiceNumber, int.Parse(pay["Key.Total), DateTime.Parse(pay["Key.CreateDateTimeStamp), int.Parse(pay["Key.Status), pay["Key.OnlineTransactionId, pay["Value);
                        invoicePath.Add("paymentSuccess", true);
                    }
                    catch (Exception ex)
                    {
                        invoicePath.Add("paymentSuccess", false);
                        invoicePath.Add("errorMessage", "Payment Failed: " + ex.Message);
                        return new OkObjectResult(invoicePath);
                    }*/

                    try
                    {
                        /*JArray itemList = new JArray();

                        foreach (dynamic item in items)
                        {
                            itemList.Add(JObject.Parse(
                                @"{""ItemName"":""" + item.ItemName +
                                @"""GSTRate"":""" + item.GSTRate +
                                @"""SacValue"":""" + item.SacValue +
                                @"""Quantity"":""" + item.Quantity +
                                @"""UnitPrice"":""" + item.UnitPrice +
                            @"""}"));
                        }*/
                        var array = JArray.FromObject(items);
                        string url = CreatePDF.CreateInvoiceForNonMember(pay["Name"], pay["State"], pay["Address"], pay["GSTIN"], pay["PaymentMode"], pay["ChequeNumber"], pay["ChequeDate"], pay["PhoneNumber"], array, curr, invoiceNumber, pay["sourceGST"]);
                        Database.UpdateInvoicePathNonMember(url, pay["InvoiceId"]);
                        result.Add("message", "Invoice Generated Succeesfully");
                        result.Add("URL", url);
                    }
                    catch (Exception e)
                    {
                        result.Add(e.Message, "Invoice Generation Failed.Try Again");
                        return new OkObjectResult(result);
                    }
                }
                catch (Exception)
                {
                    result.Add("message", "Failed. Please Try Again");
                    return new OkObjectResult(result);
                }
            }

            return new OkObjectResult(result);
        }
    }
}
