using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Chapters Object
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chapter"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        public Chapter(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets id of the chapters object
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title of the news
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
