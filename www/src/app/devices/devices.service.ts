import { Injectable } from '@angular/core';
import { ApiClient } from 'app/common/services';

@Injectable({ providedIn: 'root' })
export class DevicesService {
  constructor(private _api: ApiClient) {}

  getAllDevices() {
    return this._api.get('device/All');
  }
}
