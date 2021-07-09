using AutoMapper;
using Domain.Core.Interfaces;
using Domain.Core.Specifications;
using Domain.Dto.TicketAgg;
using Domain.Entities.TicketAgg;
using Domain.Entities.TicketAgg.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAVALIS.Application.Interfaces;

namespace Application.Services
{
    public class SalesAppService : ISalesAppService, ITransientService
    {
        private readonly IMapper _mapper;

        private readonly IRepository<Ticket> _asyncTicketRepository;
        public SalesAppService(ITravalisDataContext travalisDataContext, IMapper mapper)
        {
            _asyncTicketRepository = travalisDataContext.GetRepositoryByEntity<Ticket>();
            _mapper = mapper;
        }
        public async Task<List<TicketDto>> GetConfirmedTickets()
        {
            var specs = new GetConfirmedTicketsSpecs();
            return await GetTicketsDto(specs);
        }

        public async Task<List<TicketDto>> GetExchangedTickets()
        {
            var specs = new GetExchangedTicketsSpecs();
            return await GetTicketsDto(specs);
        }

        public async Task<List<TicketDto>> GetRefundedTickets()
        {
            var specs = new GetRefundedTicketsSpecs();
            return await GetTicketsDto(specs);
        }

        public async Task<List<TicketDto>> GetVoidedTickets()
        {
            var specs = new GetVoidedTicketsSpecs();
            return await GetTicketsDto(specs);
        }

        private async Task<List<TicketDto>> GetTicketsDto(ISpecification<Ticket> specs)
        {
            var tickets = await _asyncTicketRepository.GetListAsync(specs);
            var ticketsDto = tickets.Select(t => _mapper.Map<Ticket, TicketDto>(t)).ToList();
            return ticketsDto;
        }
    }
}
