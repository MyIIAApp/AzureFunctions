using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
    /// Create Invoice For Membership Payment
    /// </summary>
    public static class MakePayment
    {
        /// <summary>
        /// Function
        /// </summary>
        /// <param name="req">request body</param>
        /// <param name="log">logger</param>
        /// <returns>HTTP result</returns>
        [FunctionName("MakePayment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateInvoiceForMembershipPayment Request!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            string[] parameters = requestBody.Split('&');
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (string parameter in parameters)
            {
                if (parameter.Split('=').Length == 2)
                {
                    data[parameter.Split('=')[0]] = parameter.Split('=')[1];
                }
            }

            string status;
            string stringForHash = Environment.GetEnvironmentVariable("PaymentGatewaySalt") + "|" + data["status"] + "||||||" + data["udf5"] + "|" + data["udf4"] + "|" + data["udf3"] + "|" + data["udf2"] + "|" + data["udf1"] + "|" + data["email"] + "|" + data["firstname"] + "|" + data["productinfo"] + "|" + data["amount"] + "|" + data["txnid"] + "|" + data["key"];
            string hash = FunctionUtility.ComputeSha256Hash(stringForHash);
            string returnHtmlString;
            if (hash != data["hash"])
            {
                status = "tempered";
                returnHtmlString = "<!doctype html> <html> <head> <meta charset=\"utf - 8\"> <title>Payment " + status + "</title> <link rel=\"stylesheet\" type=\"text / css\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\" /> <link rel=\"stylesheet\" type=\"text/css\" href=\"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\" /> <style type=\"text/css\"> body { background:#f2f2f2;     }      .payment 	{ 		border:1px solid #f2f2f2; padding-bottom:100px;         border-radius:20px;         background:#fff; 	}    .payment_header    { 	   background:rgba(203,32,45,1); 	   padding:20px;        border-radius:20px 20px 0px 0px; 	       }        .check    { 	   margin:0px auto; 	   width:50px; 	   height:50px; 	   border-radius:100%; 	   background:#fff; 	   text-align:center;    }        .check i    { 	   vertical-align:middle; 	   line-height:50px; 	   font-size:30px;    }      .content      {         text-align:center;     }      .content  h1     {         font-size:60px;         padding-top:25px;     }      .content p { font-size:30px;   }    .content a     {         font-size:45px     }   .paymentCard{ margin-top:10vh;} .IIAImage {  width:40%; margin-top:80px  } 		 	</style> 	 </head>  <body>    <div class=\"container paymentCard\">    <div class=\"row\">       <div class=\"col-md-6 mx-auto mt-5\">          <div class=\"payment\">             <div class=\"payment_header\">                <div class=\"check\">" + (status == "success" ? "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>" : "<i class=\"fa fa-close\" aria-hidden=\"true\"></i>") + "</div>             </div>             <div class=\"content\"><img class=\"IIAImage\" src=\"https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png\"\\>                <h1>Payment " + status + " !</h1>                <p>" + (status == "success" ? string.Empty : "<br/>If the money got Deducted it will be refunded back in 72 hrs.") + "</p>" + "<p>Please Close the browser window to refresh</p></div> </div> </div> </div> </div> </body> </html> ";
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = returnHtmlString,
                };
            }

            DateTime curr = DateTime.Now;
            Dictionary<string, dynamic> invoicePath = new Dictionary<string, dynamic>();
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
                {
                    { "key", Environment.GetEnvironmentVariable("PaymentGatewayKey") },
                    { "command", "verify_payment" },
                    { "var1", data["txnid"] },
                    { "hash", FunctionUtility.ComputeSha256Hash(Environment.GetEnvironmentVariable("PaymentGatewayKey") + "|verify_payment|" + data["txnid"] + "|" + Environment.GetEnvironmentVariable("PaymentGatewaySalt")) },
                };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://info.payu.in/merchant/postservice.php?form=2", content);
            string responseString = await response.Content.ReadAsStringAsync();
            dynamic verifyPaymentData = JsonConvert.DeserializeObject<dynamic>(responseString);
            if (verifyPaymentData?.status != 1)
            {
                returnHtmlString = "<!doctype html> <html> <head> <meta charset=\"utf - 8\"> <title>Payment " + "failed" + "</title> <link rel=\"stylesheet\" type=\"text / css\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\" /> <link rel=\"stylesheet\" type=\"text/css\" href=\"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\" /> <style type=\"text/css\"> body { background:#f2f2f2;     }      .payment 	{ 		border:1px solid #f2f2f2; padding-bottom:100px;         border-radius:20px;         background:#fff; 	}    .payment_header    { 	   background:rgba(203,32,45,1); 	   padding:20px;        border-radius:20px 20px 0px 0px; 	       }        .check    { 	   margin:0px auto; 	   width:50px; 	   height:50px; 	   border-radius:100%; 	   background:#fff; 	   text-align:center;    }        .check i    { 	   vertical-align:middle; 	   line-height:50px; 	   font-size:30px;    }      .content      {         text-align:center;     }      .content  h1     {         font-size:60px;         padding-top:25px;     }      .content p { font-size:30px;   }    .content a     {         font-size:45px     }   .paymentCard{ margin-top:10vh;} .IIAImage {  width:40%; margin-top:80px  } 		 	</style> 	 </head>  <body>    <div class=\"container paymentCard\">    <div class=\"row\">       <div class=\"col-md-6 mx-auto mt-5\">          <div class=\"payment\">             <div class=\"payment_header\">                <div class=\"check\">" + "<i class=\"fa fa-close\" aria-hidden=\"true\"></i>" + "</div>             </div>             <div class=\"content\"><img class=\"IIAImage\" src=\"https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png\"\\>                <h1>Payment " + "failed" + " !</h1><p>This Payment is Failed during initiation.</p></div> </div> </div> </div> </div> </body> </html> ";
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = returnHtmlString,
                };
            }

            if (verifyPaymentData?.transaction_details?[data["txnid"]].mihpayid != data["mihpayid"] || verifyPaymentData?.transaction_details?[data["txnid"]].txnid != data["txnid"] || verifyPaymentData?.transaction_details?[data["txnid"]].amt != data["amount"])
            {
                status = "tempered";
                returnHtmlString = "<!doctype html> <html> <head> <meta charset=\"utf - 8\"> <title>Payment " + status + "</title> <link rel=\"stylesheet\" type=\"text / css\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\" /> <link rel=\"stylesheet\" type=\"text/css\" href=\"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\" /> <style type=\"text/css\"> body { background:#f2f2f2;     }      .payment 	{ 		border:1px solid #f2f2f2; padding-bottom:100px;         border-radius:20px;         background:#fff; 	}    .payment_header    { 	   background:rgba(203,32,45,1); 	   padding:20px;        border-radius:20px 20px 0px 0px; 	       }        .check    { 	   margin:0px auto; 	   width:50px; 	   height:50px; 	   border-radius:100%; 	   background:#fff; 	   text-align:center;    }        .check i    { 	   vertical-align:middle; 	   line-height:50px; 	   font-size:30px;    }      .content      {         text-align:center;     }      .content  h1     {         font-size:60px;         padding-top:25px;     }      .content p { font-size:30px;   }    .content a     {         font-size:45px     }   .paymentCard{ margin-top:10vh;} .IIAImage {  width:40%; margin-top:80px  } 		 	</style> 	 </head>  <body>    <div class=\"container paymentCard\">    <div class=\"row\">       <div class=\"col-md-6 mx-auto mt-5\">          <div class=\"payment\">             <div class=\"payment_header\">                <div class=\"check\">" + (status == "success" ? "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>" : "<i class=\"fa fa-close\" aria-hidden=\"true\"></i>") + "</div>             </div>             <div class=\"content\"><img class=\"IIAImage\" src=\"https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png\"\\>                <h1>Payment " + status + " !</h1>                <p>Transaction id :" + verifyPaymentData?.transaction_details?[data["txnid"]].mihpayid + "<br/>Order id:" + verifyPaymentData?.transaction_details?[data["txnid"]].txnid + "<br/>Amount: " + verifyPaymentData?.transaction_details?[data["txnid"]].amt + "<br/>Please note it for Future Reference" + (status == "success" ? string.Empty : "<br/>If the money got Deducted it will be refunded back in 72 hrs.") + "</p>" + "<p>Please Close the browser window to refresh</p></div> </div> </div> </div> </div> </body> </html> ";
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = returnHtmlString,
                };
            }

            if (verifyPaymentData?.transaction_details?[data["txnid"]].status != "success")
            {
                data["status"] = "failed";
            }

            if (Database.UpdatePaymentStatus(int.Parse(data["udf3"]), data["status"] == "success" ? 1 : 0, data["mihpayid"]) != -2)
            {
                returnHtmlString = "<!doctype html> <html> <head> <meta charset=\"utf - 8\"> <title>Payment " + "failed" + "</title> <link rel=\"stylesheet\" type=\"text / css\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\" /> <link rel=\"stylesheet\" type=\"text/css\" href=\"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\" /> <style type=\"text/css\"> body { background:#f2f2f2;     }      .payment 	{ 		border:1px solid #f2f2f2; padding-bottom:100px;         border-radius:20px;         background:#fff; 	}    .payment_header    { 	   background:rgba(203,32,45,1); 	   padding:20px;        border-radius:20px 20px 0px 0px; 	       }        .check    { 	   margin:0px auto; 	   width:50px; 	   height:50px; 	   border-radius:100%; 	   background:#fff; 	   text-align:center;    }        .check i    { 	   vertical-align:middle; 	   line-height:50px; 	   font-size:30px;    }      .content      {         text-align:center;     }      .content  h1     {         font-size:60px;         padding-top:25px;     }      .content p { font-size:30px;   }    .content a     {         font-size:45px     }   .paymentCard{ margin-top:10vh;} .IIAImage {  width:40%; margin-top:80px  } 		 	</style> 	 </head>  <body>    <div class=\"container paymentCard\">    <div class=\"row\">       <div class=\"col-md-6 mx-auto mt-5\">          <div class=\"payment\">             <div class=\"payment_header\">                <div class=\"check\">" + "<i class=\"fa fa-close\" aria-hidden=\"true\"></i>" + "</div>             </div>             <div class=\"content\"><img class=\"IIAImage\" src=\"https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png\"\\>                <h1>Payment " + "failed" + " !</h1><p>This Payment is Already Recorded.</p></div> </div> </div> </div> </div> </body> </html> ";
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = returnHtmlString,
                };
            }

            UserProfile userProfile = Database.GetUserProfile(data["udf2"], null, null);
            PaymentDetail paymentDetail = CalculatePayment.CalculateMembershipPayment(userProfile);
            int expiryYear = curr.Year;
            UserProfile updatedUserProfile = userProfile;
            if (data["status"] == "success")
            {
                    status = "success";
                    try
                    {
                        try
                        {
                            string membershipId = userProfile.MembershipId != null && userProfile.MembershipId != string.Empty ? userProfile.MembershipId : (20000 + userProfile.Id).ToString();
                            DateTime membershipJoinDate = userProfile.MembershipId != null && userProfile.MembershipId != string.Empty ? userProfile.MembershipJoinDate : curr;
                            expiryYear = userProfile.MembershipCurrentExpiryYear != 0 ? userProfile.MembershipCurrentExpiryYear + 1 : curr.Month < 4 ? (curr.Year + 1) : curr.Year;
                            string membershipExpiryYears = userProfile.MembershipExpiryYears != null && userProfile.MembershipExpiryYears != string.Empty ? userProfile.MembershipExpiryYears + "," + expiryYear.ToString() : expiryYear.ToString();
                            updatedUserProfile = new UserProfile(userProfile.Id, userProfile.PhoneNumber, membershipId, userProfile.MembershipAdmissionfee != 0 ? userProfile.MembershipAdmissionfee : paymentDetail.AdmissionFee, paymentDetail.MembershipFee, expiryYear, membershipJoinDate, curr, membershipExpiryYears, expiryYear > curr.Year ? 5 : expiryYear == curr.Year ? curr.Month < 4 ? 6 : 5 : expiryYear == curr.Year - 1 ? curr.Month < 4 ? 6 : 7 : 7, userProfile.ChapterId, userProfile.ChapterName, userProfile.UnitName, userProfile.GSTIN, userProfile.GSTcertpath, userProfile.IndustryStatus, userProfile.Address, userProfile.District, userProfile.City, userProfile.State, userProfile.Country, userProfile.Pincode, userProfile.WebsiteUrl, userProfile.ProductCategory, userProfile.ProductSubCategory, userProfile.MajorProducts, userProfile.AnnualTurnOver, userProfile.EnterpriseType, userProfile.Exporter, userProfile.Classification, userProfile.FirstName, userProfile.LastName, userProfile.Email, userProfile.DateOfBirth, userProfile.DateOfMarriage, userProfile.ProfileImagePath, userProfile.FinancialProofPath, userProfile.SignaturePath, userProfile.CreatedBy, -2, userProfile.CreatedDate, curr);
                            Database.InsertUpdateUserProfile(updatedUserProfile, userProfile.UpdatedBy);
                            invoicePath.Add("membershipProfileUpdate", true);
                       }
                       catch (Exception ex)
                       {
                           invoicePath.Add("membershipProfileUpdate", false);
                           invoicePath.Add("errorMessage", "Membership Updation Failed: " + ex.Message);
                       }

                        try
                        {
                           string sourceGst = Database.GetSourceGST(int.Parse(data["udf3"]));
                           string url = CreatePDF.CreateInvoice("I-" + "2324" + "-" + (int.Parse(data["udf3"]) - 8512).ToString(), DateTime.Now.ToString(), paymentDetail.AdmissionFee == 0 ? "Membership Renewal Fees" : "Membership Fees", "999599", paymentDetail.Cgst == 0 ? "0" : "9", paymentDetail.Sgst == 0 ? "0" : "9", paymentDetail.Igst == 0 ? "0" : "18", paymentDetail.Cgst.ToString(), paymentDetail.Sgst.ToString(), paymentDetail.Igst.ToString(), paymentDetail.MembershipFee.ToString(), (paymentDetail.AdmissionFee + paymentDetail.MembershipFee + paymentDetail.Cgst + paymentDetail.Sgst + paymentDetail.Igst).ToString(), "paid", "online payment", userProfile.GSTIN, userProfile.UnitName, userProfile.PhoneNumber, userProfile.ChapterName, updatedUserProfile.MembershipId, userProfile.Address, ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN) != string.Empty ? ExcelWriteForPaymentForGST.FindState(userProfile.GSTIN)[3..] : string.Empty, userProfile.GSTIN, "Membership Renewal Fees", curr, (expiryYear - 1).ToString(), expiryYear.ToString(), string.Empty, paymentDetail.AdmissionFee == 0 ? string.Empty : paymentDetail.AdmissionFee.ToString(), sourceGst);
                           Database.UpdateInvoicePath(int.Parse(data["udf2"]), -2, url, data["udf3"]);
                           invoicePath.Add("invoiceGenerated", true);
                           invoicePath.Add("InvoicePath", url);
                        }
                          catch (Exception ex)
                          {
                              invoicePath.Add("errorMessage", "Invoice Generation Failed: " + ex.Message);
                              invoicePath.Add("invoiceGenerated", false);
                          }
                      }
                      catch (Exception)
                      {
                          invoicePath.Add("Payment Failed", true);
                      }
                  }
                else
                {
                    status = "failed";
                }

            returnHtmlString = "<!doctype html> <html> <head> <meta charset=\"utf - 8\"> <title>Payment " + status + "</title> <link rel=\"stylesheet\" type=\"text / css\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\" /> <link rel=\"stylesheet\" type=\"text/css\" href=\"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css\" /> <style type=\"text/css\"> body { background:#f2f2f2;     }      .payment 	{ 		border:1px solid #f2f2f2; padding-bottom:100px;         border-radius:20px;         background:#fff; 	}    .payment_header    { 	   background:rgba(203,32,45,1); 	   padding:20px;        border-radius:20px 20px 0px 0px; 	       }        .check    { 	   margin:0px auto; 	   width:50px; 	   height:50px; 	   border-radius:100%; 	   background:#fff; 	   text-align:center;    }        .check i    { 	   vertical-align:middle; 	   line-height:50px; 	   font-size:30px;    }      .content      {         text-align:center;     }      .content  h1     {         font-size:60px;         padding-top:25px;     }      .content p { font-size:30px;   }    .content a     {         font-size:45px     }   .paymentCard{ margin-top:10vh;} .IIAImage {  width:40%; margin-top:80px  } 		 	</style> 	 </head>  <body>    <div class=\"container paymentCard\">    <div class=\"row\">       <div class=\"col-md-6 mx-auto mt-5\">          <div class=\"payment\">             <div class=\"payment_header\">                <div class=\"check\">" + (status == "success" ? "<i class=\"fa fa-check\" aria-hidden=\"true\"></i>" : "<i class=\"fa fa-close\" aria-hidden=\"true\"></i>") + "</div>             </div>             <div class=\"content\"><img class=\"IIAImage\" src=\"https://iiaprodstorage.blob.core.windows.net/utils/IIALogo.png\"\\>                <h1>Payment " + status + " !</h1>                <p>Transaction id :" + verifyPaymentData?.transaction_details?[data["txnid"]].mihpayid + "<br/>Order id:" + verifyPaymentData?.transaction_details?[data["txnid"]].txnid + "<br/>Amount: " + verifyPaymentData?.transaction_details?[data["txnid"]].amt + "<br/>Please note it for Future Reference" + (status == "success" ? string.Empty : "<br/>If the money got Deducted it will be refunded back in 72 hrs.") + "</p>" + (status == "success" && invoicePath["invoiceGenerated"] ? "<a href=\"" + invoicePath["InvoicePath"] + "\">Download Invoice</a>" : status == "success" ? "<p>Invoice Generation Failed.<br/>You will get Invoice soon in Payment History" : string.Empty) + "<p>Please Close the browser window to refresh</p></div> </div> </div> </div> </div> </body> </html> ";
            return new ContentResult
                 {
                     ContentType = "text/html",
                     StatusCode = (int)HttpStatusCode.OK,
                     Content = returnHtmlString,
                 };
        }
    }
}
