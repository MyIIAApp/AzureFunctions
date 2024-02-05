using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Offer Response Object
    /// </summary>
    public class DetailResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailResponse"/> class.
        /// </summary>
        /// <param name="details">List of details</param>
        public DetailResponse(List<dynamic> details)
        {
            this.Detail = details;
        }

        /// <summary>
        /// Gets or sets Offers.
        /// </summary>
        [JsonProperty(PropertyName = "detail")]
        public List<dynamic> Detail { get; set; }
    }
}
