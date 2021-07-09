import { NgModule } from '@angular/core';
import { LoginRoutingModule  } from './login-routing.module';
import { LoginLayoutModule } from 'src/app/components/login/layout/login-layout.module';

import { LoginCardComponent } from './login-card/login-card.component';


@NgModule({
  imports: [
    // Routing
    LoginRoutingModule,
    // ContainerModule
    LoginLayoutModule
  ],
  declarations: [
    LoginCardComponent
  ],

})
export class LoginModule { }
