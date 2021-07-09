using AutoMapper;
using Domain.Entities.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.TicketAgg.Mapping
{
    public class TicketDtoMapping : Profile
    {
        public TicketDtoMapping()
        {
            CreateMap<TicketDto, Ticket>();
            CreateMap<Ticket, TicketDto>();
        }
    }
}
