import { Injectable } from '@angular/core';
import { AddSession } from 'app/dialogs/add-session/add-session.component';
import { ApiClient } from './api.service';

@Injectable({ providedIn: 'root' })
export class SessionsService {
  constructor(private _api: ApiClient) {}

  getAllSessions() {
    return this._api.get('Session/All');
  }

  addSession(body: AddSession) {
    return this._api.post('Session/withDevice', body);
  }

  deleteSession(id: string) {
    return this._api.delete(`Session?id=${id}`);
  }
}
