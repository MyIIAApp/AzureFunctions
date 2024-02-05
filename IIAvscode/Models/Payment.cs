using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// MembershipPayment  class
  /// </summary>
    public class Payment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Payment"/> class.
        /// </summary>
        /// <param name="invoiceId">InvoiceId</param>
        /// <param name="userId">UserId</param>
        /// <param name="adminId">AdminId</param>
        /// <param name="subTotal">SubTotal</param>
        /// <param name="igstPercent">IGSTPercent</param>
        /// <param name="cgstPercent">CGSTPercent</param>
        /// <param name="sgstPercent">SGSTPercent</param>
        /// <param name="igstValue">IGSTValue</param>
        /// <param name="cgstValue">CGSTValue</param>
        /// <param name="sgstValue">SGSTValue</param>
        /// <param name="paymentReason">PaymentReason</param>
        /// <param name="paymentMode">PaymentMode</param>
        /// <param name="chequeNumber">ChequeNumber</param>
        /// <param name="onlineTransactionId">OnlineTransactionId</param>
        /// <param name="orderId">OrderId</param>
        /// <param name="discountPercent">DiscountPercent</param>
        /// <param name="discountValue">DiscountValue</param>
        /// <param name="total">Total</param>
        /// <param name="onlineFees">OnlineFees</param>
        /// <param name="ho_Share">HO_Share</param>
        /// <param name="chapterShare">ChapterShare</param>
        /// <param name="invoicePath">InvoicePath</param>
        public Payment(string invoiceId, int userId, int adminId, double subTotal, int igstPercent, int cgstPercent, int sgstPercent, double igstValue, double cgstValue, double sgstValue, string paymentReason, string paymentMode, string chequeNumber, string onlineTransactionId, string orderId, int discountPercent, double discountValue, double total, double onlineFees, double ho_Share, double chapterShare, string invoicePath)
        {
            this.InvoiceId = invoiceId;
            this.UserId = userId;
            this.AdminId = adminId;
            this.UserId = userId;
            this.SubTotal = subTotal;
            this.IGSTPercent = igstPercent;
            this.CGSTPercent = cgstPercent;
            this.SGSTPercent = sgstPercent;
            this.IGSTValue = igstValue;
            this.CGSTPercent = cgstPercent;
            this.CGSTValue = cgstValue;
            this.SGSTValue = sgstValue;
            this.PaymentReason = paymentReason;
            this.PaymentMode = paymentMode;
            this.ChequeNumber = chequeNumber;
            this.OnlineTransactionId = onlineTransactionId;
            this.OrderId = orderId;
            this.DiscountPercent = discountPercent;
            this.DiscountValue = discountValue;
            this.Total = total;
            this.OnlineFees = onlineFees;
            this.HO_Share = ho_Share;
            this.ChapterShare = chapterShare;
            this.InvoicePath = invoicePath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Payment"/> class.
        /// </summary>
        /// <param name="data">RequestBody</param>
        /// <param name="paymentDetails">paymentDetails</param>
        public Payment(dynamic data, PaymentUserId paymentDetails)
        {
            this.UserId = paymentDetails.UserId;
            this.AdminId = data?.adminId;
            this.SubTotal = data?.subTotal;
            this.IGSTPercent = paymentDetails.State != Environment.GetEnvironmentVariable("IGSTState") ? short.Parse(Environment.GetEnvironmentVariable("TaxRate")) : 0;
            this.CGSTPercent = paymentDetails.State == Environment.GetEnvironmentVariable("IGSTState") ? short.Parse(Environment.GetEnvironmentVariable("TaxRate")) / 2 : 0;
            this.SGSTPercent = paymentDetails.State == Environment.GetEnvironmentVariable("IGSTState") ? short.Parse(Environment.GetEnvironmentVariable("TaxRate")) / 2 : 0;
            this.IGSTValue = this.SubTotal * this.IGSTPercent / 100;
            this.CGSTValue = this.SubTotal * this.CGSTPercent / 100;
            this.SGSTValue = this.SubTotal * this.SGSTPercent / 100;
            this.PaymentReason = data?.paymentReason;
            this.PaymentMode = data?.paymentMode;
            this.ChequeNumber = data?.chequeNumber;
            this.OnlineTransactionId = string.Empty;
            Random random = new Random();
            this.OrderId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + random.Next(0, 9).ToString();
            this.DiscountPercent = 0;
            this.DiscountValue = 0;
            this.Total = this.SubTotal + this.IGSTValue + this.CGSTValue + this.SGSTValue - this.DiscountValue;
            this.OnlineFees = this.PaymentMode == "Online" ? 0.02 * this.Total : 0;
            this.HO_Share = this.PaymentReason == "Membership" ? 1100 : this.SubTotal - this.OnlineFees;
            this.ChapterShare = this.PaymentReason == "Membership" ? this.SubTotal - this.HO_Share - this.OnlineFees : 0;
            this.InvoicePath = string.Empty;
        }

        /// <summary>
        /// Gets or sets InvoiceId
        /// </summary>
        [JsonProperty(PropertyName = "InvoiceId")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets adminId
        /// </summary>
        [JsonProperty(PropertyName = "adminId")]
        public int AdminId { get; set; }

        /// <summary>
        /// Gets or sets subTotal
        /// </summary>
        [JsonProperty(PropertyName = "subTotal")]
        public double SubTotal { get; set; }

        /// <summary>
        /// Gets or sets igstPercent
        /// </summary>
        [JsonProperty(PropertyName = "igstPercent")]
        public int IGSTPercent { get; set; }

        /// <summary>
        /// Gets or sets cgstPercent
        /// </summary>
        [JsonProperty(PropertyName = "cgstPercent")]
        public int CGSTPercent { get; set; }

        /// <summary>
        /// Gets or sets sgstPercent
        /// </summary>
        [JsonProperty(PropertyName = "sgstPercent")]
        public int SGSTPercent { get; set; }

        /// <summary>
        /// Gets or sets igstValue
        /// </summary>
        [JsonProperty(PropertyName = "igstValue")]
        public double IGSTValue { get; set; }

        /// <summary>
        /// Gets or sets cgstValue
        /// </summary>
        [JsonProperty(PropertyName = "cgstValue")]
        public double CGSTValue { get; set; }

        /// <summary>
        /// Gets or sets sgstValue
        /// </summary>
        [JsonProperty(PropertyName = "sgstValue")]
        public double SGSTValue { get; set; }

        /// <summary>
        /// Gets or sets paymentReason
        /// </summary>
        [JsonProperty(PropertyName = "paymentReason")]
        public string PaymentReason { get; set; }

        /// <summary>
        /// Gets or sets paymentMode
        /// </summary>
        [JsonProperty(PropertyName = "paymentMode")]
        public string PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets chequeNumber
        /// </summary>
        [JsonProperty(PropertyName = "chequeNumber")]
        public string ChequeNumber { get; set; }

        /// <summary>
        /// Gets or sets onlineTransactionId
        /// </summary>
        [JsonProperty(PropertyName = "onlineTransactionId")]
        public string OnlineTransactionId { get; set; }

        /// <summary>
        /// Gets or sets orderId
        /// </summary>
        [JsonProperty(PropertyName = "orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets discountPercent
        /// </summary>
        [JsonProperty(PropertyName = "discountPercent")]
        public int DiscountPercent { get; set; }

        /// <summary>
        /// Gets or sets discountValue
        /// </summary>
        [JsonProperty(PropertyName = "discountValue")]
        public double DiscountValue { get; set; }

        /// <summary>
        /// Gets or sets total
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets onlineFees
        /// </summary>
        [JsonProperty(PropertyName = "onlineFees")]
        public double OnlineFees { get; set; }

        /// <summary>
        /// Gets or sets ho_Share
        /// </summary>
        [JsonProperty(PropertyName = "ho_Share")]
        public double HO_Share { get; set; }

        /// <summary>
        /// Gets or sets chapterShare
        /// </summary>
        [JsonProperty(PropertyName = "chapterShare")]
        public double ChapterShare { get; set; }

        /// <summary>
        /// Gets or sets invoicePath
        /// </summary>
        [JsonProperty(PropertyName = "invoicePath")]
        public string InvoicePath { get; set; }
    }
}
