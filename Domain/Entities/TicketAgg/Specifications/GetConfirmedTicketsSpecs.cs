using Domain.Core.Specifications;
using Domain.Entities.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.TicketAgg.Specifications
{
    public class GetConfirmedTicketsSpecs : BaseSpecification<Ticket>
    {
        public GetConfirmedTicketsSpecs() 
            : base(x => x.TicketStatus == TicketStatus.Confirmed)
        {

        }
    }
}
