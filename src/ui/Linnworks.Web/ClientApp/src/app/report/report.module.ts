import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReportRoutingModule } from './report-routing.module';
import { ReportComponent } from './report.component';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ReportTableComponent } from './report-table/report-table.component';

@NgModule({
    declarations: [
        ReportComponent,
        ReportTableComponent
    ],
    imports: [
        CardModule,
        CommonModule,
        ReportRoutingModule,
        TableModule
    ]
})
export class ReportModule {}
