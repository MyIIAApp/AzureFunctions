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
    /// Create Invoice For Membership Payment
    /// </summary>
    public static class NonMemberInvoice
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("CreateInvoiceForNonMember")]
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
            }*/
            try
            {
                string name = data?.name;
                string state = data?.state;
                string address = data?.address;
                string gstin = data?.gstin;
                string paymentMode = data?.paymentMode;
                string checkNumber = data?.checkNumber;
                string checkDate = data?.checkDate;
                string phoneNumber = data?.phoneNumber;
                double subTotal = data?.subTotal;
                double cgst = data?.cgst;
                double sgst = data?.sgst;
                double igst = data?.igst;
                JArray itemList = data?.itemList;
                DateTime curr = DateTime.Now;

                log.LogInformation("item name is " + (string)itemList[0]["ItemName"]);
                log.LogInformation("name is " + name);
                log.LogInformation("year is " + curr.ToString("yyyy"));
                string number = Database.StoreNonMemberPayment(name, token.Id, subTotal, cgst, sgst, igst, paymentMode, checkNumber, Math.Round(subTotal + cgst + sgst + igst), 1, phoneNumber, address, gstin, checkDate, state);
                Database.UpdateFullInvoiceNumNonMem("OTI-" + "2324" + "-" + (int.Parse(number) - 711).ToString(), number);
                string sourceGst = Database.GetSourceGSTForNonMember(int.Parse(number));
                for (int i = 0; i < itemList.Count; i++)
                {
                    Database.StoreNonMemberPaymentItems(itemList[i]["ItemName"].ToString(), itemList[i]["GSTRate"].ToString(), itemList[i]["SAC"].ToString(), double.Parse(itemList[i]["Quantity"].ToString()), double.Parse(itemList[i]["UnitPrice"].ToString()), int.Parse(number));
                }

                string url = CreatePDF.CreateInvoiceForNonMember(name, state, address, gstin, paymentMode, checkNumber, checkDate, phoneNumber, itemList, curr, "OTI-" + "2324" + "-" + (int.Parse(number) - 711).ToString(), sourceGst);
                Database.StoreNonMemberInvoicePath(url, int.Parse(number));
                try
                {
                    string mssg = SmsHelper.SendSMS2(phoneNumber, (subTotal + cgst + sgst + igst).ToString(), (int)TypeOfMessage.Payment);
                }
                catch (Exception e)
                {
                    log.LogInformation(e.Message);
                }

                return new OkObjectResult(new BaseResponse(token, url));
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
                return new OkObjectResult(new BaseResponse(token, "Invalid Input, Please Try Again"));
            }
        }
    }
}