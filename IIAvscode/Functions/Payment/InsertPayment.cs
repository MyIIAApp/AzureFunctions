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
    /// Insert Payment function
    /// </summary>
    public static class InsertPayment
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("InsertPayment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get rates request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            LoginMetadata token = FunctionUtility.ValidateToken(req);
            if (token == null)
            {
                return new UnauthorizedResult();
            }

            string phoneNumber = data?.phoneNumber;

            var paymentdetails = Database.GetPaymentUserId(phoneNumber);
            if (paymentdetails.UserId == -1)
            {
                return new BadRequestObjectResult(new BaseResponse(token, "User not found /Profile not created"));
            }

            data.adminId = token.IsAdmin ? token.Id : -1;
            Payment payment = new Payment(data, paymentdetails);

            data.userId = payment.UserId;
            data.fullAmount = payment.SubTotal;
            UserProfile userProfile = Database.GetUserProfile(payment.UserId, null, null);
            if (payment.PaymentReason == "Membership")
            {
                Membership membership = new Membership(data);
                if (userProfile.Id == -1 || userProfile.ProfileStatus < 4)
                {
                    return new BadRequestObjectResult(new BaseResponse(token, "User Profile is not Completed/Approved By Admin"));
                }

                userProfile.ProfileStatus = (int)UserProfileStatusEnum.UserProfileStatus.ActiveMembership;
                string membershipExpiryYears = (userProfile.MembershipExpiryYears == null || userProfile.MembershipExpiryYears == string.Empty) ? membership.MembershipCurrentExpiryYear.ToString() : userProfile.MembershipExpiryYears + "," + membership.MembershipCurrentExpiryYear.ToString();
                if (userProfile.MembershipJoinDate == null)
                {
                    membership.MembershipJoinDate = DateTime.Today;
                    membership.MembershipId = membership.MembershipId == null || membership.MembershipId == string.Empty ? (100000 + membership.UserId).ToString() : membership.MembershipId;
                }
                else if ((DateTime.Today.Date - DateTime.Parse(userProfile.MembershipCurrentExpiryYear.ToString() + "-03-31").Date).TotalDays >= 0)
                {
                    membership.MembershipJoinDate = userProfile.MembershipJoinDate;
                    membership.MembershipId = userProfile.MembershipId;
                }
                else
                {
                    return new BadRequestObjectResult(new BaseResponse(token, "Active Membership Already Existed"));
                }

                var invoiceId = Database.InsertPayment(payment);
                var invoiceNumber = "IIA-" + invoiceId.ToString();
                string invoicePath = CreatePDF.CreateInvoice(invoiceNumber, DateTime.Now.ToString(), payment.PaymentReason, "99959", payment.CGSTPercent.ToString(), payment.SGSTPercent.ToString(), payment.IGSTPercent.ToString(), payment.CGSTValue.ToString(), payment.SGSTValue.ToString(), payment.IGSTValue.ToString(), payment.SubTotal.ToString(), payment.Total.ToString(), payment.Total.ToString(), payment.PaymentMode, paymentdetails.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName);
                Database.AddInvoicePath(invoicePath, payment.OrderId);
                Database.InsertMembership(membership, membershipExpiryYears, token.Id);
            }
            else
            {
                var invoiceId = Database.InsertPayment(payment);
                var invoiceNumber = "IIA-" + invoiceId.ToString();
                string invoicePath = CreatePDF.CreateInvoice(invoiceNumber, DateTime.Now.ToString(), payment.PaymentReason, "99959", payment.CGSTPercent.ToString(), payment.SGSTPercent.ToString(), payment.IGSTPercent.ToString(), payment.CGSTValue.ToString(), payment.SGSTValue.ToString(), payment.IGSTValue.ToString(), payment.SubTotal.ToString(), payment.Total.ToString(), payment.Total.ToString(), payment.PaymentMode, paymentdetails.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName);
                Database.AddInvoicePath(invoicePath, payment.OrderId);
            }

            return new OkObjectResult(new BaseResponse(token, "Payment created"));
        }
    }
}