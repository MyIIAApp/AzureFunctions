using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Chapters Object
    /// </summary>
    public class MembershipStatusEnum
    {
        /// <summary>
        /// Membership Enum
        /// </summary>
        public enum MembershipStatus
        {
            /// <summary>
            /// Active Memberbeship
            /// </summary>
            Active,

            /// <summary>
            /// ActiveGrace
            /// </summary>
            ActiveGrace,

            /// <summary>
            /// Expired Memberbeship
            /// </summary>
            Expired,
        }
    }
}
