using Domain.Core.Specifications;
using Domain.Entities.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.TicketAgg.Specifications
{
    public class GetRefundedTicketsSpecs : BaseSpecification<Ticket>
    {
        public GetRefundedTicketsSpecs() 
            : base(x => x.TicketStatus == TicketStatus.Refunded)
        {

        }
    }
}
