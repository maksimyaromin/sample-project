import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject } from '@angular/core';

export abstract class ApiRequestService {
    abstract serviceRoute: string;

    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    };

    get route(): string {
        return environment.apiUrl + this.serviceRoute;
    }
}
