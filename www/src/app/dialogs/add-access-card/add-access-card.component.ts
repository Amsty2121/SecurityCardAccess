import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { AccessCard } from 'app/access-cards/access-cards.component';
import { Account } from 'app/accounts/accounts.component';
import { AccessLevel, accessLevels } from 'app/common/models';
import { CardsService, SnackbarService } from 'app/common/services';

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
  templateUrl: './add-access-card.component.html',
  styleUrls: ['./add-access-card.component.scss'],
})
export class AddAccessCardComponent {
  readonly accessLevels = accessLevels;

  cardsControls = new FormGroup({
    userId: new FormControl('', Validators.required),
    accessLevel: new FormControl<AccessLevel>(null, Validators.required),
    passCode: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required),
  });

  constructor(
    private _dialogRef: MatDialogRef<AddAccessCardComponent>,
    private _sbs: SnackbarService,
    private _cardsService: CardsService,
    @Inject(MAT_DIALOG_DATA) protected _users: Account[]
  ) {}

  add() {
    this.cardsControls.markAllAsTouched();

    if (this.cardsControls.invalid) return;

    this._cardsService
      .addAccessCard(this.cardsControls.value as AccessCard)
      .subscribe({
        next: (addedCard: AccessCard) => {
          this._sbs.displayMessage('Access Card added successfully');
          this._dialogRef.close(addedCard);
        },
        error: (err) => {
          this._sbs.displayMessage(err.error);
          this._dialogRef.close();
        },
      });
  }
}
