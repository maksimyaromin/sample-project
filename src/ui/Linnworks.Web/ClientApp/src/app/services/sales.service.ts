import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiRequestService } from '../common/services/api-request.service';

@Injectable()
export class SalesService extends ApiRequestService {
    serviceRoute = '/sales';

    constructor(@Inject(HttpClient) private readonly httpClient: HttpClient) {
        super();
    }

    get(): Observable<any> {
        return this.httpClient.post<any>(this.route, {
            currentPage: 1,
            pageSize: 100
        }, this.httpOptions);
    }
}
