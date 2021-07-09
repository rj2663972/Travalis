import { SalesClient } from './../../../../../services/travalis/travalis.services';
import { Component, OnInit } from "@angular/core";
import { TicketDto } from "src/app/services/travalis/travalis.services";

@Component({
  selector: 'sales-container',
  template: `<h1>sales-container</h1>`
})
export class SalesContainerComponent implements OnInit{

  tickets: Array<TicketDto> = new Array<TicketDto>();

  constructor(private salesClient: SalesClient)
  {

  }

  ngOnInit(){
    this.salesClient.getConfirmedTickets().subscribe(data =>{
      this.tickets = data;
    })
  }


}
