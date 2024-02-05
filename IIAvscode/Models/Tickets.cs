using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Tickets Object
    /// </summary>
    public class Tickets
    {
        /// <summary>
        /// Gets categories of the tickets
        /// </summary>
        [JsonIgnore]
        public static readonly List<string> Categories = new List<string>() { "UP Govt.", "Centre Govt.", "UKD Govt.", "IIA Related Issues/Queries" };

        /// <summary>
        /// Initializes a new instance of the <see cref="Tickets"/> class.
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <param name="title">Title</param>
        /// <param name="description">Description</param>
        /// <param name="category">Category</param>
        /// <param name="chapterId">ChapterId</param>
        /// <param name="status">Status</param>
        /// <param name="ticketCreationTime">TicketCreationTime</param>
        /// <param name="userId">UserId</param>
        /// <param name="comment">Comment</param>
        /// <param name="attachment">Attachment</param>
        /// <param name="closedTicketTime">ClosedTicketTime</param>
        public Tickets(string ticketNumber, string title, string description, string category, string chapterId, string status, DateTime ticketCreationTime, string userId, List<Comment> comment, List<Attachment> attachment, DateTime closedTicketTime)
        {
            this.TicketNumber = ticketNumber;
            this.Title = title;
            this.Description = description;
            this.Category = category;
            this.ChapterId = chapterId;
            this.Status = status;
            this.TicketCreationTime = ticketCreationTime;
            this.UserId = userId;
            this.Comment = comment;
            this.Attachment = attachment;
            this.ClosedTicketTime = closedTicketTime;
        }

        /// <summary>
        /// Gets or sets TicketNumber of the Tickets object
        /// </summary>
        [JsonProperty(PropertyName = "TicketNumber")]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets Title of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets Description of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets Category of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets ChapterId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "ChapterId")]
        public string ChapterId { get; set; }

        /// <summary>
        /// Gets or sets Status of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets CreationTime of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "TicketCreationTime")]
        public DateTime TicketCreationTime { get; set; }

        /// <summary>
        /// Gets or sets UserId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets Comment of the Ticket
        /// </summary>
        [JsonProperty(PropertyName = "Comment")]
        public List<Comment> Comment { get; set; }

        /// <summary>
        /// Gets or sets Attachment of the Ticket
        /// </summary>
        [JsonProperty(PropertyName = "Attachment")]
        public List<Attachment> Attachment { get; set; }

        /// <summary>
        /// Gets or sets Closed Ticket Time of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "ClosedTicketTime")]
        public DateTime ClosedTicketTime { get; set; }
    }
}
