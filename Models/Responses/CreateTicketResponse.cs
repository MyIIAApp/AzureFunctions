using System.Collections.Generic;

namespace IIABackend
{
    /// <summary>
    /// Ticket Response Object
    /// </summary>
    public class CreateTicketResponse : BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTicketResponse"/> class.
        /// </summary>
        /// <param name="ticketNumber">Ticket number</param>
        /// <param name="token">token</param>
        /// <param name="message">message</param>
        public CreateTicketResponse(string ticketNumber, LoginMetadata token, string message)
            : base(token, message)
        {
            this.TicketNumber = ticketNumber;
        }

        /// <summary>
        /// Gets or sets TicketNumber.
        /// </summary>
        public string TicketNumber { get; set; }
    }
}
