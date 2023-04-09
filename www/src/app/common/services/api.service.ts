import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { devEnvironment as env } from 'environments';
import { merge } from 'lodash-es';

@Injectable({ providedIn: 'root' })
export class ApiClient {
  private readonly _defaultOptions = {
    params: null,
    responseType: 'json',
    withCredentials: true,
    headers: {
      'Cache-Control': 'no-cache, no-store, must-revalidate',
      Pragma: 'no-cache',
      Expires: '0',
    },
  };

  constructor(private _http: HttpClient) {}

  get(url: string, requestOptions?): Observable<any> {
    return this._http.get(
      this._buildUrl(url),
      this._buildHttpOptions(requestOptions)
    );
  }

  post(url: string, body: any, requestOptions?): Observable<any> {
    return this._http.post(
      this._buildUrl(url),
      body,
      this._buildHttpOptions(requestOptions)
    );
  }

  patch(url: string, body: any, requestOptions?): Observable<any> {
    return this._http.patch(
      this._buildUrl(url),
      body,
      this._buildHttpOptions(requestOptions)
    );
  }

  delete(url: string, requestOptions?): Observable<any> {
    return this._http.delete(
      this._buildUrl(url),
      this._buildHttpOptions(requestOptions)
    );
  }

  private _buildUrl(urlChunk: string): string {
    return `${env.apiHost}${urlChunk}`;
  }

  private _buildHttpOptions(requestOptions): any {
    var options = merge({}, this._defaultOptions, requestOptions);
    options.headers = new HttpHeaders(options.headers);
    return options;
  }
}
