import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable()
export class LocationService {
  constructor(private http: HttpClient) {
  }
  getLocation() {
    return this.http.get('api/OpenNotify/Get');
  }
  getSavedLocations() {
    return this.http.get('api/OpenNotify/GetSavedLocations');
  }
  saveLocations(location: any) {
    return this.http.post('api/OpenNotify/Save', location);
  }
}
