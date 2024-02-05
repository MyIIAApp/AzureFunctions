using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// MembershipPayment  class
  /// </summary>
    public class PaymentDetailsForExcel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetailsForExcel"/> class.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="firstName">FirstName</param>
        /// <param name="lastName">LastName</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="email">email</param>
        /// <param name="invoiceId">InvoiceId</param>
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
        /// <param name="total">Total</param>
        /// <param name="onlineFees">OnlineFees</param>
        /// <param name="invoicePath">InvoicePath</param>
        /// <param name="createDateTimeStamp">CreateDateTimeStamp</param>
        /// <param name="status">Status</param>
        /// <param name="gstin">GSTIN</param>
        /// <param name="invoiceNumber">Invoice Number</param>
        /// <param name="hoShare">HOShare</param>
        /// <param name="chapterShare">ChapterShare</param>
        public PaymentDetailsForExcel(string userId, string firstName, string lastName, string phoneNumber, string email, string invoiceId, string adminId, string subTotal, string igstPercent, string cgstPercent, string sgstPercent, string igstValue, string cgstValue, string sgstValue, string paymentReason, string paymentMode, string chequeNumber, string onlineTransactionId, string orderId, string total, string onlineFees, string invoicePath, string createDateTimeStamp, string status, string gstin, string invoiceNumber, string hoShare, string chapterShare)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.InvoiceId = invoiceId;
            this.AdminId = adminId;
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
            this.Total = total;
            this.OnlineFees = onlineFees;
            this.InvoicePath = invoicePath;
            this.CreateDateTimeStamp = createDateTimeStamp;
            this.Status = status;
            this.GSTIN = gstin;
            this.InvoiceNumber = invoiceNumber;
            this.HOShare = hoShare;
            this.ChapterShare = chapterShare;
        }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets firstname
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets lastname
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets phonenumber
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets InvoiceId
        /// </summary>
        [JsonProperty(PropertyName = "InvoiceId")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets adminId
        /// </summary>
        [JsonProperty(PropertyName = "adminId")]
        public string AdminId { get; set; }

        /// <summary>
        /// Gets or sets subTotal
        /// </summary>
        [JsonProperty(PropertyName = "subTotal")]
        public string SubTotal { get; set; }

        /// <summary>
        /// Gets or sets igstPercent
        /// </summary>
        [JsonProperty(PropertyName = "igstPercent")]
        public string IGSTPercent { get; set; }

        /// <summary>
        /// Gets or sets cgstPercent
        /// </summary>
        [JsonProperty(PropertyName = "cgstPercent")]
        public string CGSTPercent { get; set; }

        /// <summary>
        /// Gets or sets sgstPercent
        /// </summary>
        [JsonProperty(PropertyName = "sgstPercent")]
        public string SGSTPercent { get; set; }

        /// <summary>
        /// Gets or sets igstValue
        /// </summary>
        [JsonProperty(PropertyName = "igstValue")]
        public string IGSTValue { get; set; }

        /// <summary>
        /// Gets or sets cgstValue
        /// </summary>
        [JsonProperty(PropertyName = "cgstValue")]
        public string CGSTValue { get; set; }

        /// <summary>
        /// Gets or sets sgstValue
        /// </summary>
        [JsonProperty(PropertyName = "sgstValue")]
        public string SGSTValue { get; set; }

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
        /// Gets or sets total
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets onlineFees
        /// </summary>
        [JsonProperty(PropertyName = "onlineFees")]
        public string OnlineFees { get; set; }

        /// <summary>
        /// Gets or sets invoicePath
        /// </summary>
        [JsonProperty(PropertyName = "invoicePath")]
        public string InvoicePath { get; set; }

        /// <summary>
        /// Gets or sets date stamp
        /// </summary>
        [JsonProperty(PropertyName = "createDateTimeStamp")]
        public string CreateDateTimeStamp { get; set; }

        /// <summary>
        /// Gets or sets staus
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets GSTIN
        /// </summary>
        [JsonProperty(PropertyName = "GSTIN")]
        public string GSTIN { get; set; }

        /// <summary>
        /// Gets or sets InvoiceNumber
        /// </summary>
        [JsonProperty(PropertyName = "InvoiceNumber")]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets HOShare
        /// </summary>
        [JsonProperty(PropertyName = "HOShare")]
        public string HOShare { get; set; }

        /// <summary>
        /// Gets or sets ChapterShare
        /// </summary>
        [JsonProperty(PropertyName = "ChapterShare")]
        public string ChapterShare { get; set; }
    }
}
