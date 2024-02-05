using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// UserCompanyProfile  class
  /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="membershipId">MembershipId</param>
        /// <param name="membershipAdmissionfee">MembershipAdmissionfee</param>
        /// <param name="membershipFees">MembershipFees</param>
        /// <param name="membershipCurrentExpiryYear">MembershipCurrentExpiryYear</param>
        /// <param name="membershipJoinDate">MembershipJoinDate</param>
        /// <param name="membershipRenewDate">MembershipRenewDate</param>
        /// <param name="membershipExpiryYears">MembershipExpiryYears</param>
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
        /// <param name="dateOfBirth">dateOfBirth</param>
        /// <param name="dateOfMarriage">dateOfMarriage</param>
        /// <param name="profileImagePath">profileImagePath</param>
        /// <param name="financialProofPath">financialProofPath</param>
        /// <param name="signaturePath">signaturePath</param>
        /// <param name="createdBy">createdBy</param>
        /// <param name="updatedBy">UpdatedBy</param>
        /// <param name="createdDate">CreatedDate</param>
        /// <param name="updatedDate">UpdatedDate</param>
        public UserProfile(int id, string phoneNumber, string membershipId, double membershipAdmissionfee, double membershipFees, int membershipCurrentExpiryYear, DateTime membershipJoinDate, DateTime membershipRenewDate, string membershipExpiryYears, int profileStatus, int chapterId, string chapterName, string unitName, string gstin, string gstCertPath, string industryStatus, string address, string district, string city, string state, string country, string pincode, string websiteUrl, string productCategory, string productSubCategory, string majorProducts, string annualTurnOver, string enterpriseType, string exporter, string classification,  string firstName, string lastName, string email, DateTime? dateOfBirth, DateTime? dateOfMarriage, string profileImagePath, string financialProofPath, string signaturePath, int createdBy, int updatedBy, DateTime createdDate, DateTime updatedDate)
        {
            this.Id = id;
            this.PhoneNumber = phoneNumber;
            this.MembershipId = membershipId;
            this.MembershipAdmissionfee = membershipAdmissionfee;
            this.MembershipFees = membershipFees;
            this.MembershipCurrentExpiryYear = membershipCurrentExpiryYear;
            this.MembershipJoinDate = membershipJoinDate;
            this.MembershipRenewDate = membershipRenewDate;
            this.MembershipExpiryYears = membershipExpiryYears;
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
            this.DateOfBirth = dateOfBirth;
            this.DateOfMarriage = dateOfMarriage;
            this.ProfileImagePath = profileImagePath;
            this.FinancialProofPath = financialProofPath;
            this.SignaturePath = signaturePath;
            this.CreatedBy = createdBy;
            this.UpdatedBy = updatedBy;
            this.CreatedDate = createdDate;
            this.UpdatedDate = updatedDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class only with id
        /// </summary>
        /// <param name="id">Id</param>
        public UserProfile(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class only with id
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="id">Id</param>
        public UserProfile(dynamic data, int id)
        {
            this.Id = id;
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
            string dob = data?.dateOfBirth;
            string dom = data?.dateOfMarriage;
            this.DateOfBirth = FunctionUtility.ConvertDate(dob);
            this.DateOfMarriage = FunctionUtility.ConvertDate(dom);
            this.ProfileImagePath = data?.profileImagePath;
            this.FinancialProofPath = data?.financialProofPath;
            this.SignaturePath = data?.signaturePath;
            this.UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets  id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

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
        /// Gets or sets MembershipAdmissionfee
        /// </summary>
        [JsonProperty(PropertyName = "membershipAdmissionfee")]
        public double MembershipAdmissionfee { get; set; }

        /// <summary>
        /// Gets or sets MembershipFees
        /// </summary>
        [JsonProperty(PropertyName = "membershipFees")]
        public double MembershipFees { get; set; }

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
        [JsonProperty(PropertyName = "gstCertPath")]
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
        /// Gets or sets dateOfBirth
        /// </summary>
        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets dateOfMarriage
        /// </summary>
        [JsonProperty(PropertyName = "dateOfMarriage")]
        public DateTime? DateOfMarriage { get; set; }

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
        /// Gets or sets UpdatedBy
        /// </summary>
        [JsonProperty(PropertyName = "updatedBy")]
        public int UpdatedBy { get; set; }

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
