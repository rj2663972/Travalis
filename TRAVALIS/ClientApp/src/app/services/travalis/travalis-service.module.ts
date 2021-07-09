import { SalesClient } from './travalis.services';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Alphabetical order please! ;-)


@NgModule({
  imports: [
    CommonModule
  ],
  /**Remember to declare or services (controllers) the generate name is {controller}Client */
  providers: [
    SalesClient
  ],
  declarations: []
})
export class TravalisServiceModule { }
