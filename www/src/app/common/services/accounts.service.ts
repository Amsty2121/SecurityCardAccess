import { Injectable } from '@angular/core';
import { AddAccount } from 'app/dialogs/add-account/add-account.component';
import { ApiClient } from './api.service';

@Injectable({ providedIn: 'root' })
export class AccountsService {
  constructor(private _api: ApiClient) {}

  getAllAccounts() {
    return this._api.get('Account/users');
  }

  deleteAccount(id: string) {
    return this._api.delete(`Account/delete?id=${id}`);
  }

  addAccount(body: AddAccount) {
    return this._api.post('Account/register', body);
  }
}
