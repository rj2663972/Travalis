/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.11.3.0 (NJsonSchema v10.4.4.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { TravalisTransformOptions } from './travalis-transform-options';
import { Injector } from '@angular/core';
import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, from as _observableFrom, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class SalesClient extends TravalisTransformOptions {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(Injector) configuration: Injector, @Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        super(configuration);
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getConfirmedTickets(): Observable<TicketDto[] | null> {
        let url_ = this.baseUrl + "/api/Sales/confirmed";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
            return this.http.request("get", url_, transformedOptions_);
        })).pipe(_observableMergeMap((response_: any) => {
            return this.transformResult(url_, response_, (r) => this.processGetConfirmedTickets(<any>r));
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processGetConfirmedTickets(<any>r));
                } catch (e) {
                    return <Observable<TicketDto[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<TicketDto[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetConfirmedTickets(response: HttpResponseBase): Observable<TicketDto[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(TicketDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TicketDto[] | null>(<any>null);
    }

    getExchangedTickets(): Observable<TicketDto[] | null> {
        let url_ = this.baseUrl + "/api/Sales/exchanged";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
            return this.http.request("get", url_, transformedOptions_);
        })).pipe(_observableMergeMap((response_: any) => {
            return this.transformResult(url_, response_, (r) => this.processGetExchangedTickets(<any>r));
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processGetExchangedTickets(<any>r));
                } catch (e) {
                    return <Observable<TicketDto[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<TicketDto[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetExchangedTickets(response: HttpResponseBase): Observable<TicketDto[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(TicketDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TicketDto[] | null>(<any>null);
    }

    getVoidedTickets(): Observable<TicketDto[] | null> {
        let url_ = this.baseUrl + "/api/Sales/voided";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
            return this.http.request("get", url_, transformedOptions_);
        })).pipe(_observableMergeMap((response_: any) => {
            return this.transformResult(url_, response_, (r) => this.processGetVoidedTickets(<any>r));
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processGetVoidedTickets(<any>r));
                } catch (e) {
                    return <Observable<TicketDto[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<TicketDto[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetVoidedTickets(response: HttpResponseBase): Observable<TicketDto[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(TicketDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TicketDto[] | null>(<any>null);
    }

    getRefundedTickets(): Observable<TicketDto[] | null> {
        let url_ = this.baseUrl + "/api/Sales/refunded";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return _observableFrom(this.transformOptions(options_)).pipe(_observableMergeMap(transformedOptions_ => {
            return this.http.request("get", url_, transformedOptions_);
        })).pipe(_observableMergeMap((response_: any) => {
            return this.transformResult(url_, response_, (r) => this.processGetRefundedTickets(<any>r));
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.transformResult(url_, response_, (r) => this.processGetRefundedTickets(<any>r));
                } catch (e) {
                    return <Observable<TicketDto[] | null>><any>_observableThrow(e);
                }
            } else
                return <Observable<TicketDto[] | null>><any>_observableThrow(response_);
        }));
    }

    protected processGetRefundedTickets(response: HttpResponseBase): Observable<TicketDto[] | null> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(TicketDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TicketDto[] | null>(<any>null);
    }
}

export abstract class BaseDto {
    id!: number;

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
        }
    }

    static fromJS(data: any): BaseDto {
        data = typeof data === 'object' ? data : {};
        throw new Error("The abstract class 'BaseDto' cannot be instantiated.");
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        return data; 
    }
}

export class TicketDto extends BaseDto {
    pnr?: string | undefined;
    ticketNumber?: string | undefined;
    airline?: string | undefined;
    description?: string | undefined;
    basicFare!: number;
    taxes!: number;
    totalAmout!: number;
    commission!: number;
    ticketStatus!: TicketStatus;
    transactionDate?: Date | undefined;
    user?: UserDto | undefined;

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.pnr = _data["pnr"];
            this.ticketNumber = _data["ticketNumber"];
            this.airline = _data["airline"];
            this.description = _data["description"];
            this.basicFare = _data["basicFare"];
            this.taxes = _data["taxes"];
            this.totalAmout = _data["totalAmout"];
            this.commission = _data["commission"];
            this.ticketStatus = _data["ticketStatus"];
            this.transactionDate = _data["transactionDate"] ? new Date(_data["transactionDate"].toString()) : <any>undefined;
            this.user = _data["user"] ? UserDto.fromJS(_data["user"]) : <any>undefined;
        }
    }

    static fromJS(data: any): TicketDto {
        data = typeof data === 'object' ? data : {};
        let result = new TicketDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["pnr"] = this.pnr;
        data["ticketNumber"] = this.ticketNumber;
        data["airline"] = this.airline;
        data["description"] = this.description;
        data["basicFare"] = this.basicFare;
        data["taxes"] = this.taxes;
        data["totalAmout"] = this.totalAmout;
        data["commission"] = this.commission;
        data["ticketStatus"] = this.ticketStatus;
        data["transactionDate"] = this.transactionDate ? this.transactionDate.toISOString() : <any>undefined;
        data["user"] = this.user ? this.user.toJSON() : <any>undefined;
        super.toJSON(data);
        return data; 
    }
}

export enum TicketStatus {
    Confirmed = 1,
    Exchanged = 2,
    Voided = 3,
    Refunded = 4,
}

export class UserDto extends BaseDto {
    isDeleted!: boolean;
    name?: string | undefined;
    surname?: string | undefined;
    email?: string | undefined;
    userName?: string | undefined;
    password?: string | undefined;

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.isDeleted = _data["isDeleted"];
            this.name = _data["name"];
            this.surname = _data["surname"];
            this.email = _data["email"];
            this.userName = _data["userName"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): UserDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["isDeleted"] = this.isDeleted;
        data["name"] = this.name;
        data["surname"] = this.surname;
        data["email"] = this.email;
        data["userName"] = this.userName;
        data["password"] = this.password;
        super.toJSON(data);
        return data; 
    }
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}