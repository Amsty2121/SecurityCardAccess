import { MatFormFieldModule } from '@angular/material/form-field';
import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Device } from 'app/devices/devices.component';
import { MatSelectModule } from '@angular/material/select';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DevicesService, SnackbarService } from 'app/common/services';
import { accessLevels } from 'app/common/models';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
  ],
  templateUrl: './edit-device.component.html',
  styleUrls: ['./edit-device.component.scss'],
})
export class EditDeviceDialogComponent {
  readonly accessLevels = accessLevels;

  accessLevel = new FormControl(
    this._data.accessLevel || null,
    Validators.required
  );

  constructor(
    private _devicesService: DevicesService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<EditDeviceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: Device
  ) {}

  edit() {
    this._devicesService
      .editAccessLevel(this._data.id, this.accessLevel.value)
      .subscribe({
        next: () => {
          this._dialogRef.close({
            ...this._data,
            accessLevel: this.accessLevel.value,
          });
          this._sbs.displayMessage('Device Access Level Updated');
        },
        error: () => {
          this._sbs.displayMessage(`Couldn't update Device Access Level`);
          this._dialogRef.close();
        },
      });
  }
}
