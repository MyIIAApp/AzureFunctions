using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// ChaptersResponse Object
    /// </summary>
    public class ChaptersResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChaptersResponse"/> class.
        /// </summary>
        /// <param name="chapters">List of chapters</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public ChaptersResponse(List<Chapter> chapters, LoginMetadata token, string message)
            : base(token, message)
        {
            this.Chapters = chapters;
        }

        /// <summary>
        /// Gets or sets Chapters.
        /// </summary>
        public List<Chapter> Chapters { get; set; }
    }
}
