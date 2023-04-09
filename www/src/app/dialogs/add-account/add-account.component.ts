import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { UserRole, userRoles } from 'app/common/models';
import { AccountsService, SnackbarService } from 'app/common/services';
import { Account } from 'app/accounts/accounts.component';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

export interface AddAccount {
  department: string;
  username: string;
  password: string;
  passwordConfirmation: string;
  role: UserRole;
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
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.scss'],
})
export class AddAccountsDialogComponent {
  readonly userRoles = userRoles;

  accountControls = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.email]),
    department: new FormControl('', Validators.required),
    role: new FormControl<UserRole>(null, Validators.required),
    password: new FormControl('', Validators.required),
    passwordConfirmation: new FormControl('', Validators.required),
  });

  constructor(
    private _accountsService: AccountsService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<AddAccountsDialogComponent>
  ) {}

  add() {
    this.accountControls.markAllAsTouched();

    if (this.accountControls.invalid) return;

    this._accountsService
      .addAccount(this.accountControls.value as AddAccount)
      .subscribe({
        next: (addedAccount: Account) => {
          this._sbs.displayMessage('Account added successfully');
          this._dialogRef.close(addedAccount);
        },
        error: (err) => {
          this._sbs.displayMessage(err.error);
          this._dialogRef.close();
        },
      });
  }
}
