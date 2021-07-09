import { NgModule } from '@angular/core';
import { TravalisLayoutModule } from '../../layout/travalis-layout.module';
import { SalesContainerComponent } from './container/sales-container.component';
import { SalesRoutingModule } from './sales-routing.module';



@NgModule({
  imports: [
    // Routing
    SalesRoutingModule,
    // ContainerModule
    TravalisLayoutModule
  ],
  declarations: [
    SalesContainerComponent
  ],

})
export class SalesModule { }
