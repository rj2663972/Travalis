import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreCacheService } from '../../core/cache/core-cache.service';
import { UtilitiesService } from 'src/app/services/utilities/utilities.service';
// import { LoaderModule } from '../../ui-controls/loader/loader.module';
// import { SnackBarsModule } from '../../ui-controls/snackbars/snackbars.module';

@NgModule({
  imports: [
    CommonModule,
    // LoaderModule,
    // SnackBarsModule
  ],
  providers: [CoreCacheService, UtilitiesService],
  declarations: []
})
export class UtilitiesServiceModule { }


