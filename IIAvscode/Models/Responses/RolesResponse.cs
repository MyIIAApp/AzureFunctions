using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Roles Response Object
    /// </summary>
    public class RolesResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RolesResponse"/> class.
        /// </summary>
        /// <param name="role">List of Roles</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public RolesResponse(Roles role, LoginMetadata token, string message)
            : base(token, message)
        {
            this.Role = role;
        }

        /// <summary>
        /// Gets or sets Ticket.
        /// </summary>
        public Roles Role { get; set; }
    }
}
