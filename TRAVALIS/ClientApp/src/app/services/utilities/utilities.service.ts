import { Injectable } from '@angular/core';
import { CoreCacheService } from '../../core/cache/core-cache.service';
import { HttpClient } from '@angular/common/http';
// import { LoaderService } from '../../ui-controls/loader/loader.service';


@Injectable()

export class UtilitiesService {

    constructor(
        private _coreCacheService: CoreCacheService,
        private _httpClient: HttpClient,
        // private _loaderService: LoaderService
    ) {
    }

    public getTokenUveCommon(): string {
        const userLogonDto = this._coreCacheService.getByKey('UserLogonDto');
        if (userLogonDto) {
            return userLogonDto.uveCommonToken.authToken;
        } else {
            return null;
        }
    }

    public getTokenUvePm(): string {
        const userLogonDto = this._coreCacheService.getByKey('UserLogonDto');
        if (userLogonDto) {
            return userLogonDto.uvePmToken.authToken;
        } else {
            return null;
        }
    }

    public getUserLogonDto(): any {
        return this._coreCacheService.getByKey('UserLogonDto');
    }

    public logOut() {
        this._coreCacheService.removeByKey('UserLogonDto');
    }


    public getEnvironment(): string {
        const userLogonDto = this._coreCacheService.getByKey('UserLogonDto');
        if (userLogonDto && userLogonDto.environmentApp !== 'Production') {
            return userLogonDto.environmentApp;
        } else {
            return '';
        }
    }


    // ************************** START LOADER **********************************
    public activateLoader() {
        // this._loaderService.show();
    }

    public deactivateLoader() {
        // this._loaderService.hide();
    }
    // ************************** END LOADER **************************************
}
