using Domain.Dto.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TRAVALIS.Application.Interfaces
{
    public interface ISalesAppService
    {
        Task<List<TicketDto>> GetConfirmedTickets();
        Task<List<TicketDto>> GetExchangedTickets();
        Task<List<TicketDto>> GetRefundedTickets();
        Task<List<TicketDto>> GetVoidedTickets();
    }
}
