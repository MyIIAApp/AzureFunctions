using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Offer Response Object
    /// </summary>
    public class ItemListResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemListResponse"/> class.
        /// </summary>
        /// <param name="items">List of items</param>
        public ItemListResponse(List<Item1> items)
        {
            this.ItemList = items;
        }

        /// <summary>
        /// Gets or sets Offers.
        /// </summary>
        [JsonProperty(PropertyName = "itemList")]
        public List<Item1> ItemList { get; set; }
    }
}
