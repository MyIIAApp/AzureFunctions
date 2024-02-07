using System;
using System.Collections.Generic;
using System.Text;

namespace IIABackend
{
    /// <summary>
    /// OTP response class
    /// </summary>
    public class OtpResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OtpResponse"/> class.
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        public OtpResponse(string token, string phoneNumber)
        {
            this.Token = token;
            this.PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Gets or sets Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
