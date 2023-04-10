import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiClient } from 'app/common/services';
import Cookies from 'js-cookie';
import jwt_decode from 'jwt-decode';

export const AUTH_TOKEN = 'auth_token';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private _api: ApiClient, private _router: Router) {}

  login({ login, password }) {
    return this._api.post(
      'Account/login',
      { login, password },
      { responseType: 'text' }
    );
  }

  logout() {
    Cookies.remove(AUTH_TOKEN);
    this._router.navigate(['auth']);
  }

  checkToken() {
    return this._api.get('Account/tokenVerify');
  }

  token() {
    return Cookies.get(AUTH_TOKEN);
  }

  username() {
    const token = Cookies.get(AUTH_TOKEN);
    return token ? jwt_decode(Cookies.get(AUTH_TOKEN)) : '';
  }
}
