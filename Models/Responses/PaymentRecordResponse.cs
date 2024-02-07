using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// News Response Object
    /// </summary>
    public class PaymentRecordResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRecordResponse"/> class.
        /// </summary>
        /// <param name="paymentrecord">List Payment Record</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public PaymentRecordResponse(List<PaymentRecordModel> paymentrecord, LoginMetadata token, string message)
            : base(token, message)
        {
            this.PaymentRecord = paymentrecord;
        }

        /// <summary>
        /// Gets or sets Payment Record List.
        /// </summary>
        public List<PaymentRecordModel> PaymentRecord { get; set; }
    }
}