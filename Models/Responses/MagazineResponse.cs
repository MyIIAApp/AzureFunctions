using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Magazine Response Object
    /// </summary>
    public class MagazineResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagazineResponse"/> class.
        /// </summary>
        /// <param name="magazine">List of Magzine</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public MagazineResponse(List<Magazine> magazine, LoginMetadata token, string message)
            : base(token, message)
        {
            this.Magazine = magazine;
        }

        /// <summary>
        /// Gets or sets Magazine.
        /// </summary>
        public List<Magazine> Magazine { get; set; }
    }
}
