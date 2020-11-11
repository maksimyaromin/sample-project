import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BASE_URL_TOKEN } from 'src/app/common/constants';
import { SearchCriteria } from 'src/app/common/models/search-criteria.model';
import { SearchOptions } from 'src/app/common/models/search-options.model';
import { Sale } from '../models/sale.model';

@Injectable({ providedIn: 'root' })
export class ReportService {
    constructor(
        private readonly http: HttpClient,
        @Inject(BASE_URL_TOKEN) private readonly baseUrl: string
    ) {}

    searchSales(searchCriteria: SearchCriteria): Observable<Sale[]> {
        return this.http.get<Sale[]>(this.baseUrl + 'api/Sales/search', {
            params: new HttpParams()
                .set('currentPage', searchCriteria.currentPage.toString())
                .set('pageSize', searchCriteria.pageSize.toString())
        });
    }

    getSearchOptions(): Observable<SearchOptions> {
        return this.http.get<SearchOptions>(this.baseUrl + 'api/Sales/search-options');
    }
}
