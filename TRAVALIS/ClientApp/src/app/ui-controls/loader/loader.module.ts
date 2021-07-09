import { NgModule } from '@angular/core';
import { LoaderComponent } from './components/loader.component';
import { LoaderService } from './loader.service';

@NgModule({
    imports: [
        // MaterialFormsModule
    ],
    declarations: [
        LoaderComponent
    ],
    providers: [
        LoaderService
    ],
    exports: [
        LoaderComponent
    ]
})

export class LoaderModule { }
