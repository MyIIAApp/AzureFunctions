using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Ticket Response Object
    /// </summary>
    public class TicketResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketResponse"/> class.
        /// </summary>
        /// <param name="ticketiia">List of ticketiia details</param>
        /// <param name="ticketmember">List of ticketmember details</param>
        /// <param name="ticketclosed">List of ticketclosed details</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public TicketResponse(List<Tickets> ticketiia, List<Tickets> ticketmember, List<Tickets> ticketclosed, LoginMetadata token, string message)
            : base(token, message)
        {
            this.TicketIia = ticketiia;
            this.TicketMember = ticketmember;
            this.TicketClosed = ticketclosed;
        }

        /// <summary>
        /// Gets or sets Ticket.
        /// </summary>
        public List<Tickets> TicketIia { get; set; }

        /// <summary>
        /// Gets or sets Ticket.
        /// </summary>
        public List<Tickets> TicketMember { get; set; }

        /// <summary>
        /// Gets or sets Ticket.
        /// </summary>
        public List<Tickets> TicketClosed { get; set; }
    }
}
