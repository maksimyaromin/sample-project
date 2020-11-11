import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { SalesReportModule } from './components/sales-report/sales-report.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        SalesReportModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
