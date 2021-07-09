using Domain.Core.Entities;
using Domain.Entities.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.TicketAgg
{
    public enum TicketStatus
    {
        Confirmed = 1,
        Exchanged = 2,
        Voided = 3,
        Refunded = 4
    }
    public class Ticket : BaseEntity
    {
        public string PNR { get; private set; }
        public string TicketNumber { get; private set; }
        public string Airline { get; private set; }
        public string Description { get; private set; }
        public double BasicFare { get; private set; }
        public double Taxes { get; private set; }
        public double TotalAmout { get; protected set; }
        public double Commission { get; private set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime? TransactionDate { get; private set; }
        public User User { get; private set; }
    }
}
