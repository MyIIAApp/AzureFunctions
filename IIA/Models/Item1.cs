using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Items Object
    /// </summary>
    public class Item1
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item1"/> class.
        /// Seller items object
        /// </summary>
        /// <param name="id"> Id</param>
        /// <param name="name"> name</param>
        /// <param name="description"> description</param>
        /// <param name="photoPath">photpath</param>
        /// <param name="category">category</param>
        /// <param name="subCategory">subCategory</param>
        /// <param name="price">price</param>
        /// <param name="score">score</param>
        /// <param name="active">active of item</param>
        public Item1(string id, string name, string description, string photoPath, string category, string subCategory, string price, string score, string active)
        {
            this.Id = id;
            this.Name = name;
            this.ItemDescription = description;
            this.PhotoPath = photoPath;
            this.Category = category;
            this.SubCategory = subCategory;
            this.Price = price;
            this.Score = score;
            this.Activation = active;
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
        /// Gets or sets id of the item
        /// </summary>
        [JsonProperty(PropertyName = "Activation")]
        public string Activation { get; set; }
    }
}
