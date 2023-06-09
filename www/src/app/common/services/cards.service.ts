import { Injectable } from '@angular/core';
import { AccessCard } from 'app/access-cards/access-cards.component';
import { AccessLevel } from '../models';
import { ApiClient } from './api.service';

@Injectable({ providedIn: 'root' })
export class CardsService {
  constructor(private _api: ApiClient) {}

  getAllCards() {
    return this._api.get('AccessCard/All');
  }

  addAccessCard(body: AccessCard) {
    return this._api.post('AccessCard', body);
  }

  deleteCard(id: string) {
    return this._api.delete(`AccessCard?id=${id}`);
  }

  editAccessLevel(id: string, accessLevel: AccessLevel) {
    return this._api.patch('AccessCard/change-access', { id, accessLevel });
  }
}
