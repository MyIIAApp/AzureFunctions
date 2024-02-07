using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Payment Record All Invoice Response Object
    /// </summary>
    public class PaymentRecordAllInvoiceResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRecordAllInvoiceResponse"/> class.
        /// </summary>
        /// <param name="paymentrecord">List Payment Record</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public PaymentRecordAllInvoiceResponse(List<PaymentRecordAllInvoiceModel> paymentrecord, LoginMetadata token, string message)
            : base(token, message)
        {
            this.PaymentRecord = paymentrecord;
        }

        /// <summary>
        /// Gets or sets Payment Record List.
        /// </summary>
        public List<PaymentRecordAllInvoiceModel> PaymentRecord { get; set; }
    }
}