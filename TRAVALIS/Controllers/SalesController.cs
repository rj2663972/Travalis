using Domain.Dto.TicketAgg;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRAVALIS.Application.Interfaces;
using TRAVALIS.Controllers.Base;

namespace TRAVALIS.Controllers
{
    public class SalesController : ApiControllerBase
    {
        private readonly ISalesAppService salesAppService;
        public SalesController(ISalesAppService salesAppService)
        {
            this.salesAppService = salesAppService;
        }

        [HttpGet("confirmed")]
        public async Task<ActionResult<List<TicketDto>>> GetConfirmedTickets()
        {
            var tickets = await salesAppService.GetConfirmedTickets();
            return Ok(tickets);
        }

        [HttpGet("exchanged")]
        public async Task<ActionResult<List<TicketDto>>> GetExchangedTickets()
        {
            var tickets = await salesAppService.GetExchangedTickets();
            return Ok(tickets);
        }

        [HttpGet("voided")]
        public async Task<ActionResult<List<TicketDto>>> GetVoidedTickets()
        {
            var tickets = await salesAppService.GetVoidedTickets();
            return Ok(tickets);
        }

        [HttpGet("refunded")]
        public async Task<ActionResult<List<TicketDto>>> GetRefundedTickets()
        {
            var tickets = await salesAppService.GetRefundedTickets();
            return Ok(tickets);
        }


    }
}
