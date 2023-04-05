import { Injectable } from '@angular/core';
import { ApiClient } from 'app/common/services';
import Cookies from 'js-cookie';

export const AUTH_TOKEN = 'auth_token';

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

  checkToken() {
    return this._api.get('Account/tokenVerify');
  }

  token() {
    return Cookies.get(AUTH_TOKEN);
  }
}
