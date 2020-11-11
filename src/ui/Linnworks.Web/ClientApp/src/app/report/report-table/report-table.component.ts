import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { take, takeUntil } from 'rxjs/operators';
import { DestroyableComponent } from 'src/app/common/components/destroyable.component';
import { SearchOptions } from 'src/app/common/models/search-options.model';
import { TableColumn, TableColumnFormat } from 'src/app/common/models/table-column.model';
import { Sale } from '../models/sale.model';
import { ReportService } from '../services/report.service';

@Component({
    selector: 'linnworks-report-table',
    templateUrl: './report-table.component.html',
    styleUrls: ['./report-table.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReportTableComponent extends DestroyableComponent implements OnInit {
    public searchOptions: SearchOptions = {
        total: 0
    };
    public isLoading = true;
    public items: Sale[] = [];
    public columns: TableColumn[];

    public TableColumnFormat = TableColumnFormat;

    constructor(
        private readonly reportService: ReportService,
        private readonly changeDetectorRef: ChangeDetectorRef
    ) {
        super();
    }

    ngOnInit(): void {
        this.reportService.getSearchOptions()
            .pipe(takeUntil(this.onDestroy))
            .subscribe(options => {
                this.searchOptions = options;
                this.changeDetectorRef.markForCheck();
            });

        this.columns = [
            {
                field: 'countryRegionName',
                displayName: 'Region',
                format: TableColumnFormat.String
            },
            {
                field: 'countryName',
                displayName: 'Country',
                format: TableColumnFormat.String
            },
            {
                field: 'itemName',
                displayName: 'Item Type',
                format: TableColumnFormat.String
            },
            {
                field: 'salesChannel',
                displayName: 'Sales Channel',
                format: TableColumnFormat.String
            },
            {
                field: 'orderPrioritySymbol',
                displayName: 'Order Priority',
                format: TableColumnFormat.String
            },
            {
                field: 'orderedAt',
                displayName: 'Order Date',
                format: TableColumnFormat.Date
            },
            {
                field: 'orderId',
                displayName: 'Order ID',
                format: TableColumnFormat.Number
            },
            {
                field: 'shippedAt',
                displayName: 'Ship Date',
                format: TableColumnFormat.Date
            },
            {
                field: 'unitsSold',
                displayName: 'Units Sold',
                format: TableColumnFormat.Currency
            },
            {
                field: 'unitPrice',
                displayName: 'Unit Price',
                format: TableColumnFormat.Currency
            },
            {
                field: 'unitCost',
                displayName: 'Unit Cost',
                format: TableColumnFormat.Currency
            },
            {
                field: 'totalRevenue',
                displayName: 'Total Revenue',
                format: TableColumnFormat.Currency
            },
            {
                field: 'totalCost',
                displayName: 'Total Cost',
                format: TableColumnFormat.Currency
            },
            {
                field: 'totalProfit',
                displayName: 'Total Profit',
                format: TableColumnFormat.Currency
            }
        ];
    }

    goToPage(event: LazyLoadEvent): void {
        this.isLoading = true;

        this.reportService.searchSales({
            currentPage: Math.ceil(event.first / event.rows) + 1,
            pageSize: event.rows
        })
            .pipe(take(1))
            .subscribe(items => {
                this.items = items;
                this.isLoading = false;
                this.changeDetectorRef.markForCheck();
            });
    }
}
