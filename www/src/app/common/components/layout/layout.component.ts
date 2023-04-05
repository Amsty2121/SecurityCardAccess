import { Component } from '@angular/core';
import { AuthService } from 'app/common/services';

@Component({
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent {
  username = this._auth.username()['UserName'];

  constructor(private _auth: AuthService) {}

  logout() {
    this._auth.logout();
  }
}
