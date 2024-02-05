using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Ofers Object
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Offer"/> class.
        /// </summary>
        /// <param name="sNo">SNo</param>
        /// <param name="name">Name</param>
        /// <param name="offersSummary">OfferSummary</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="emailId">EmailId</param>
        /// <param name="address">Address</param>
        /// <param name="website">Website</param>
        /// <param name="city">City</param>
        /// <param name="category">Category</param>
        /// <param name="bulletPoint1">BulletPoint1</param>
        /// <param name="bulletPoint2">BulletPoint2</param>
        /// <param name="bulletPoint3">BulletPoint3</param>
        /// <param name="bulletPoint4">BulletPoint4</param>
        /// <param name="bulletPoint5">BulletPoint5</param>
        /// <param name="documentName">DocumentName</param>
        /// <param name="prospectusPath">ProspectusPath</param>
        public Offer(string sNo, string name, string offersSummary, string phoneNumber, string emailId, string address, string website, string city, string category, string bulletPoint1, string bulletPoint2, string bulletPoint3, string bulletPoint4, string bulletPoint5, string documentName, string prospectusPath)
        {
            this.SNo = sNo;
            this.Name = name;
            this.OfferSummary = offersSummary;
            this.PhoneNumber = phoneNumber;
            this.EmailId = emailId;
            this.Address = address;
            this.Website = website;
            this.City = city;
            this.Category = category;
            this.BulletPoint1 = bulletPoint1;
            this.BulletPoint2 = bulletPoint2;
            this.BulletPoint3 = bulletPoint3;
            this.BulletPoint4 = bulletPoint4;
            this.BulletPoint5 = bulletPoint5;
            this.DocumentName = documentName;
            this.ProspectusPath = prospectusPath;
        }

        /// <summary>
        /// Gets or sets sno of the offers
        /// </summary>
        [JsonProperty(PropertyName = "SNo")]
        public string SNo { get; set; }

        /// <summary>
        /// Gets or sets Name of the offers
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get;  set; }

        /// <summary>
        /// Gets or sets OfferSummary of the offers
        /// </summary>
        [JsonProperty(PropertyName = "OfferSummary")]
        public string OfferSummary { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber of the offers
        /// </summary>
        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets OrganisationAddress of the offers
        /// </summary>
        [JsonProperty(PropertyName = "Address")]
        public string Address { get;  set; }

        /// <summary>
        /// Gets or sets City of the offers
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get;  set; }

        /// <summary>
        /// Gets or sets Email of the offers
        /// </summary>
        [JsonProperty(PropertyName = "EmailId")]
        public string EmailId { get;  set; }

        /// <summary>
        /// Gets or sets Website of the offers
        /// </summary>
        [JsonProperty(PropertyName = "Website")]
        public string Website { get;  set; }

        /// <summary>
        /// Gets or sets Bulletpoint1 of the offers
        /// </summary>
        [JsonProperty(PropertyName = "BulletPoint1")]
        public string BulletPoint1 { get; set; }

        /// <summary>
        /// Gets or sets Bulletpoint2 of the offers
        /// </summary>
        [JsonProperty(PropertyName = "BulletPoint2")]
        public string BulletPoint2 { get; set; }

        /// <summary>
        /// Gets or sets Bulletpoint3 of the offers
        /// </summary>
        [JsonProperty(PropertyName = "BulletPoint3")]
        public string BulletPoint3 { get; set; }

        /// <summary>
        /// Gets or sets Bulletpoint4 of the offers
        /// </summary>
        [JsonProperty(PropertyName = "BulletPoint4")]
        public string BulletPoint4 { get; set; }

        /// <summary>
        /// Gets or sets Bulletpoint5 of the offers
        /// </summary>
        [JsonProperty(PropertyName = "BulletPoint5")]
        public string BulletPoint5 { get; set; }

        /// <summary>
        /// Gets or sets ProspectusPath of the offers
        /// </summary>
        [JsonProperty(PropertyName = "ProspectusPath")]
        public string ProspectusPath { get; set; }

        /// <summary>
        /// Gets or sets CategoryName of the offers
        /// </summary>
        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets documentname of the offers
        /// </summary>
        [JsonProperty(PropertyName = "DocumentName")]
        public string DocumentName { get; set; }
    }
}
