import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { SessionsService, SnackbarService } from 'app/common/services';
import { Session } from 'app/sessions/sessions.component';

@Component({
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './delete-session.component.html',
  styleUrls: ['./delete-session.component.scss'],
})
export class DeleteSessionComponent {
  constructor(
    private _sessionsService: SessionsService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<DeleteSessionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Session
  ) {}

  deleteSession() {
    this._sessionsService.deleteSession(this.data.id).subscribe({
      next: () => {
        this._dialogRef.close(this.data.id);
        this._sbs.displayMessage('Session closed successfully');
      },
      error: () => {
        this._dialogRef.close(false);
        this._sbs.displayMessage(`Couldn't close session`);
      },
    });
  }
}
