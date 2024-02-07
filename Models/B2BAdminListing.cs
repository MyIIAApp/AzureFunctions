using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// B2B Admin Listing
    /// </summary>
    public class B2BAdminListing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="B2BAdminListing"/> class.
        /// </summary>
        /// <param name="itemId">Id of the item</param>
        /// <param name="itemName">Name of the item</param>
        /// <param name="sellerName">Name of seller</param>
        /// <param name="itemPrice">Price of the item</param>
        /// <param name="photoPath">Photopath of the item</param>
        /// <param name="status">status of the item whether it is blocked or not</param>
        public B2BAdminListing(string itemId, string itemName, string sellerName, string itemPrice, string photoPath, string status)
        {
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.SellerName = sellerName;
            this.ItemPrice = itemPrice;
            this.PhotoPath = photoPath;
            this.Status = status;
        }

        /// <summary>
        /// Gets or sets Item id
        /// </summary>
        [JsonProperty(PropertyName = "ItemId")]
        public string ItemId { get; set; }

        /// <summary>
        /// Gets or sets Item name
        /// </summary>
        [JsonProperty(PropertyName = "ItemName")]
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets Seller name
        /// </summary>
        [JsonProperty(PropertyName = "SellerName")]
        public string SellerName { get; set; }

        /// <summary>
        /// Gets or sets Item price
        /// </summary>
        [JsonProperty(PropertyName = "ItemPrice")]
        public string ItemPrice { get; set; }

        /// <summary>
        /// Gets or sets Photo path
        /// </summary>
        [JsonProperty(PropertyName = "PhotoPath")]
        public string PhotoPath { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
    }
}