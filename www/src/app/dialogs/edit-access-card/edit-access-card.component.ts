import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { AccessCard } from 'app/access-cards/access-cards.component';
import { accessLevels } from 'app/common/models';
import { CardsService, SnackbarService } from 'app/common/services';

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
  templateUrl: './edit-access-card.component.html',
  styleUrls: ['./edit-access-card.component.scss'],
})
export class EditAccessCardComponent {
  readonly accessLevels = accessLevels;

  accessLevel = new FormControl(
    this.data.accessLevel || null,
    Validators.required
  );

  constructor(
    private _cardsService: CardsService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<EditAccessCardComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AccessCard
  ) {}

  edit() {
    this._cardsService
      .editAccessLevel(this.data.id, this.accessLevel.value)
      .subscribe({
        next: () => {
          this._dialogRef.close({
            ...this.data,
            accessLevel: this.accessLevel.value,
          });
          this._sbs.displayMessage('Access Level Updated');
        },
        error: () => {
          this._sbs.displayMessage(`Couldn't update Access Level`);
          this._dialogRef.close();
        },
      });
  }
}
