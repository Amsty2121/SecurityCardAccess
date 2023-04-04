import { Injectable } from '@angular/core';
import { ApiClient } from 'app/common/services';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private _api: ApiClient) {}

  login({ login, password }) {
    return this._api.post(
      'Account/login',
      { login, password },
      { responseType: 'text' }
    );
  }
}
