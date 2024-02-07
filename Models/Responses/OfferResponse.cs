using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Offer Response Object
    /// </summary>
    public class OfferResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferResponse"/> class.
        /// </summary>
        /// <param name="offer">List of offers</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public OfferResponse(List<dynamic> offer, LoginMetadata token, string message)
            : base(token, message)
        {
            this.Offer = offer;
        }

        /// <summary>
        /// Gets or sets Offers.
        /// </summary>
        public List<dynamic> Offer { get; set; }
    }
}
