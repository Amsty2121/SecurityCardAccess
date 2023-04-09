import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({ standalone: true, template: '' })
export class DestroyableComponent implements OnDestroy {
  readonly destroy$ = new Subject<void>();

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
