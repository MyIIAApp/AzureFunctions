using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// IIA Directory Object
    /// </summary>
    public class IIADirectory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IIADirectory"/> class.
        /// </summary>
        /// <param name="phoneNumber">phone number</param>
        /// <param name="membershipId">membership id</param>
        /// <param name="chapter">chapter</param>
        /// <param name="classification">classification</param>
        /// <param name="email">email</param>
        /// <param name="enterpriseType">enterprise type</param>
        /// <param name="firstName">first name</param>
        /// <param name="industryStatus">industry status</param>
        /// <param name="lastName">last name</param>
        /// <param name="majorProducts">major products</param>
        /// <param name="productCategory">product category</param>
        /// <param name="productSubCategory">product sub category</param>
        /// <param name="unitName">unit name</param>
        /// <param name="websiteUrl">website url</param>
        /// <param name="profileImagePath">ProfileImagePath</param>
        public IIADirectory(string phoneNumber, string membershipId, string chapter, string unitName, string industryStatus, string websiteUrl, string productCategory, string productSubCategory, string majorProducts, string enterpriseType, string classification, string firstName, string lastName, string email, string profileImagePath)
        {
            this.PhoneNumber = phoneNumber;
            this.MembershipId = membershipId;
            this.Chapter = chapter;
            this.UnitName = unitName;
            this.IndustryStatus = industryStatus;
            this.WebsiteUrl = websiteUrl;
            this.ProductCategory = productCategory;
            this.ProductSubCategory = productSubCategory;
            this.MajorProducts = majorProducts;
            this.EnterpriseType = enterpriseType;
            this.Classification = classification;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ProfileImagePath = profileImagePath;
        }

        /// <summary>
        /// Gets or sets Phone Number
        /// </summary>
        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Membership Id
        /// </summary>
        [JsonProperty(PropertyName = "MembershipId")]
        public string MembershipId { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "Chapter")]
        public string Chapter { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "UnitName")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "IndustryStatus")]
        public string IndustryStatus { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "WebsiteUrl")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "ProductCategory")]
        public string ProductCategory { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "ProductSubCategory")]
        public string ProductSubCategory { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "MajorProducts")]
        public string MajorProducts { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "EnterpriseType")]
        public string EnterpriseType { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "Classification")]
        public string Classification { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets ProfileImagePath
        /// </summary>
        [JsonProperty(PropertyName = "ProfileImagePath")]
        public string ProfileImagePath { get; set; }
    }
}