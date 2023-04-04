import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class SnackbarService {
  constructor(private snackbar: MatSnackBar) {}

  public displayMessage(message: string, action = 'OK', duration = 5000) {
    return this.snackbar.open(message, action, {
      duration,
    });
  }

  public displayDefaultMessage(message: string) {
    this.displayMessage(message, null, null);
  }

  public dismiss() {
    this.snackbar.dismiss();
  }
}
