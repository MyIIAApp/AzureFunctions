using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// News Object
    /// </summary>
    public class News
    {
        /// <summary>
        /// Gets categories of the news
        /// </summary>
        [JsonIgnore]
        public static readonly List<string> Categories = new List<string>() { "Finance", "Trade", "IIA", "Covid19" };

        /// <summary>
        /// Initializes a new instance of the <see cref="News"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="link">Link</param>
        /// <param name="category">Category</param>
        /// <param name="imagePath">ImagePath</param>
        /// <param name="creationTime">CreationTime</param>
        /// <param name="chapterName">ChapterName</param>
        /// <param name="creatorAdminId">CreatorAdminName</param>
        public News(string id, string title, string description, string link, string imagePath,  string category, int creatorAdminId, DateTime creationTime, string chapterName)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Link = link;
            this.Category = category;
            this.ImagePath = imagePath;
            this.CreatorAdminId = creatorAdminId;
            this.CreationTime = creationTime;
            this.ChapterName = chapterName;
        }

        /// <summary>
        /// Gets or sets id of the news
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets title of the news
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets description of the news
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets link of the news
        /// </summary>
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets category of the news
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets creationTime of the news
        /// </summary>
        [JsonProperty(PropertyName = "creationTime")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets imagePath of the news
        /// </summary>
        [JsonProperty(PropertyName = "imagePath")]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets CreatorAdminId of the news
        /// </summary>
        [JsonProperty(PropertyName = "creatorAdminId")]
        public int CreatorAdminId { get; set; }

        /// <summary>
        /// Gets or sets chapterName of the news
        /// </summary>
        [JsonProperty(PropertyName = "chapterName")]
        public string ChapterName { get; set; }
    }
}
