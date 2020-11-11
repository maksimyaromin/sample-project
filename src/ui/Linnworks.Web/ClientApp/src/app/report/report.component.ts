import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'linnworks-report',
    templateUrl: './report.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReportComponent implements OnInit {

    constructor() { }

    ngOnInit(): void {
    }

}
