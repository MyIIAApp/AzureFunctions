using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// User class
    /// </summary>
    public class LoginMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginMetadata"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="phoneNumber">phoneNumber</param>
        /// <param name="membershipStatus">membershipStatus</param>
        /// <param name="isAdmin">isAdmin</param>
        /// <param name="chapterName">chapterName</param>
        /// <param name="token">token</param>
        /// <param name="chapterId">ChapterId</param>
        public LoginMetadata(string id, string phoneNumber, string membershipStatus, int chapterId, bool isAdmin, string chapterName, string token)
        {
            this.Id = int.Parse(id);
            this.PhoneNumber = phoneNumber;
            this.MembershipStatus = int.Parse(membershipStatus);
            this.ChapterId = chapterId;
            this.IsAdmin = isAdmin;
            this.ChapterName = chapterName;
            this.TokenString = token;
        }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets membershipStatus
        /// </summary>
        public int MembershipStatus { get; set; }

        /// <summary>
        /// Gets or sets HeadOffice
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// Gets or sets chapterName
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// Gets or sets phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user is isAdmin
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets TokenString
        /// </summary>
        public string TokenString { get; set; }
    }
}
