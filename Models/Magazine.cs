using System;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Magzine Object
    /// </summary>
    public class Magazine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Magazine"/> class.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="title">Title</param>
        /// <param name="magazinePath">MagzinePath</param>
        /// <param name="creatorAdminId">CreatorAdminName</param>
        /// <param name="creationTime">CreationTime</param>
        /// <param name="chapterId">ChapterId</param>
        /// <param name="magazineMonth">MagzineMonth</param>
        /// <param name="magazineYear">MagzineYear</param>
        /// <param name="coverPhotoPath">CoverPhotoPath</param>
        public Magazine(string id, string title, string magazinePath, int creatorAdminId, DateTime creationTime, int chapterId, string magazineMonth, string magazineYear, string coverPhotoPath)
        {
            this.Id = id;
            this.Title = title;
            this.CreatorAdminId = creatorAdminId;
            this.MagazinePath = magazinePath;
            this.CreatorAdminId = creatorAdminId;
            this.CreationTime = creationTime;
            this.ChapterId = chapterId;
            this.MagazineMonth = magazineMonth;
            this.MagazineYear = magazineYear;
            this.CoverPhotoPath = coverPhotoPath;
        }

        /// <summary>
        /// Gets or sets id of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets title of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets creationTime of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "creationTime")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets magzinePath of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "magazinePath")]
        public string MagazinePath { get; set; }

        /// <summary>
        /// Gets or sets CreatorAdminId of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "creatorAdminId")]
        public int CreatorAdminId { get; set; }

        /// <summary>
        /// Gets or sets chapterId of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "chapterId")]
        public int ChapterId { get; set; }

        /// <summary>
        /// Gets or sets magzineMonth of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "magazineMonth")]
        public string MagazineMonth { get; set; }

        /// <summary>
        /// Gets or sets magzineYear of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "magazineYear")]
        public string MagazineYear { get; set; }

        /// <summary>
        /// Gets or sets cover photo path of the magzine
        /// </summary>
        [JsonProperty(PropertyName = "coverPhotoPath")]
        public string CoverPhotoPath { get; set; }
    }
}
