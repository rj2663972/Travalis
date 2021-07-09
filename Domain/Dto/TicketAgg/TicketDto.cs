using Domain.Core.Dto;
using Domain.Dto.UserDtoAgg;
using Domain.Entities.TicketAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.TicketAgg
{
    public class TicketDto : BaseDto
    {
        public string PNR { get; set; }
        public string TicketNumber { get; set; }
        public string Airline { get; set; }
        public string Description { get; set; }
        public double BasicFare { get; set; }
        public double Taxes { get; set; }
        public double TotalAmout { get; set; }
        public double Commission { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime? TransactionDate { get; set; }
        public UserDto User { get; set; }
    }
}
