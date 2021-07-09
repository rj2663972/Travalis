import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TravalisContainerComponent } from './container/travalis-container.component';
import { TopMenuComponent } from './components/top-menu/top-menu.component';
import { LeftMenuComponent } from './components/left-menu/left-menu.component';
import { TravalisServiceModule } from 'src/app/services/travalis/travalis-service.module';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    TravalisServiceModule
  ],
  declarations: [
    TravalisContainerComponent,
    TopMenuComponent,
    LeftMenuComponent
  ],
  exports: [

  ]
})
export class TravalisLayoutModule { }
