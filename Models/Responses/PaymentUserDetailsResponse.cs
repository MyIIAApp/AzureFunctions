using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Payment User Details Response Object
    /// </summary>
    public class PaymentUserDetailsResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentUserDetailsResponse"/> class.
        /// </summary>
        /// <param name="details">List of news</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public PaymentUserDetailsResponse(MembershipProfile details, LoginMetadata token, string message)
            : base(token, message)
        {
            this.Details = details;
        }

        /// <summary>
        /// Gets or sets News.
        /// </summary>
        public MembershipProfile Details { get; set; }
    }
}
