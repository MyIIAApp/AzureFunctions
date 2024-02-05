using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Comment Object
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <param name="comments">Comment</param>
        /// <param name="username">UserName</param>
        /// <param name="commentcreationtime">CommentCreationTime</param>
        /// <param name="adminname">AdminName</param>
        public Comment(string ticketNumber, string username, string comments, DateTime commentcreationtime, string adminname)
        {
            this.TicketNumber = ticketNumber;
            this.Comments = comments;
            this.UserName = username;
            this.CommentCreationTime = commentcreationtime;
            this.AdminName = adminname;
        }

        /// <summary>
        /// Gets or sets TicketNumber of the Tickets object
        /// </summary>
        [JsonProperty(PropertyName = "TicketNumber")]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets Title of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "Comments")]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets UserId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets CommentCreationTime of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "CommentCreationTime")]
        public DateTime CommentCreationTime { get; set; }

        /// <summary>
        /// Gets or sets CreationTime of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "AdminName")]
        public string AdminName { get; set; }
    }
}
