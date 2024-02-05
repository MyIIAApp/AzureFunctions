using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Chapters Object
    /// </summary>
    public class WaMemberDetailsModal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaMemberDetailsModal"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        /// <param name="number">PhoneNumber</param>
        public WaMemberDetailsModal(int id, string name, string number)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNumber = number;
        }

        /// <summary>
        /// Gets or sets id of the members
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets title of the news
        /// </summary>
        [JsonProperty(PropertyName = "number")]
        public string PhoneNumber { get; set; }
    }
}
