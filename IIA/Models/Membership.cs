using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// MembershipProfile  class
  /// </summary>
    public class Membership
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Membership"/> class.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="firstName">FirstName</param>
        /// <param name="lastName">LastName</param>
        /// <param name="email">Email</param>
        /// <param name="membershipId">MembershipId</param>
        /// <param name="chapterId">chapterId</param>
        /// <param name="chapterName">chapterName</param>
        /// <param name="membershipAdmissionFee">MemberAdmissionFee</param>
        /// <param name="membershipFees">MembershipFees</param>
        /// <param name="membershipExpiryYear">MembershipExpiryYear</param>
        /// <param name="membershipRenewDate">membershipRenewDate</param>
        /// <param name="createdBy">CreatedBy</param>
        /// <param name="membershipJoinDate">FirstLoginTime</param>
        public Membership(int userId, string firstName, string lastName, string email, string membershipId,  int chapterId, string chapterName, double membershipAdmissionFee, double membershipFees, int membershipExpiryYear, DateTime membershipRenewDate, DateTime membershipJoinDate, int createdBy)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.MembershipId = membershipId;
            this.ChapterId = chapterId;
            this.ChapterName = chapterName;
            this.MembershipAdmissionfee = membershipAdmissionFee;
            this.MembershipFees = membershipFees;
            this.MembershipCurrentExpiryYear = membershipExpiryYear;
            this.MembershipJoinDate = membershipJoinDate;
            this.MembershipRenewDate = membershipRenewDate;
            this.CreatedBy = createdBy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Membership"/> class only with id
        /// </summary>
        /// <param name="data">data</param>
        /// <param name="id">Id</param>
        public Membership(dynamic data)
        {
            this.UserId = data?.userId;
            this.MembershipId = data?.membershipId;
            this.MembershipAdmissionfee = data?.membershipAdmissionfee != null ? data.membershipAdmissionfee : 0;
            this.MembershipFees = data?.membershipFees != null ? data.membershipFees : data?.fullAmount;
            this.MembershipRenewDate = data?.membershipRenewDate != null ? data.membershipRenewDate : DateTime.Today;
            this.MembershipCurrentExpiryYear = this.MembershipRenewDate.Month > 3 ? this.MembershipRenewDate.AddYears(1).Year : this.MembershipRenewDate.Year;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Membership"/> class only with id
        /// </summary>
        /// <param name="id">Id</param>
        public Membership(int id)
        {
            this.UserId = id;
        }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets membership id
        /// </summary>
        [JsonProperty(PropertyName = "membershipId")]
        public string MembershipId { get; set; }

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
        /// Gets or sets MembershipAdmissionfee
        /// </summary>
        [JsonProperty(PropertyName = "membershipAdmissionfee")]
        public double MembershipAdmissionfee { get; set; }

        /// <summary>
        /// Gets or sets memberFees
        /// </summary>
        [JsonProperty(PropertyName = "membershipFees")]
        public double MembershipFees { get; set; }

        /// <summary>
        /// Gets or sets MembershipCurrentExpiryYear
        /// </summary>
        [JsonProperty(PropertyName = "membershipCurrentExpiryYear")]
        public int MembershipCurrentExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        [JsonProperty(PropertyName = "createdBy")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets created_dt
        /// </summary>
        [JsonProperty(PropertyName = "membershipJoinDate")]
        public DateTime MembershipJoinDate { get; set; }

        /// <summary>
        /// Gets or sets MembershipRenewDate
        /// </summary>
        [JsonProperty(PropertyName = "membershipRenewDate")]
        public DateTime MembershipRenewDate { get; set; }
    }
}
