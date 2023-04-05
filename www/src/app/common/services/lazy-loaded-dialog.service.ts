import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import {
  MatDialog,
  MatDialogConfig,
  MatDialogRef,
} from '@angular/material/dialog';

@Injectable({
  providedIn: 'root',
})
export class LazyLoadedDialogService {
  private defaultConfig: MatDialogConfig = { autoFocus: false };

  constructor(private _dialog: MatDialog) {}

  async openDialog(
    importCb,
    config?: MatDialogConfig
  ): Promise<MatDialogRef<any>> {
    const chunk = await importCb;
    const dialogComponent = Object.values(chunk)[0] as ComponentType<unknown>;
    return this._dialog.open(dialogComponent, {
      ...this.defaultConfig,
      ...config,
    });
  }
}
