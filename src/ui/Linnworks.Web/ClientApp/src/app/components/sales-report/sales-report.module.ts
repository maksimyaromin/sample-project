import { NgModule } from '@angular/core';
import { SalesService } from 'src/app/services/sales.service';
import { SalesReportComponent } from './sales-report.component';

@NgModule({
    declarations: [SalesReportComponent],
    exports: [SalesReportComponent],
    providers: [SalesService]
})
export class SalesReportModule {}
