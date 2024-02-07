using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Payment Reason Model
    /// </summary>
    public class PaymentRecordAllInvoiceModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRecordAllInvoiceModel"/> class.
        /// </summary>
        /// <param name="paymentReason">Payment Reason</param>
        /// <param name="paymentMode">Payment Mode</param>
        /// <param name="total">total amount</param>
        /// <param name="invoicePath">invoice Path</param>
        /// <param name="dateTime">Date and Time</param>
        /// <param name="chapterId">chapterId</param>
        /// <param name="chapterName">Chapter Name</param>
        /// <param name="adminName">Admin Name</param>
        /// <param name="expiryYear">ExpiryYear</param>
        /// <param name="invoiceId">Invoice ID</param>
        /// <param name="membershipId">Membership Id</param>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="gstin">GSTIN</param>
        public PaymentRecordAllInvoiceModel(string paymentReason, string paymentMode, string total, string invoicePath, string dateTime, int chapterId, string chapterName, string adminName, string expiryYear, string phoneNumber, string invoiceId, string membershipId, string gstin)
        {
            this.PaymentReason = paymentReason;
            this.PaymentMode = paymentMode;
            this.Total = total;
            this.InvoicePath = invoicePath;
            this.DateTime = dateTime;
            this.ChapterId = chapterId;
            this.AdminName = adminName;
            this.ChapterName = chapterName;
            this.ExpiryYear = expiryYear;
            this.PhoneNumber = phoneNumber;
            this.InvoiceId = invoiceId;
            this.MembershipId = membershipId;
            this.GSTIN = gstin;
        }

        /// <summary>
        /// Gets or sets Payment Reason
        /// </summary>
        [JsonProperty(PropertyName = "PaymentReason")]
        public string PaymentReason { get; set; }

        /// <summary>
        /// Gets or sets Payment Mode
        /// </summary>
        [JsonProperty(PropertyName = "PaymentMode")]
        public string PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets Total amount
        /// </summary>
        [JsonProperty(PropertyName = "Total")]
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets Invoice Path
        /// </summary>
        [JsonProperty(PropertyName = "InvoicePath")]
        public string InvoicePath { get; set; }

        /// <summary>
        /// Gets or sets Date and Time
        /// </summary>
        [JsonProperty(PropertyName = "DateTime")]
        public string DateTime { get; set; }

        /// <summary>
        /// Gets or sets ChapterId
        /// </summary>
        [JsonProperty(PropertyName = "ChapterId")]
        public int ChapterId { get; set; }

        /// <summary>
        /// Gets or sets ChapterName
        /// </summary>
        [JsonProperty(PropertyName = "ChapterName")]

        public string ChapterName { get; set; }

        /// <summary>
        /// Gets or sets Admin Name
        /// </summary>
        [JsonProperty(PropertyName = "AdminName")]
        public string AdminName { get; set; }

        /// <summary>
        /// Gets or sets ExpiryYear
        /// </summary>
        [JsonProperty(PropertyName = "ExpiryYear")]
        public string ExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets InvoiceId
        /// </summary>
        [JsonProperty(PropertyName = "InvoiceId")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets MembershipId
        /// </summary>
        [JsonProperty(PropertyName = "MembershipId")]
        public string MembershipId { get; set; }

        /// <summary>
        /// Gets or sets GSTIN
        /// </summary>
        [JsonProperty(PropertyName = "GSTIN")]
        public string GSTIN { get; set; }
    }
}