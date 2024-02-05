using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Ofers Object
    /// </summary>
    public class PaymentUserId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentUserId"/> class.
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="userId">UserId</param>
        /// <param name="state">State</param>
        /// <param name="annualTurnOver">AnnualTurnOver</param>
        /// <param name="gstin">GSTIN</param>
        public PaymentUserId(string phoneNumber, int userId, string state, string annualTurnOver, string gstin)
        {
            this.PhoneNumber = phoneNumber;
            this.UserId = userId;
            this.State = state;
            this.AnnualTurnOver = annualTurnOver;
            this.GSTIN = gstin;
        }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets UserId
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public int UserId { get;  set; }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get;  set; }

        /// <summary>
        /// Gets or sets AnnualTurnOver
        /// </summary>
        [JsonProperty(PropertyName = "AnnualTurnOver")]
        public string AnnualTurnOver { get;  set; }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        [JsonProperty(PropertyName = "gstin")]
        public string GSTIN { get;  set; }
    }
}
