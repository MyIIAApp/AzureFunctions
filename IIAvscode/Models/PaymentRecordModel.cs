using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Payment Reason Model
    /// </summary>
    public class PaymentRecordModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRecordModel"/> class.
        /// </summary>
        /// <param name="paymentReason">Payment Reason</param>
        /// <param name="paymentMode">Payment Mode</param>
        /// <param name="total">total amount</param>
        /// <param name="invoicePath">invoice Path</param>
        /// <param name="dateTime">Date and Time</param>
        public PaymentRecordModel(string paymentReason, string paymentMode, string total, string invoicePath, string dateTime)
        {
            this.PaymentReason = paymentReason;
            this.PaymentMode = paymentMode;
            this.Total = total;
            this.InvoicePath = invoicePath;
            this.DateTime = dateTime;
            
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
    }
}