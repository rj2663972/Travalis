import { Component, Input, OnDestroy, OnInit} from '@angular/core';
import { Subscription} from 'rxjs';
import { LoaderService } from '../loader.service';
import { LoaderState } from '../models/loader';

@Component({
    selector: 'travalis-loader',
    template: `<div [hidden]="!show" class="loading">

                </div>`,
    styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements OnInit, OnDestroy {
    show = false;
    private subscription: Subscription;

    constructor(private _loaderService: LoaderService) {}

    ngOnInit() {
        this.subscription = this._loaderService.loaderState
            .subscribe((state: LoaderState) => {
                this.show = state.show;
            });
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
