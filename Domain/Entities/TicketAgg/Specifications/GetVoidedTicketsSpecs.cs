using Domain.Core.Specifications;
using Domain.Entities.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.TicketAgg.Specifications
{
    public class GetVoidedTicketsSpecs : BaseSpecification<Ticket>
    {
        public GetVoidedTicketsSpecs() 
            : base(x => x.TicketStatus == TicketStatus.Voided)
        {

        }
    }
}
