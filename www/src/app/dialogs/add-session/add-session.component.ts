import { AccessCard } from 'app/access-cards/access-cards.component';
import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import {
  FormGroup,
  ReactiveFormsModule,
  FormControl,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { Device } from 'app/devices/devices.component';
import { SnackbarService, SessionsService } from 'app/common/services';
import { Session } from 'app/sessions/sessions.component';

export interface AddSession {
  accessCardId: string;
  deviceId?: string;
}

@Component({
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatButtonModule,
  ],
  templateUrl: './add-session.component.html',
  styleUrls: ['./add-session.component.scss'],
})
export class SessionComponent {
  sessionControl = new FormGroup({
    accessCardId: new FormControl('', Validators.required),
    deviceId: new FormControl(null),
  });

  constructor(
    private _sbs: SnackbarService,
    private _sessionsService: SessionsService,
    private _dialogRef: MatDialogRef<SessionComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { cards: AccessCard[]; devices: Device[] }
  ) {}

  add() {
    this.sessionControl.markAllAsTouched();

    if (this.sessionControl.invalid) return;

    this._sessionsService
      .addSession(
        this.sessionControl.value.deviceId
          ? (this.sessionControl.value as AddSession)
          : ({
              accessCardId: this.sessionControl.value.accessCardId,
            } as AddSession)
      )
      .subscribe({
        next: (addedDevice: Session) => {
          this._sbs.displayMessage('Session added successfully');
          this._dialogRef.close(addedDevice);
        },
        error: () => {
          this._sbs.displayMessage(`Couldn't add session`);
          this._dialogRef.close();
        },
      });
  }
}
