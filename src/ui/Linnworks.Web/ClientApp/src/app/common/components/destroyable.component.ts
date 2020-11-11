import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({ template: '' })
export abstract class DestroyableComponent implements OnDestroy {
    protected readonly onDestroy: Subject<void> = new Subject<void>();

    ngOnDestroy(): void {
        this.onDestroy.next();
        this.onDestroy.complete();
    }
}
