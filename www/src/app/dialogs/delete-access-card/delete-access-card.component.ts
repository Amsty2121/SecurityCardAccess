import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { AccessCard } from 'app/access-cards/access-cards.component';
import { CardsService, SnackbarService } from 'app/common/services';

@Component({
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './delete-access-card.component.html',
  styleUrls: ['./delete-access-card.component.scss'],
})
export class DeleteAccessCardDialogComponent {
  constructor(
    private _cardsService: CardsService,
    private _sbs: SnackbarService,
    private _dialogRef: MatDialogRef<DeleteAccessCardDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AccessCard
  ) {}

  deleteCard() {
    this._cardsService.deleteCard(this.data.id).subscribe({
      next: () => {
        this._dialogRef.close(true);
        this._sbs.displayMessage('Access Card deleted successfully');
      },
      error: () => {
        this._dialogRef.close(false);
        this._sbs.displayMessage(`Couldn't delete access card`);
      },
    });
  }
}
