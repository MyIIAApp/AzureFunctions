using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Items Object
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="id">ItemId</param>
        /// <param name="name">Name</param>
        /// <param name="description">ItemDescription</param>
        /// <param name="photoPath">ItemPhotoPath</param>
        /// <param name="category">ItemCategory</param>
        /// <param name="subCategory">ItemSubCategory</param>
        /// <param name="price">ItemPrice</param>
        /// <param name="score">ItemScore</param>
        /// <param name="sellerId">ItemSellerId</param>
        /// <param name="sellerPhoneNumber">ItemSellerPhoneNumber</param>
        /// <param name="sellerName">ItemSellerName</param>
        /// <param name="sellerLocation">ItemSellerLocation</param>
        /// <param name="enquiryStatus">ItemEnquiryBySpecificBuyer</param>
        public Item(string id, string name, string description, string photoPath, string category, string subCategory, string price, string score, string sellerId, string sellerPhoneNumber, string sellerName, string sellerLocation, string enquiryStatus)
        {
            this.Id = id;
            this.Name = name;
            this.ItemDescription = description;
            this.PhotoPath = photoPath;
            this.Category = category;
            this.SubCategory = subCategory;
            this.Price = price;
            this.Score = score;
            this.SellerId = sellerId;
            this.SellerName = sellerName;
            this.SellerPhoneNumber = sellerPhoneNumber;
            this.SellerLocation = sellerLocation;
            this.EnquiryStatus = enquiryStatus;
        }

        /// <summary>
        /// Gets or sets id of the item
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets Name of the item
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ItemDescription of the item
        /// </summary>
        [JsonProperty(PropertyName = "ItemDescription")]
        public string ItemDescription { get; set; }

        /// <summary>
        /// Gets or sets PhotoPath of the item
        /// </summary>
        [JsonProperty(PropertyName = "PhotoPath")]
        public string PhotoPath { get; set; }

        /// <summary>
        /// Gets or sets Category of the item
        /// </summary>
        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets SubCategory of the item
        /// </summary>
        [JsonProperty(PropertyName = "SubCategory")]
        public string SubCategory { get; set; }

        /// <summary>
        /// Gets or sets Price of the item
        /// </summary>
        [JsonProperty(PropertyName = "Price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets Score of the item
        /// </summary>
        [JsonProperty(PropertyName = "Score")]
        public string Score { get; set; }

        /// <summary>
        /// Gets or sets SellerId of the seller of the item
        /// </summary>
        [JsonProperty(PropertyName = "SellerId")]
        public string SellerId { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber of the seller of the item
        /// </summary>
        [JsonProperty(PropertyName = "SellerPhoneNumber")]
        public string SellerPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Name of the Seller of the item
        /// /// </summary>
        [JsonProperty(PropertyName = "SellerName")]
        public string SellerName { get; set; }

        /// <summary>
        /// Gets or sets Location of the Seller of the item
        /// </summary>
        [JsonProperty(PropertyName = "SellerLocation")]
        public string SellerLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether status of the enquiry of the item by a particular buyer
        /// </summary>
        [JsonProperty(PropertyName = "EnquiryStatus")]
        public string EnquiryStatus { get; set; }
    }
}
