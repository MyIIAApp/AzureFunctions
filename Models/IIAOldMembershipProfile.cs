using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// UserCompanyProfile  class
  /// </summary>
    public class IIAOldMembershipProfile
    {
        /// <summary>
        /// Gets or sets  id
        /// </summary>
        [JsonProperty(PropertyName = "no")]
        public int No { get; set; }

        /// <summary>
        /// Gets or sets ProfileStatus
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public int ProfileStatus { get; set; }

        /// <summary>
        /// Gets or sets ChapterId
        /// </summary>
        [JsonProperty(PropertyName = "chapter")]
        public string ChapterId { get; set; }

        /// <summary>
        /// Gets or sets Chapter
        /// </summary>
        [JsonProperty(PropertyName = "ChapterName")]
        public string ChapterName { get; set; }

        /// <summary>
        /// Gets or sets UnitName
        /// </summary>
        [JsonProperty(PropertyName = "unit")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets GSTIN
        /// </summary>
        [JsonProperty(PropertyName = "gstin")]
        public string GSTIN { get; set; }

        /// <summary>
        /// Gets or sets GSTcertpath
        /// </summary>
        [JsonProperty(PropertyName = "gstCertPath")]
        public string GSTcertpath { get; set; }

        /// <summary>
        /// Gets or sets industryStatus
        /// </summary>
        [JsonProperty(PropertyName = "IndustryStatus")]
        public string IndustryStatus { get; set; }

        /// <summary>
        /// Gets or sets address
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets District
        /// </summary>
        [JsonProperty(PropertyName = "district")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets City
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets State
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets Country
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets Pincode
        /// </summary>
        [JsonProperty(PropertyName = "pincode")]
        public string Pincode { get; set; }

        /// <summary>
        /// Gets or sets WebsiteUrl
        /// </summary>
        [JsonProperty(PropertyName = "website")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets ProductCategory
        /// </summary>
        [JsonProperty(PropertyName = "Category")]
        public string ProductCategory { get; set; }

        /// <summary>
        /// Gets or sets ProductSubCategory
        /// </summary>
        [JsonProperty(PropertyName = "subcategory")]
        public string ProductSubCategory { get; set; }

        /// <summary>
        /// Gets or sets MajorProducts
        /// </summary>
        [JsonProperty(PropertyName = "product")]
        public string MajorProducts { get; set; }

        /// <summary>
        /// Gets or sets AnnualTurnOver
        /// </summary>
        [JsonProperty(PropertyName = "AnnualTurnOver")]
        public string AnnualTurnOver { get; set; }

        /// <summary>
        /// Gets or sets EnterpriseType
        /// </summary>
        [JsonProperty(PropertyName = "EnterpriseType")]
        public string EnterpriseType { get; set; }

        /// <summary>
        /// Gets or sets Exporter
        /// </summary>
        [JsonProperty(PropertyName = "exporter_type")]
        public string Exporter { get; set; }

        /// <summary>
        /// Gets or sets Classification
        /// </summary>
        [JsonProperty(PropertyName = "EnterpriseClassification")]
        public string Classification { get; set; }

        /// <summary>
        /// Gets or sets Owner
        /// </summary>
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets ApplicantName
        /// </summary>
        [JsonProperty(PropertyName = "ApplicantName")]
        public string ApplicantName { get; set; }

        /// <summary>
        /// Gets or sets Email
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets MemberId
        /// </summary>
        [JsonProperty(PropertyName = "memberId")]
        public string MemberId { get; set; }

        /// <summary>
        /// Gets or sets MemberJoinDate
        /// </summary>
        [JsonProperty(PropertyName = "join_date")]
        public string MemberJoinDate { get; set; }

        /// <summary>
        /// Gets or sets Lastrenewaldate
        /// </summary>
        [JsonProperty(PropertyName = "lastrenewaldate")]
        public string Lastrenewaldate { get; set; }

        /// <summary>
        /// Gets or sets Expirydate
        /// </summary>
        [JsonProperty(PropertyName = "expiry_date")]
        public string Expirydate { get; set; }

        /// <summary>
        /// Gets or sets MemberSatus
        /// </summary>
        [JsonProperty(PropertyName = "MemberSatus")]
        public string MemberSatus { get; set; }

        /// <summary>
        /// Gets or sets Mobile
        /// </summary>
        [JsonProperty(PropertyName = "mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets ProfileImage
        /// </summary>
        [JsonProperty(PropertyName = "ProfileImage")]
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets SignatureImage
        /// </summary>
        [JsonProperty(PropertyName = "SignatureImage")]
        public string SignatureImage { get; set; }

        /// <summary>
        /// Gets or sets Admissionfee
        /// </summary>
        [JsonProperty(PropertyName = "admissionfee")]
        public string Admissionfee { get; set; }

        /// <summary>
        /// Gets or sets Annualsubscription
        /// </summary>
        [JsonProperty(PropertyName = "annualsubscription")]
        public string Annualsubscription { get; set; }
    }
}
