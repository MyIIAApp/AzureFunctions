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
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string userId;
            string paymentMode;
            string paymentMade;
            string chequeNumber;
            string startYear;
            string expiryYear;
            string paymentType;
            string subTotal;
            string paymentReason2;
            string paymentReason;
            string number;
            double total2 = 0.0;
            string newsms;
            string sourceGst;
            DateTime curr = DateTime.Now;
            string invoiceNumber = curr.ToString("yyyy") + curr.Millisecond.ToString();
            try
            {
                userId = data?.userId;
                paymentMode = data?.paymentMode;
                paymentMade = data?.paymentMade;
                chequeNumber = data?.chequeNumber;
                startYear = data?.startYear;
                expiryYear = data?.expiryYear;
                paymentType = data?.paymentType;
                subTotal = data?.subTotal;
                paymentReason2 = data?.paymentReason2;
                paymentReason = data?.paymentReason;
                Dictionary<string, dynamic> invoicePath = new Dictionary<string, dynamic>();
                UserProfile userProfile = Database.GetUserProfile(int.Parse(userId), null, null);

                PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile);
                if (paymentReason == "Membership")
                {
                    int updatedStatus = userProfile.MembershipCurrentExpiryYear > int.Parse(expiryYear) ? userProfile.ProfileStatus : curr.Month <= 3 ? int.Parse(expiryYear) >= curr.Year ? 5 : int.Parse(expiryYear) == curr.Year - 1 ? 6 : 7 : int.Parse(expiryYear) > curr.Year ? 5 : int.Parse(expiryYear) == curr.Year ? 6 : 7;
                    UserProfile updatedUserProfile;
                    try
                    {
                        number = Database.StorePayment(int.Parse(userId), token.Id, paymentType == "Default" ? paymentDetail.MembershipFee + paymentDetail.AdmissionFee : double.Parse(subTotal), paymentDetail.Cgst == 0 ? 0 : 9, paymentDetail.Sgst == 0 ? 0 : 9, paymentDetail.Igst == 0 ? 0 : 18, paymentType == "Default" ? paymentDetail.Igst : paymentDetail.Igst == 0 ? 0 : (double.Parse(subTotal) * 18) / 100, paymentType == "Default" ? paymentDetail.Sgst : paymentDetail.Sgst == 0 ? 0 : (double.Parse(subTotal) * 9) / 100, paymentType == "Default" ? paymentDetail.Cgst : paymentDetail.Cgst == 0 ? 0 : (double.Parse(subTotal) * 9) / 100, "Membership Renewal Fees", paymentMode, chequeNumber ?? string.Empty, invoiceNumber, paymentType == "Default" ? paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst : (double.Parse(subTotal) * 118) / 100, curr, 1, string.Empty, int.Parse(expiryYear));
                        Database.UpdateFullInvoiceNum("I-" + "2324" + "-" + (int.Parse(number) - 8512).ToString(), number);
                        sourceGst = Database.GetSourceGST(int.Parse(number));
                        total2 = paymentType == "Default" ? paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst : (double.Parse(subTotal) * 118) / 100;
                        invoicePath.Add("paymentSuccess", true);
                    }
                    catch (Exception ex)
                    {
                        invoicePath.Add("paymentSuccess", false);
                        invoicePath.Add("errorMessage", "Payment Failed: " + ex.Message);
                        return new OkObjectResult(invoicePath);
                    }

                    /*number = Database.StorePayment(int.Parse(userId), token.Id, paymentDetail.MembershipFee + paymentDetail.AdmissionFee, paymentDetail.Cgst == 0 ? 0 : 9, paymentDetail.Sgst == 0 ? 0 : 9, paymentDetail.Igst == 0 ? 0 : 18, paymentDetail.Igst, paymentDetail.Sgst, paymentDetail.Cgst, "Membership Renewal Fees", paymentMode, chequeNumber ?? string.Empty, invoiceNumber, paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst, curr, 1, string.Empty, int.Parse(expiryYear));
                    invoicePath.Add("paymentSuccess", true);*/

                    try
                    {
                        string membershipId = userProfile.MembershipId != null && userProfile.MembershipId != string.Empty ? userProfile.MembershipId : (20000 + userProfile.Id).ToString();
                        DateTime membershipJoinDate = userProfile.MembershipId != null && userProfile.MembershipId != string.Empty ? userProfile.MembershipJoinDate : curr;
                        string membershipExpiryYears = SortExpiryYears(userProfile.MembershipExpiryYears != null && userProfile.MembershipExpiryYears != string.Empty ? userProfile.MembershipExpiryYears + "," + expiryYear : expiryYear);
                        updatedUserProfile = new UserProfile(userProfile.Id, userProfile.PhoneNumber, membershipId, userProfile.MembershipAdmissionfee != 0 ? userProfile.MembershipAdmissionfee : paymentDetail.AdmissionFee, paymentDetail.MembershipFee, userProfile.MembershipCurrentExpiryYear > int.Parse(expiryYear) ? userProfile.MembershipCurrentExpiryYear : int.Parse(expiryYear), membershipJoinDate, curr, membershipExpiryYears, updatedStatus, userProfile.ChapterId, userProfile.ChapterName, userProfile.UnitName, userProfile.GSTIN, userProfile.GSTcertpath, userProfile.IndustryStatus, userProfile.Address, userProfile.District, userProfile.City, userProfile.State, userProfile.Country, userProfile.Pincode, userProfile.WebsiteUrl, userProfile.ProductCategory, userProfile.ProductSubCategory, userProfile.MajorProducts, userProfile.AnnualTurnOver, userProfile.EnterpriseType, userProfile.Exporter, userProfile.Classification, userProfile.FirstName, userProfile.LastName, userProfile.Email, userProfile.DateOfBirth, userProfile.DateOfMarriage, userProfile.ProfileImagePath, userProfile.FinancialProofPath, userProfile.SignaturePath, userProfile.CreatedBy, token.Id, userProfile.CreatedDate, curr);
                        Database.InsertUpdateUserProfile(updatedUserProfile, token.Id);
                        invoicePath.Add("membershipProfileUpdate", true);
                    }
                    catch (Exception ex)
                    {
                        invoicePath.Add("membershipProfileUpdate", false);
                        invoicePath.Add("errorMessage", "Membership Updation Failed: " + ex.Message);
                        return new OkObjectResult(invoicePath);
                    }

                    try
                    {
                        string url = CreatePDF.CreateInvoice("I-" + "2324" + "-" + (int.Parse(number) - 8512).ToString(), DateTime.Now.ToString(), paymentDetail.AdmissionFee == 0 ? "Membership Renewal Fees" : "Membership Fee", "999599", paymentDetail.Cgst == 0 ? "0" : "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "18", paymentType == "Default" ? paymentDetail.Cgst.ToString() : paymentDetail.Cgst == 0 ? "0" : ((double.Parse(subTotal) * 9) / 100).ToString(), paymentType == "Default" ? paymentDetail.Sgst.ToString() : paymentDetail.Sgst == 0 ? "0" : ((double.Parse(subTotal) * 9) / 100).ToString(), paymentType == "Default" ? paymentDetail.Igst.ToString() : paymentDetail.Igst == 0 ? "0" : ((double.Parse(subTotal) * 18) / 100).ToString(), paymentType == "Default" ? paymentDetail.MembershipFee.ToString() : (double.Parse(subTotal) - paymentDetail.AdmissionFee).ToString(), paymentType == "Default" ? (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString() : Math.Round(double.Parse(subTotal) * 118 / 100).ToString(), paymentMade, paymentMode, userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, updatedUserProfile.MembershipId, userProfile.Address, userProfile.State != string.Empty ? userProfile.State : ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN) != string.Empty ? ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN)[3..] : string.Empty, userProfile.GSTIN, paymentDetail.AdmissionFee == 0 ? "Membership Renewal Fees" : "Membership Fee", curr, startYear, expiryYear, chequeNumber.Split("$%#")[0], paymentDetail.AdmissionFee == 0 ? string.Empty : paymentDetail.AdmissionFee.ToString(), sourceGst);
                        Database.UpdateInvoicePath(int.Parse(userId), token.Id, url, number);
                        invoicePath.Add("invoiceGenerated", true);
                        invoicePath.Add("InvoicePath", url);
                        try
                        {
                            newsms = SmsHelper.SendSMS2(userProfile.PhoneNumber, total2.ToString(), (int)TypeOfMessage.Payment);
                        }
                        catch (Exception e)
                        {
                            log.LogInformation(e.Message);
                        }

                        return new OkObjectResult(invoicePath);
                    }
                    catch (Exception ex)
                    {
                        invoicePath.Add("errorMessage", "Invoice Generation Failed: " + ex.Message);
                        invoicePath.Add("invoiceGenerated", false);
                        return new OkObjectResult(invoicePath);
                    }
                }

                /*try
                {
                    string url = CreatePDF.CreateInvoice("IIA-" + curr.ToString("yyyyMMdd") + "-" + number, DateTime.Now.ToString(), "Membership Renewal Fees", "999599", paymentDetail.Cgst == 0 ? "0" : "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "18", paymentDetail.Cgst.ToString(), paymentDetail.Sgst.ToString(), paymentDetail.Igst.ToString(), paymentDetail.MembershipFee.ToString(), (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString(), paymentMade, paymentMode, userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, updatedUserProfile.MembershipId, userProfile.Address, userProfile.State, userProfile.GSTIN, "Membership Renewal Fees", curr, startYear, expiryYear, chequeNumber.Split("$%#")[0], paymentDetail.AdmissionFee == 0 ? string.Empty : paymentDetail.AdmissionFee.ToString());
                }*/
                else
                {
                    try
                    {
                        number = Database.StorePayment(int.Parse(userId), token.Id, paymentType == "Default" ? paymentDetail.MembershipFee + paymentDetail.AdmissionFee : double.Parse(subTotal), paymentDetail.Cgst == 0 ? 0 : 9, paymentDetail.Sgst == 0 ? 0 : 9, paymentDetail.Igst == 0 ? 0 : 18, paymentType == "Default" ? paymentDetail.Igst : paymentDetail.Igst == 0 ? 0 : (double.Parse(subTotal) * 18) / 100, paymentType == "Default" ? paymentDetail.Sgst : paymentDetail.Sgst == 0 ? 0 : (double.Parse(subTotal) * 9) / 100, paymentType == "Default" ? paymentDetail.Cgst : paymentDetail.Cgst == 0 ? 0 : (double.Parse(subTotal) * 9) / 100, paymentReason2, paymentMode, chequeNumber ?? string.Empty, invoiceNumber, paymentType == "Default" ? paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst : (double.Parse(subTotal) * 118) / 100, curr, 1, string.Empty, -int.Parse(expiryYear));
                        Database.UpdateFullInvoiceNum("I-" + "2324" + "-" + (int.Parse(number) - 8512).ToString(), number);
                        sourceGst = Database.GetSourceGST(int.Parse(number));
                        total2 = paymentType == "Default" ? paymentDetail.MembershipFee + paymentDetail.AdmissionFee + paymentDetail.Cgst + paymentDetail.Igst + paymentDetail.Sgst : (double.Parse(subTotal) * 118) / 100;
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
                        string url = CreatePDF.CreateInvoice("I-" + "2324" + "-" + (int.Parse(number) - 8512).ToString(), DateTime.Now.ToString(), paymentReason2, "999599", paymentDetail.Cgst == 0 ? "0" : "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "18", paymentType == "Default" ? paymentDetail.Cgst.ToString() : paymentDetail.Cgst == 0 ? "0" : ((double.Parse(subTotal) * 9) / 100).ToString(), paymentType == "Default" ? paymentDetail.Sgst.ToString() : paymentDetail.Sgst == 0 ? "0" : ((double.Parse(subTotal) * 9) / 100).ToString(), paymentType == "Default" ? paymentDetail.Igst.ToString() : paymentDetail.Igst == 0 ? "0" : ((double.Parse(subTotal) * 18) / 100).ToString(), paymentType == "Default" ? paymentDetail.MembershipFee.ToString() : (double.Parse(subTotal) - paymentDetail.AdmissionFee).ToString(), paymentType == "Default" ? (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString() : ((double.Parse(subTotal) * 118) / 100).ToString(), paymentMade, paymentMode, userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, userProfile.MembershipId, userProfile.Address, userProfile.State, userProfile.GSTIN, paymentReason2, curr, startYear, expiryYear, chequeNumber.Split("$%#")[0], paymentDetail.AdmissionFee == 0 ? string.Empty : paymentDetail.AdmissionFee.ToString(), sourceGst);
                        Database.UpdateInvoicePath(int.Parse(userId), token.Id, url, number);
                        invoicePath.Add("invoiceGenerated", true);
                        invoicePath.Add("InvoicePath", url);
                        try
                        {
                            newsms = SmsHelper.SendSMS2(userProfile.PhoneNumber, total2.ToString(), (int)TypeOfMessage.Payment);
                        }
                        catch (Exception e)
                        {
                            log.LogInformation(e.Message);
                        }

                        return new OkObjectResult(invoicePath);
                    }
                    catch (Exception ex)
                    {
                        invoicePath.Add("errorMessage", "Invoice Generation Failed: " + ex.Message);
                        invoicePath.Add("invoiceGenerated", false);
                        return new OkObjectResult(invoicePath);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
                return new BadRequestObjectResult("Bad Request");
            }
        }

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="expiryYears">expiryYears</param>
        /// <returns>Number</returns>
        public static string SortExpiryYears(string expiryYears)
        {
            string[] arr = expiryYears.Split(",");
            Array.Sort(arr, StringComparer.InvariantCulture);
            return string.Join(',', arr);
        }
    }
}
