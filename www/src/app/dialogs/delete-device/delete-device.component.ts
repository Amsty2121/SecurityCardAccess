import { Device } from 'app/devices/devices.component';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { DevicesService, SnackbarService } from 'app/common/services';

@Component({
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './delete-device.component.html',
  styleUrls: ['./delete-device.component.scss'],
})
export class DeleteDeviceDialogComponent {
  constructor(
    private _devicesService: DevicesService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<DeleteDeviceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Device
  ) {}

  deleteDevice() {
    this._devicesService.deleteDevice(this.data.id).subscribe({
      next: () => {
        this._dialogRef.close(true);
        this._sbs.displayMessage('Device deleted successfully');
      },
      error: () => {
        this._dialogRef.close(false);
        this._sbs.displayMessage(`Couldn't delete device`);
      },
    });
  }
}
