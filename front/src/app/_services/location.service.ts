import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Location } from '@/_models/location';

@Injectable({ providedIn: 'root' })
export class LocationService {
    constructor(private http: HttpClient) { }

    getCountries() {
        return this.http.get<Location[]>(`${config.apiUrl}/locations`);
      }

    getProvinces(countryId: number){
        return this.http.get<Location[]>(`${config.apiUrl}/locations/${countryId}`);
    }
}