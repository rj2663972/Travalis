import { AuthGuardService } from './auth-guard.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        CommonModule
    ],
    providers: [AuthGuardService],
    declarations: []
})
export class AuthorizationServiceModule { }


