import { Component } from '@angular/core';
import { AuthService } from './common/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(protected _authService: AuthService) {}
}
