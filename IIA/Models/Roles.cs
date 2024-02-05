using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Tickets Object
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Roles"/> class.
        /// </summary>
        /// <param name="createNews">CreateNews</param>
        /// <param name="chaptersHelpdesk">ChaptersHelpdesk</param>
        /// <param name="allHelpdesk">AllHelpdesk</param>
        /// <param name="recordAllPayment">RecordAllPayment</param>
        /// <param name="recordChapterPayment">RecordChapterPayment</param>
        /// <param name="approveAllMembership">ApproveAllMembership</param>
        /// <param name="approveChapterMembership">ApproveChapterMembership</param>
        /// <param name="editUserProfile">EditUserProfile</param>
        /// <param name="createUserProfile">CreateUserProfile</param>
        public Roles(string createNews, string chaptersHelpdesk, string allHelpdesk, string recordAllPayment, string recordChapterPayment, string approveAllMembership, string approveChapterMembership, string editUserProfile, string createUserProfile)
        {
            this.CreateNews = createNews;
            this.ChaptersHelpdesk = chaptersHelpdesk;
            this.AllHelpdesk = allHelpdesk;
            this.RecordAllPayment = recordAllPayment;
            this.RecordChapterPayment = recordChapterPayment;
            this.ApproveAllMembership = approveAllMembership;
            this.ApproveChapterMembership = approveChapterMembership;
            this.EditUserProfile = editUserProfile;
            this.CreateUserProfile = createUserProfile;
        }

        /// <summary>
        /// Gets or sets Role of the Create News
        /// </summary>
        [JsonProperty(PropertyName = "CreateNews")]
        public string CreateNews { get; set; }

        /// <summary>
        /// Gets or sets Title of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "ChaptersHelpdesk")]
        public string ChaptersHelpdesk { get; set; }

        /// <summary>
        /// Gets or sets Description of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "AllHelpdesk")]
        public string AllHelpdesk { get; set; }

        /// <summary>
        /// Gets or sets Category of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "RecordAllPayment")]
        public string RecordAllPayment { get; set; }

        /// <summary>
        /// Gets or sets ChapterId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "RecordChapterPayment")]
        public string RecordChapterPayment { get; set; }

        /// <summary>
        /// Gets or sets Status of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "ApproveAllMembership")]
        public string ApproveAllMembership { get; set; }

        /// <summary>
        /// Gets or sets CreationTime of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "ApproveChapterMembership")]
        public string ApproveChapterMembership { get; set; }

        /// <summary>
        /// Gets or sets UserId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "EditUserProfile")]
        public string EditUserProfile { get; set; }

        /// <summary>
        /// Gets or sets Comment of the Ticket
        /// </summary>
        [JsonProperty(PropertyName = "CreateUserProfile")]
        public string CreateUserProfile { get; set; }
    }
}
