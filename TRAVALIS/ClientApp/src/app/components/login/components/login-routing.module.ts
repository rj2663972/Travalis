import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginContainerComponent } from '../layout/container/login-container.component';
import { LoginCardComponent } from './login-card/login-card.component';

const loginRoutes: Routes = [
    {
        path: '',
        component: LoginContainerComponent,
        pathMatch: 'prefix',
        children: [
          { path: 'login', component: LoginCardComponent }
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
export class LoginRoutingModule { }
