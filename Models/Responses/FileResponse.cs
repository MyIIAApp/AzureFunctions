using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// News Response Object
    /// </summary>
    public class FileResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileResponse"/> class.
        /// </summary>
        /// <param name="path">List of news</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public FileResponse(string path, LoginMetadata token, string message)
            : base(token, message)
        {
            this.Path = path;
        }

        /// <summary>
        /// Gets or sets News.
        /// </summary>
        public string Path { get; set; }
    }
}
