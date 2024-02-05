using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Chapters Object
    /// </summary>
    public class UserProfileStatusEnum
    {
        /// <summary>
        /// Membership Enum
        /// </summary>
        public enum UserProfileStatus
        {
            /// <summary>
            /// New Member
            /// </summary>
            NewMembership = 0,

            /// <summary>
            /// Membership details are saved
            /// </summary>
            SavedMembershipProfile,

            /// <summary>
            /// Membership Applied,Pending with Admin
            /// </summary>
            SubmittedMembershipProfile,

            /// <summary>
            /// Membership Applied,Pending with Admin
            /// </summary>
            ApprovedMembershipProfile,

            /// <summary>
            /// Membership Applied,Rejected by Admin
            /// </summary>
            RejectedMembershipProfile,

            /// <summary>
            /// Membership Active
            /// </summary>
            ActiveMembership,

            /// <summary>
            /// Membership Active
            /// </summary>
            ActiveGraceMembership,

            /// <summary>
            /// Membership Expired
            /// </summary>
            ExpiredMembership,
        }
    }
}
