import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { LoaderModule } from "src/app/ui-controls/loader/loader.module";
import { LoginContainerComponent } from "./container/login-container.component";


@NgModule({
  imports: [
    RouterModule,
    // Services
    // WebCacheServiceModule,
    // UtilitiesServiceModule,
    // Material and Forms Modules
    // MaterialFormsModule,
    // FormControlsModule,
    // Own modules
    LoaderModule
  ],
  declarations: [
    LoginContainerComponent
  ],
  exports: [
    // Services
    // WebCacheServiceModule,
    // UveCommonServiceModule,
    // UvePmServiceModule,
    // UtilitiesServiceModule,
    // Material and Forms Modules
    // MaterialFormsModule,
    // FormControlsModule,
    // Own modules
    LoaderModule,
    // Layout Components
    LoginContainerComponent
  ]

})
export class LoginLayoutModule { }
