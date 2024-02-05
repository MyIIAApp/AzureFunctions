using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Payment Detail
    /// </summary>
    public class PaymentDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetail"/> class.
        /// </summary>
        /// <param name="admissionFee">Admission Fee</param>
        /// <param name="cgst">CGST</param>
        /// <param name="igst">IGST</param>
        /// <param name="membershipFee">Membeship Fee</param>
        /// <param name="sgst">SGST</param>
        public PaymentDetail(int membershipFee, int cgst, int sgst, int igst, int admissionFee)
        {
            this.MembershipFee = membershipFee;
            this.Cgst = cgst;
            this.Sgst = sgst;
            this.Igst = igst;
            this.AdmissionFee = admissionFee;
        }

        /// <summary>
        /// Gets or sets membershipFee
        /// </summary>
        [JsonProperty(PropertyName = "membershipFee")]
        public int MembershipFee { get; set; }

        /// <summary>
        /// Gets or sets cgst
        /// </summary>
        [JsonProperty(PropertyName = "cgst")]
        public int Cgst { get; set; }

        /// <summary>
        /// Gets or sets sgst
        /// </summary>
        [JsonProperty(PropertyName = "sgst")]
        public int Sgst { get; set; }

        /// <summary>
        /// Gets or sets igst
        /// </summary>
        [JsonProperty(PropertyName = "igst")]
        public int Igst { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        [JsonProperty(PropertyName = "admissionFee")]
        public int AdmissionFee { get; set; }
    }
}