using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Membership Dashboard Model
    /// </summary>
    public class MembershipDashboardModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipDashboardModel"/> class.
        /// </summary>
        /// <param name="totalMembers">Total number of members</param>
        /// <param name="totalMembersChapterLevel">Total number of members for a chapter</param>
        /// <param name="activeMembers">Total number of active members</param>
        /// <param name="membersUnderGracePeriod">Total number of members under grace period</param>
        /// <param name="expiredMemberships">Total number of memberships expired</param>
        /// <param name="usersLoggedinButNotMembers">Total Users who have logged in but not members</param>
        /// <param name="newMembers">Total number of members added in current financial year</param>
        /// <param name="pendingRequests">Total number of requests pending</param>
        /// <param name="approvedMemberPaymentPending">New approved members but payment pending</param>
        public MembershipDashboardModel(string totalMembers, string totalMembersChapterLevel, string activeMembers, string membersUnderGracePeriod, string expiredMemberships, string usersLoggedinButNotMembers, string newMembers, string pendingRequests, string approvedMemberPaymentPending)
        {
            this.TotalMembers = totalMembers;
            this.TotalMembersChapterLevel = totalMembersChapterLevel;
            this.ActiveMembers = activeMembers;
            this.MembersUnderGracePeriod = membersUnderGracePeriod;
            this.ExpiredMemberships = expiredMemberships;
            this.UsersLoggedinButNotMembers = usersLoggedinButNotMembers;
            this.NewMembers = newMembers;
            this.PendingRequests = pendingRequests;
            this.ApprovedMemberPaymentPending = approvedMemberPaymentPending;
        }

        /// <summary>
        /// Gets or sets total number of members
        /// </summary>
        [JsonProperty(PropertyName = "TotalMembers")]
        public string TotalMembers { get; set; }

        /// <summary>
        /// Gets or sets total number of members for a chapter
        /// </summary>
        [JsonProperty(PropertyName = "TotalMembersChapterLevel")]
        public string TotalMembersChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets number of active members
        /// </summary>
        [JsonProperty(PropertyName = "Activemembers")]
        public string ActiveMembers { get; set; }

        /// <summary>
        /// Gets or sets number of members under grace period
        /// </summary>
        [JsonProperty(PropertyName = "MembersUnderGracePeriod")]
        public string MembersUnderGracePeriod { get; set; }

        /// <summary>
        /// Gets or sets number of expired memberships
        /// </summary>
        [JsonProperty(PropertyName = "ExpiredMemberships")]
        public string ExpiredMemberships { get; set; }

        /// <summary>
        /// Gets or sets number of users Logged in but not members
        /// </summary>
        [JsonProperty(PropertyName = "UsersLoggedinButNotMembers")]
        public string UsersLoggedinButNotMembers { get; set; }

        /// <summary>
        /// Gets or sets number of new members in current financial year
        /// </summary>
        [JsonProperty(PropertyName = "NewMembers")]
        public string NewMembers { get; set; }

        /// <summary>
        /// Gets or sets number of pending requests
        /// </summary>
        [JsonProperty(PropertyName = "PendingRequests")]
        public string PendingRequests { get; set; }

        /// <summary>
        /// Gets or sets number of approved member pending payment
        /// </summary>
        [JsonProperty(PropertyName = "ApprovedmemberPaymentPending")]
        public string ApprovedMemberPaymentPending { get; set; }
    }
}
