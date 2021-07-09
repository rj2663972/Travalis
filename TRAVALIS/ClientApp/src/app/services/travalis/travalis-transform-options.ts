import { HttpHeaders, HttpResponseBase } from '@angular/common/http';
import { Injector } from '@angular/core';
import { Observable } from 'rxjs';

export class TravalisTransformOptions {

    // _loadingService: LoadingService;
    // _webCacheService: WebCacheService;

    constructor(
        private _injector: Injector
    ) {
        // this._loadingService = this._injector.get(LoadingService);
        // this._webCacheService = this._injector.get(WebCacheService);
    }

    protected transformOptions(options: any): Promise<any> {
        // this._loadingService.showLoader();
        options.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=UTF-8',
            'Accept': 'application/json',
            // 'Authorization': `Bearer ${this._webCacheService.getToken()}`,
            'Cache-Control': 'no-cache',
            'Pragma': 'no-cache'
        });
        return Promise.resolve(options);
    }


    protected transformResult(url: string, response: HttpResponseBase, processor: (response: HttpResponseBase) => any): Observable<any> {
        // this._loadingService.hideLoader();
        if (response.status !== 200) {
            // todo alert with error
        }
        return processor(response);
    }

}


