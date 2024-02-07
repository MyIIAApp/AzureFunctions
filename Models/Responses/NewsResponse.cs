using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// News Response Object
    /// </summary>
    public class NewsResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsResponse"/> class.
        /// </summary>
        /// <param name="news">List of news</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public NewsResponse(List<News> news, LoginMetadata token, string message)
            : base(token, message)
        {
            this.News = news;
        }

        /// <summary>
        /// Gets or sets News.
        /// </summary>
        public List<News> News { get; set; }
    }
}
