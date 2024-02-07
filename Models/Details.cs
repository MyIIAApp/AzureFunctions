using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Ofers Object
    /// </summary>
    public class Details
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Details"/> class.
        /// </summary>
        /// <param name="enquiryId">enquiry id</param>
        /// /// <param name="buyerId">buyerId</param>
        /// <param name="sNo">item id</param>
        /// /// <param name="firstname">firstname</param>
        /// /// <param name="lastname">lastname</param>
        /// /// <param name="message">message</param>
        /// <param name="description">description</param>
        /// <param name="photoPath">photoPath</param>
        /// <param name="category">category</param>
        /// <param name="subcategory">subcategory</param>
        /// <param name="price">price</param>
        /// <param name="phoneNumber">PhoneNumber</param>
        /// <param name="time">time</param>
        /// <param name="enquiryStatus">status</param>
        /// <param name="address">address</param>
        /// <param name="itemName">name of item</param>
        public Details(string enquiryId, string sNo, string buyerId, string firstname, string lastname, string message, string description, string photoPath, string category, string subcategory, string price, string phoneNumber, string time, string enquiryStatus, string address, string itemName)
        {
            this.EnquiryId = enquiryId;
            this.Id = sNo;
            this.BuyerId = buyerId;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Message = message;
            this.PhoneNumber = phoneNumber;
            this.Description = description;
            this.PhotoPath = photoPath;
            this.Category = category;
            this.SubCategory = subcategory;
            this.Price = price;
            this.Time = time;
            this.EnquiryStatus = enquiryStatus;
            this.Address = address;
            this.Name = itemName;
        }

        /// <summary>
        /// Gets or sets buyerId
        /// </summary>
        [JsonProperty(PropertyName = "EnquiryId")]
        public string EnquiryId { get; set; }

        /// <summary>
        /// Gets or sets buyerId
        /// </summary>
        [JsonProperty(PropertyName = "BuyerId")]
        public string BuyerId { get; set; }

        /// <summary>
        /// Gets or sets id of enquiry
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets firstname
        /// </summary>
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets firstname
        /// </summary>
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets firstname
        /// </summary>
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets photoPath
        /// </summary>
        [JsonProperty(PropertyName = "PhotoPath")]
        public string PhotoPath { get; set; }

        /// <summary>
        /// Gets or sets category
        /// </summary>
        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets subcategory
        /// </summary>
        [JsonProperty(PropertyName = "SubCategory")]
        public string SubCategory { get; set; }

        /// <summary>
        /// Gets or sets price
        /// </summary>
        [JsonProperty(PropertyName = "Price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber of the offers
        /// </summary>
        [JsonProperty(PropertyName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets time
        /// </summary>
        [JsonProperty(PropertyName = "Time")]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        [JsonProperty(PropertyName = "EnquiryStatus")]
        public string EnquiryStatus { get; set; }

        /// <summary>
        /// Gets or sets Address
        /// </summary>
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets item name
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
    }
}