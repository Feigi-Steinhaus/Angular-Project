import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, count, map } from 'rxjs';
import { ApiService } from './api.service';
import { HttpParams } from '@angular/common/http';
import { Trip } from '../classes/trip';


@Injectable({
  providedIn: 'root'
})
export class TripService {

  constructor(private http: HttpClient, private api: ApiService) { }

  public getData(): Observable<any> {
    return this.http.get<any>(`${this.api.baseUrl}/Trip`);
  }

  public getTrip(tripCode: number): Observable<any> {
    return this.http.get<any>(`${this.api.baseUrl}/Trip/getByCode/${tripCode}`);
  }

  public addTrip(trip: Trip): Observable<boolean> {
    return this.http.post<boolean>(`${this.api.baseUrl}/Trip`, trip);
  }

  public updateTrip(tripCode: number, count: number): Observable<any> {
    debugger
    return this.http.put<boolean>(`${this.api.baseUrl}/Trip/Places/${tripCode}/${count}`,null);
  }
}