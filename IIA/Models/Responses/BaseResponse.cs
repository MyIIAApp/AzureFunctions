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
        /// Initializes a new instance of the <see cref="BaseResponse"/> class.
        /// </summary>
        /// <param name="token">List of Tokens</param>
        /// <param name="unitName">List of Unit Names</param>
        /// <param name="message">Message</param>
        public BaseResponse(List<LoginMetadata> token, List<string> unitName, string message)
        {
            this.LoginMetadataList = token;
            this.Message = message;
            this.UnitName = unitName;
            this.LoginMetadata = token != null ? token.Count > 0 ? token[0] : null : null;
        }

        /// <summary>
        /// Gets or sets Token
        /// </summary>
        public LoginMetadata LoginMetadata { get; set; }

        /// <summary>
        /// Gets or sets Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets List of Tokens
        /// </summary>
        public List<LoginMetadata> LoginMetadataList { get; set; }

        /// <summary>
        /// Gets or sets List of UnitNames
        /// </summary>
        public List<string> UnitName { get; set; }
    }
}
