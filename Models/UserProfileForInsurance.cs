using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// UserCompanyProfile  class
  /// </summary>
    public class UserProfileForInsurance
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileForInsurance"/> class.
        /// </summary>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="membershipId">MembershipId</param>
        /// <param name="membershipCurrentExpiryYear">MembershipCurrentExpiryYear</param>
        /// <param name="membershipJoinDate">MembershipJoinDate</param>
        /// <param name="membershipRenewDate">MembershipRenewDate</param>
        /// <param name="membershipExpiryYears">MembershipExpiryYears</param>
        /// <param name="chapterName">chapterName</param>
        /// <param name="unitName">UnitName</param>
        /// <param name="gstin">GSTIN</param>
        /// <param name="address">Address</param>
        /// <param name="district">District</param>
        /// <param name="city">City</param>
        /// <param name="state">State</param>
        /// <param name="country">Country</param>
        /// <param name="pincode">Pincode</param>
        /// <param name="firstName">firstName</param>
        /// <param name="lastName">lastName</param>
        /// <param name="email">email</param>
        public UserProfileForInsurance(string phoneNumber, string membershipId, int membershipCurrentExpiryYear, DateTime membershipJoinDate, DateTime membershipRenewDate, string membershipExpiryYears, string chapterName, string unitName, string gstin, string address, string district, string city, string state, string country, string pincode, string firstName, string lastName, string email)
        {
            this.PhoneNumber = phoneNumber;
            this.MembershipId = membershipId;
            this.MembershipCurrentExpiryYear = membershipCurrentExpiryYear;
            this.MembershipJoinDate = membershipJoinDate;
            this.MembershipRenewDate = membershipRenewDate;
            this.MembershipExpiryYears = membershipExpiryYears;
            this.ChapterName = chapterName;
            this.UnitName = unitName;
            this.GSTIN = gstin;
            this.Address = address;
            this.District = district;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Pincode = pincode;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets MembershipId
        /// </summary>
        [JsonProperty(PropertyName = "membershipId")]
        public string MembershipId { get; set; }

        /// <summary>
        /// Gets or sets MembershipCurrentExpiryYear
        /// </summary>
        [JsonProperty(PropertyName = "membershipCurrentExpiryYear")]
        public int MembershipCurrentExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets MembershipJoinDate
        /// </summary>
        [JsonProperty(PropertyName = "membershipJoinDate")]
        public DateTime MembershipJoinDate { get; set; }

        /// <summary>
        /// Gets or sets MembershipRenewDate
        /// </summary>
        [JsonProperty(PropertyName = "membershipRenewDate")]
        public DateTime MembershipRenewDate { get; set; }

        /// <summary>
        /// Gets or sets MembershipExpiryYears
        /// </summary>
        [JsonProperty(PropertyName = "membershipExpiryYears")]
        public string MembershipExpiryYears { get; set; }

        /// <summary>
        /// Gets or sets Chapter
        /// </summary>
        [JsonProperty(PropertyName = "chapterName")]
        public string ChapterName { get; set; }

        /// <summary>
        /// Gets or sets UnitName
        /// </summary>
        [JsonProperty(PropertyName = "unitName")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets GSTIN
        /// </summary>
        [JsonProperty(PropertyName = "gstin")]
        public string GSTIN { get; set; }

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
        /// Gets or sets FirstName
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets LastName
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Email
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
