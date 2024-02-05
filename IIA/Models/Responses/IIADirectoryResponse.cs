using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// IIA Directory Response Object
    /// </summary>
    public class IIADirectoryResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IIADirectoryResponse"/> class.
        /// </summary>
        /// <param name="iIADirectory">List of details</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public IIADirectoryResponse(List<IIADirectory> iIADirectory)
        {
            this.IIADirectory = iIADirectory;
        }

        /// <summary>
        /// Gets or sets Members.
        /// </summary>
        public List<IIADirectory> IIADirectory { get; set; }
    }
}
