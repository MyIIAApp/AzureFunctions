using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Items Response Object
    /// </summary>
    public class ItemListingResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemListingResponse"/> class.
        /// </summary>
        /// <param name="itemListing">List of items</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public ItemListingResponse(List<dynamic> itemListing, LoginMetadata token, string message)
            : base(token, message)
        {
            this.ItemListing = itemListing;
        }

        /// <summary>
        /// Gets or sets items.
        /// </summary>
        public List<dynamic> ItemListing { get; set; }
    }
}
