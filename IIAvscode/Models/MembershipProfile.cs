using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// UserCompanyProfile  class
  /// </summary>
    public class MembershipProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipProfile"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="profileStatus">ProfileStatus</param>
        /// <param name="chapterId">ChapterId</param>
        /// <param name="chapterName">chapterName</param>
        /// <param name="unitName">UnitName</param>
        /// <param name="gstin">GSTIN</param>
        /// <param name="gstCertPath">GSTcertpath</param>
        /// <param name="industryStatus">IndustryStatus</param>
        /// <param name="address">Address</param>
        /// <param name="district">District</param>
        /// <param name="city">City</param>
        /// <param name="state">State</param>
        /// <param name="country">Country</param>
        /// <param name="pincode">Pincode</param>
        /// <param name="websiteUrl">WebsiteUrl</param>
        /// <param name="productCategory">ProductCategory</param>
        /// <param name="productSubCategory">ProductSubCategory</param>
        /// <param name="majorProducts">MajorProducts</param>
        /// <param name="annualTurnOver">AnnualTurnOver</param>
        /// <param name="enterpriseType">EnterpriseType</param>
        /// <param name="exporter">Exporter</param>
        /// <param name="classification">Classification</param>
        /// <param name="firstName">firstName</param>
        /// <param name="lastName">lastName</param>
        /// <param name="email">email</param>
        /// <param name="profileImagePath">profileImagePath</param>
        /// <param name="financialProofPath">financialProofPath</param>
        /// <param name="signaturePath">signaturePath</param>
        /// <param name="createdBy">createdBy</param>
        /// <param name="createdDate">CreatedDate</param>
        /// <param name="updatedDate">UpdatedDate</param>
        public MembershipProfile(int id, int profileStatus, int chapterId, string chapterName, string unitName, string gstin, string gstCertPath, string industryStatus, string address, string district, string city, string state, string country, string pincode, string websiteUrl, string productCategory, string productSubCategory, string majorProducts, string annualTurnOver, string enterpriseType, string exporter, string classification,  string firstName, string lastName, string email, string profileImagePath, string financialProofPath, string signaturePath, int createdBy, DateTime createdDate, DateTime updatedDate)
        {
            this.Id = id;
            this.ProfileStatus = profileStatus;
            this.ChapterId = chapterId;
            this.ChapterName = chapterName;
            this.UnitName = unitName;
            this.GSTIN = gstin;
            this.GSTcertpath = gstCertPath;
            this.IndustryStatus = industryStatus;
            this.Address = address;
            this.District = district;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Pincode = pincode;
            this.WebsiteUrl = websiteUrl;
            this.ProductCategory = productCategory;
            this.ProductSubCategory = productSubCategory;
            this.MajorProducts = majorProducts;
            this.AnnualTurnOver = annualTurnOver;
            this.EnterpriseType = enterpriseType;
            this.Exporter = exporter;
            this.Classification = classification;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.ProfileImagePath = profileImagePath;
            this.FinancialProofPath = financialProofPath;
            this.SignaturePath = signaturePath;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.UpdatedDate = updatedDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipProfile"/> class only with id
        /// </summary>
        /// <param name="id">Id</param>
        public MembershipProfile(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipProfile"/> class only with id
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="id">Id</param>
        public MembershipProfile(dynamic data, int id)
        {
            this.Id = id;
            this.ProfileStatus = data?.profileStatus != null ? data.profileStatus : 0;
            this.ChapterId = data?.chapterId != null ? data.chapterId : 0;
            this.ChapterName = data?.chapterName;
            this.UnitName = data?.unitName;
            this.GSTIN = data?.gstin;
            this.GSTcertpath = data?.gstCertPath;
            this.IndustryStatus = data?.industryStatus;
            this.Address = data?.address;
            this.District = data?.district;
            this.City = data?.city;
            this.State = data?.state;
            this.Country = data?.country;
            this.Pincode = data?.pincode;
            this.WebsiteUrl = data?.websiteUrl;
            this.ProductCategory = data?.productCategory;
            this.ProductSubCategory = data?.productSubCategory;
            this.MajorProducts = data?.majorProducts;
            this.AnnualTurnOver = data?.annualTurnOver;
            this.EnterpriseType = data?.enterpriseType;
            this.Exporter = data?.exporter;
            this.Classification = data?.classification;
            this.FirstName = data?.firstName;
            this.LastName = data?.lastName;
            this.Email = data?.email;
            this.ProfileImagePath = data?.profileImagePath;
            this.FinancialProofPath = data?.financialProofPath;
            this.SignaturePath = data?.signaturePath;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets  id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets ProfileStatus
        /// </summary>
        [JsonProperty(PropertyName = "profileStatus")]
        public int ProfileStatus { get; set; }

        /// <summary>
        /// Gets or sets ChapterId
        /// </summary>
        [JsonProperty(PropertyName = "chapterId")]
        public int ChapterId { get; set; }

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
        /// Gets or sets GSTcertpath
        /// </summary>
        [JsonProperty(PropertyName = "gstcertpath")]
        public string GSTcertpath { get; set; }

        /// <summary>
        /// Gets or sets industryStatus
        /// </summary>
        [JsonProperty(PropertyName = "industryStatus")]
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
        [JsonProperty(PropertyName = "websiteUrl")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets ProductCategory
        /// </summary>
        [JsonProperty(PropertyName = "productCategory")]
        public string ProductCategory { get; set; }

        /// <summary>
        /// Gets or sets ProductSubCategory
        /// </summary>
        [JsonProperty(PropertyName = "productSubCategory")]
        public string ProductSubCategory { get; set; }

        /// <summary>
        /// Gets or sets MajorProducts
        /// </summary>
        [JsonProperty(PropertyName = "majorProducts")]
        public string MajorProducts { get; set; }

        /// <summary>
        /// Gets or sets AnnualTurnOver
        /// </summary>
        [JsonProperty(PropertyName = "annualTurnOver")]
        public string AnnualTurnOver { get; set; }

        /// <summary>
        /// Gets or sets EnterpriseType
        /// </summary>
        [JsonProperty(PropertyName = "enterpriseType")]
        public string EnterpriseType { get; set; }

        /// <summary>
        /// Gets or sets Exporter
        /// </summary>
        [JsonProperty(PropertyName = "exporter")]
        public string Exporter { get; set; }

        /// <summary>
        /// Gets or sets Classification
        /// </summary>
        [JsonProperty(PropertyName = "classification")]
        public string Classification { get; set; }

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

        /// <summary>
        /// Gets or sets ProfileImagePath
        /// </summary>
        [JsonProperty(PropertyName = "profileImagePath")]
        public string ProfileImagePath { get; set; }

        /// <summary>
        /// Gets or sets FinancialProofPath
        /// </summary>
        [JsonProperty(PropertyName = "financialProofPath")]
        public string FinancialProofPath { get; set; }

        /// <summary>
        /// Gets or sets SignaturePath
        /// </summary>
        [JsonProperty(PropertyName = "signaturePath")]
        public string SignaturePath { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        [JsonProperty(PropertyName = "createdBy")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets CreatedDate
        /// </summary>
        [JsonProperty(PropertyName = "createdDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets UpdatedDate
        /// </summary>
        [JsonProperty(PropertyName = "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}
