import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { SalesService } from 'src/app/services/sales.service';

@Component({
    selector: 'linnworks-sales-report',
    template: '',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SalesReportComponent implements OnInit {
    constructor(
        private readonly salesService: SalesService
    ) {}

    ngOnInit(): void {
        this.salesService.get()
            .subscribe(sales => {
                console.log(sales);
            });
    }
}
