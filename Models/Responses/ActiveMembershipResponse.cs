using System;
using Newtonsoft.Json;

namespace IIABackend
{ /// <summary>
  /// MembershipProfile  class
  /// </summary>
    public class ActiveMembershipResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveMembershipResponse"/> class.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="firstName">FirstName</param>
        /// <param name="lastName">LastName</param>
        /// <param name="email">Email</param>
        /// <param name="membershipId">MembershipId</param>
        /// <param name="chapterId">chapterId</param>
        /// <param name="chapterName">chapterName</param>
        /// <param name="membershipFees">MembershipFees</param>
        /// <param name="membershipExpiryDate">MembershipExpiryDate</param>
        /// <param name="membershipSince">MembershipSince</param>
        /// <param name="membershipStatus">MembershipStatus</param>
        public ActiveMembershipResponse(int userId, string firstName, string lastName, string email, string membershipId,  int chapterId, string chapterName, double membershipFees, DateTime membershipExpiryDate, DateTime membershipSince, int membershipStatus)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.MembershipId = membershipId;
            this.ChapterId = chapterId;
            this.ChapterName = chapterName;
            this.MembershipFees = membershipFees;
            this.MembershipExpiryDate = membershipExpiryDate;
            this.MembershipSince = membershipSince;
            this.MembershipStatus = membershipStatus;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveMembershipResponse"/> class only with id
        /// </summary>
        /// <param name="id">Id</param>
        public ActiveMembershipResponse(int id)
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
        /// Gets or sets memberFees
        /// </summary>
        [JsonProperty(PropertyName = "membershipFees")]
        public double MembershipFees { get; set; }

        /// <summary>
        /// Gets or sets memberExpiry
        /// </summary>
        [JsonProperty(PropertyName = "membershipExpiryDate")]
        public DateTime MembershipExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets created_dt
        /// </summary>
        [JsonProperty(PropertyName = "membershipSince")]
        public DateTime MembershipSince { get; set; }

        /// <summary>
        /// Gets or sets updated_dt
        /// </summary>
        [JsonProperty(PropertyName = "membershipStatus")]
        public int MembershipStatus { get; set; }
    }
}
