using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        public User(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
