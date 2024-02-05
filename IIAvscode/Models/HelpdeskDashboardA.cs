using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// AdminHelp
    /// </summary>
    public class HelpdeskDashboardA
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpdeskDashboardA"/> class.
        /// </summary>
        /// <param name="pendingOnIIAChapterLevel">Total number of pending on IIA at chapter level</param>
        /// <param name="pendingOnUsersChapterLevel">Total number of panding on member at chapter level</param>
        /// <param name="openFor15ChapterLevel">Total number of Open for less than 15days at chapter level</param>
        /// <param name="openFor30ChapterLevel">Total number of members panding between 15-30 days at chapter level</param>
        /// <param name="openFor30MoreChapterLevel">Total number of member pending for more than 30 days at chapter level</param>
        /// <param name="openTicketChapterLevel">Total Users with open ticket at chapter level</param>
        /// <param name="ticketclosedChapterlevel">Total users with closed tickets at chapter level</param>
        /// <param name="pendingOnIIACenterLevel">Total number of pending on IIA at center level</param>
        /// <param name="pendingOnUsersCenterLevel">Total User pending at center level</param>
        /// <param name="openFor15CenterLevel">Total number of open for less than 15 days at center level</param>
        /// <param name="openFor30CenterLevel">Total number of open betwenn 15 to 30 days at center level</param>
        /// <param name="openFor30MoreCenterLevel">Total number of open for more than 30 days</param>
        /// <param name="openTicketCenterLevel">Total User with open ticket at center level</param>
        /// <param name="ticketclosedCenterlevel">Total User with closed ticket at center level</param>
        public HelpdeskDashboardA(string pendingOnIIAChapterLevel, string pendingOnUsersChapterLevel, string openFor15ChapterLevel, string openFor30ChapterLevel, string openFor30MoreChapterLevel, string openTicketChapterLevel, string ticketclosedChapterlevel, string pendingOnIIACenterLevel, string pendingOnUsersCenterLevel, string openFor15CenterLevel, string openFor30CenterLevel, string openFor30MoreCenterLevel, string openTicketCenterLevel, string ticketclosedCenterlevel)
        {
            this.PendingOnIIAChapterLevel = pendingOnIIAChapterLevel;
            this.PendinOnUsersChapterLevel = pendingOnUsersChapterLevel;
            this.OpenFor15ChapterLevel = openFor15ChapterLevel;
            this.OpenFor30ChapterLevel = openFor30ChapterLevel;
            this.OpenFor30MoreChapterLevel = openFor30MoreChapterLevel;
            this.OpenTicketChapterLevel = openTicketChapterLevel;
            this.ClosedTicketChapterLevel = ticketclosedChapterlevel;
            this.PendingOnIIACenterLevel = pendingOnIIACenterLevel;
            this.PendingOnUsersCenterLevel = pendingOnUsersCenterLevel;
            this.OpenFor15CenterLevel = openFor15CenterLevel;
            this.OpenFor30CenterLevel = openFor30CenterLevel;
            this.OpenFor30MoreCenterLevel = openFor30MoreCenterLevel;
            this.OpenTicketCenterLevel = openTicketCenterLevel;
            this.ClosedTicketCenterLevel = ticketclosedCenterlevel;
        }

        /// <summary>
        /// Gets or sets Total number of pending on IIA at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "PendingOnIIAChapterLevel")]
        public string PendingOnIIAChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of panding on member at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "PendinOnUsersChapterLevel")]
        public string PendinOnUsersChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of Open for less than 15days at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "OpenFor15ChapterLevel")]
        public string OpenFor15ChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of members panding between 15-30 days at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "OpenFor30ChapterLevel")]
        public string OpenFor30ChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of member pending for more than 30 days at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "OpenFor30MoreChapterLevel")]
        public string OpenFor30MoreChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total Users with open ticket at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "OpenTicketChapterLevel")]
        public string OpenTicketChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total users with closed tickets at chapter level
        /// </summary>
        [JsonProperty(PropertyName = "ClosedTicketChapterLevel")]
        public string ClosedTicketChapterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of pending on IIA at center level
        /// </summary>
        [JsonProperty(PropertyName = "PendingOnIIACenterLevel")]
        public string PendingOnIIACenterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total User pending at center level
        /// </summary>
        [JsonProperty(PropertyName = "PendingOnUsersCenterLevel")]
        public string PendingOnUsersCenterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of open for less than 15 days at center level
        /// </summary>
        [JsonProperty(PropertyName = "OpenFor15CenterLevel")]
        public string OpenFor15CenterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of open betwenn 15 to 30 days at center level
        /// </summary>
        [JsonProperty(PropertyName = "OpenFor30CenterLevel")]
        public string OpenFor30CenterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total number of open for more than 30 days
        /// </summary>
        [JsonProperty(PropertyName = "OpenFor30MoreCenterLevel")]
        public string OpenFor30MoreCenterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total User with open ticket at center level
        /// </summary>
        [JsonProperty(PropertyName = "OpenTicketCenterLevel")]
        public string OpenTicketCenterLevel { get; set; }

        /// <summary>
        /// Gets or sets Total User with closed ticket at center level
        /// </summary>
        [JsonProperty(PropertyName = "ClosedTicketCenterLevel")]
        public string ClosedTicketCenterLevel { get; set; }
    }
}