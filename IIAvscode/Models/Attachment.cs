using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// Attachment Object
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class.
        /// </summary>
        /// <param name="ticketNumber">TicketNumber</param>
        /// <param name="attachmenturl">AttachmentURL</param>
        /// <param name="username">UserName</param>
        /// <param name="adminname">AdminName</param>
        /// <param name="attachmentcreationtime">AttachmentCreationTime</param>
        public Attachment(string ticketNumber, string username, string adminname, string attachmenturl, DateTime attachmentcreationtime)
        {
            this.TicketNumber = ticketNumber;
            this.UserName = username;
            this.AdminName = adminname;
            this.AttachmentURL = attachmenturl;
            this.AttachmentCreationTime = attachmentcreationtime;
        }

        /// <summary>
        /// Gets or sets TicketNumber of the Tickets object
        /// </summary>
        [JsonProperty(PropertyName = "TicketNumber")]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets Title of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "AttachmentURL")]
        public string AttachmentURL { get; set; }

        /// <summary>
        /// Gets or sets UserId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets AdminId of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "AdminName")]
        public string AdminName { get; set; }

        /// <summary>
        /// Gets or sets Attachment CreationTime of the Tickets
        /// </summary>
        [JsonProperty(PropertyName = "AttachmentCreationTime")]
        public DateTime AttachmentCreationTime { get; set; }
    }
}
