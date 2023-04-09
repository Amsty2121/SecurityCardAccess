import { Injectable } from '@angular/core';
import { ApiClient } from 'app/common/services';
import { AddDevice } from 'app/dialogs/add-device/add-device.component';
import { AccessLevel } from '../models';

@Injectable({ providedIn: 'root' })
export class DevicesService {
  constructor(private _api: ApiClient) {}

  getAllDevices() {
    return this._api.get('device/All');
  }

  editAccessLevel(id: string, accessLevel: AccessLevel) {
    return this._api.patch('Device/change-access', { id, accessLevel });
  }

  addDevice(body: AddDevice) {
    return this._api.post('Device', body);
  }

  deleteDevice(id: string) {
    return this._api.delete(`Device?id=${id}`);
  }
}
