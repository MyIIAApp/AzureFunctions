using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Leader Details Object
    /// </summary>
    public class LeaderDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaderDetails"/> class.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="designation">Designation</param>
        /// <param name="chapterName">ChapterName</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="email">Email</param>
        /// <param name="profilePhoto">ProfilePhoto</param>
        public LeaderDetails(string name, string designation, string chapterName, string phoneNumber, string email, string profilePhoto)
        {
            this.Name = name;
            this.Designation = designation;
            this.ChapterName = chapterName;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.ProfilePhoto = profilePhoto;
        }

        /// <summary>
        /// Gets or sets Name of the LeaderDetails object
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Designation of the LeaderDetails object
        /// </summary>
        [JsonProperty(PropertyName = "Designation")]
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets ChapterName of the LeaderDetails object
        /// </summary>
        [JsonProperty(PropertyName = "ChapterName")]
        public string ChapterName { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber of the LeaderDetails object
        /// </summary>
        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Email of the LeaderDetails object
        /// </summary>
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets Email of the LeaderDetails object
        /// </summary>
        [JsonProperty(PropertyName = "ProfilePhoto")]
        public string ProfilePhoto { get; set; }
    }
}
