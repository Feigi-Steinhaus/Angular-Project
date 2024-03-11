import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { BookingPlace } from '../classes/booking';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http: HttpClient, private api: ApiService) { }

  public getData(): Observable<any> {
    return this.http.get<any>(`${this.api.baseUrl}/BookingPlace`);
  }

  public addBooking(b: BookingPlace): Observable<boolean> {
    return this.http.post<boolean>(`${this.api.baseUrl}/BookingPlace`, b);
  }

  public deleteBooking(code: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.api.baseUrl}/BookingPlace/Delete/${code}`);
  }

  public updateBooking(b: BookingPlace): Observable<boolean> {
    return this.http.put<boolean>(`${this.api.baseUrl}/BookingPlace`, b)
  }
}
