import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { Account } from 'app/accounts/accounts.component';
import { AccountsService, SnackbarService } from 'app/common/services';

@Component({
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './delete-account.component.html',
  styleUrls: ['./delete-account.component.scss'],
})
export class DeleteAccountDialogComponent {
  constructor(
    private _accountsService: AccountsService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<DeleteAccountDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Account
  ) {}

  deleteAccount() {
    this._accountsService.deleteAccount(this.data.id).subscribe({
      next: () => {
        this._dialogRef.close(true);
        this._sbs.displayMessage('Account deleted successfully');
      },
      error: () => {
        this._dialogRef.close(false);
        this._sbs.displayMessage(`Couldn't delete account`);
      },
    });
  }
}
