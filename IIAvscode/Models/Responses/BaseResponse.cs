using System;
using System.Collections.Generic;
using System.Text;

namespace IIABackend
{
    /// <summary>
    /// Base response class
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse"/> class.
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="message">Message</param>
        public BaseResponse(LoginMetadata token, string message)
        {
            this.LoginMetadata = token;
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets Token
        /// </summary>
        public LoginMetadata LoginMetadata { get; set; }

        /// <summary>
        /// Gets or sets Message
        /// </summary>
        public string Message { get; set; }
    }
}
