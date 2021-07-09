import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TravalisContainerComponent } from '../../layout/container/travalis-container.component';
import { SalesContainerComponent } from './container/sales-container.component';


const loginRoutes: Routes = [
  {
    path: '',
    component: TravalisContainerComponent,
    pathMatch: 'prefix',
    children: [
      { path: '', component: SalesContainerComponent },
      { path: 'voided', component: SalesContainerComponent },
      { path: 'exchanged', component: SalesContainerComponent },
      { path: 'refunded', component: SalesContainerComponent },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(loginRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class SalesRoutingModule { }
