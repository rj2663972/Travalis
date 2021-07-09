using Domain.Core.Specifications;
using Domain.Entities.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.TicketAgg.Specifications
{
    public class GetExchangedTicketsSpecs : BaseSpecification<Ticket>
    {
        public GetExchangedTicketsSpecs() 
            : base(x => x.TicketStatus == TicketStatus.Exchanged)
        {

        }
    }
}
