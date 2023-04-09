import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { Component } from '@angular/core';
import {
  FormGroup,
  ReactiveFormsModule,
  FormControl,
  Validators,
} from '@angular/forms';
import { AccessLevel, accessLevels } from 'app/common/models';
import { CommonModule } from '@angular/common';
import { DevicesService, SnackbarService } from 'app/common/services';
import { Device } from 'app/devices/devices.component';

export interface AddDevice {
  description: string;
  accessLevel: AccessLevel;
}

@Component({
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatButtonModule,
  ],
  templateUrl: './add-device.component.html',
  styleUrls: ['./add-device.component.scss'],
})
export class AddDeviceDialogComponent {
  readonly accessLevels = accessLevels;

  deviceControls = new FormGroup({
    description: new FormControl('', Validators.required),
    accessLevel: new FormControl<AccessLevel>(null, Validators.required),
  });

  constructor(
    private _dialogRef: MatDialogRef<AddDeviceDialogComponent>,
    private _sbs: SnackbarService,
    private _devicesService: DevicesService
  ) {}

  add() {
    this.deviceControls.markAllAsTouched();

    if (this.deviceControls.invalid) return;

    this._devicesService
      .addDevice(this.deviceControls.value as AddDevice)
      .subscribe({
        next: (addedDevice: Device) => {
          this._sbs.displayMessage('Device added successfully');
          this._dialogRef.close(addedDevice);
        },
        error: () => {
          this._sbs.displayMessage(`Couldn't add device`);
          this._dialogRef.close();
        },
      });
  }
}
