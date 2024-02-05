using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// B2B Admin Enquiry
    /// </summary>
    public class B2BAdminEnquiry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="B2BAdminEnquiry"/> class.
        /// </summary>
        /// <param name="sellerName">Name of seller</param>
        /// <param name="sellerPhoneNumber">Phone number of seller</param>
        /// <param name="sellerChapterName">Chapter Name of seller</param>
        /// <param name="pendingEnquiries">total number of pending enquiries</param>
        public B2BAdminEnquiry(string sellerName, string sellerPhoneNumber, string sellerChapterName, string pendingEnquiries)
        {
            this.SellerName = sellerName;
            this.SellerPhoneNumber = sellerPhoneNumber;
            this.SellerChapterName = sellerChapterName;
            this.PendingEnquiries = pendingEnquiries;
        }

        /// <summary>
        /// Gets or sets Seller name
        /// </summary>
        [JsonProperty(PropertyName = "SellerName")]
        public string SellerName { get; set; }

        /// <summary>
        /// Gets or sets Seller phone number
        /// </summary>
        [JsonProperty(PropertyName = "SellerPhoneNumber")]
        public string SellerPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Seller's Chapter name
        /// </summary>
        [JsonProperty(PropertyName = "SellerChapterName")]
        public string SellerChapterName { get; set; }

        /// <summary>
        /// Gets or sets Total number of pending enquiries
        /// </summary>
        [JsonProperty(PropertyName = "PendingEnquiries")]
        public string PendingEnquiries { get; set; }
    }
}