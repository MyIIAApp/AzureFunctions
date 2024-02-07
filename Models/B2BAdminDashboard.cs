using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IIABackend
{
    /// <summary>
    /// B2B Admin Dashboard
    /// </summary>
    public class B2BAdminDashboard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="B2BAdminDashboard"/> class.
        /// </summary>
        /// <param name="totalSellers">total number of sellers</param>
        /// <param name="totalListing">total number of listing</param>
        /// <param name="activeListing">total Number of active Listing</param>
        /// <param name="inactiveListing">total number of inactive Listing</param>
        /// <param name="totalEnquiries">total number of enquiries</param>
        /// <param name="resolvedEnquiries">total number of resolved enquiries</param>
        /// <param name="pendingEnquiries">total number of pending enquiries</param>
        public B2BAdminDashboard(string totalSellers, string totalListing, string activeListing, string inactiveListing, string totalEnquiries, string resolvedEnquiries, string pendingEnquiries)
        {
            this.TotalSellers = totalSellers;
            this.TotalListing = totalListing;
            this.ActiveListing = activeListing;
            this.InactiveListing = inactiveListing;
            this.TotalEnquiries = totalEnquiries;
            this.ResolvedEnquiries = resolvedEnquiries;
            this.PendingEnquiries = pendingEnquiries;
        }

        /// <summary>
        /// Gets or sets Total number of sellers
        /// </summary>
        [JsonProperty(PropertyName = "TotalSellers")]
        public string TotalSellers { get; set; }

        /// <summary>
        /// Gets or sets Total number of Listing
        /// </summary>
        [JsonProperty(PropertyName = "TotalListing")]
        public string TotalListing { get; set; }

        /// <summary>
        /// Gets or sets Total number of active listing
        /// </summary>
        [JsonProperty(PropertyName = "ActiveListing")]
        public string ActiveListing { get; set; }

        /// <summary>
        /// Gets or sets Total number of inactive listing
        /// </summary>
        [JsonProperty(PropertyName = "InactiveListing")]
        public string InactiveListing { get; set; }

        /// <summary>
        /// Gets or sets Total number of enquiries
        /// </summary>
        [JsonProperty(PropertyName = "TotalEnquiries")]
        public string TotalEnquiries { get; set; }

        /// <summary>
        /// Gets or sets Total number of resolved enquiries
        /// </summary>
        [JsonProperty(PropertyName = "ResolvedEnquiries")]
        public string ResolvedEnquiries { get; set; }

        /// <summary>
        /// Gets or sets Total number of pending enquiries
        /// </summary>
        [JsonProperty(PropertyName = "PendingEnquiries")]
        public string PendingEnquiries { get; set; }
    }
}