import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { TripType } from '../classes/tripType';

@Injectable({
  providedIn: 'root'
})
export class TripTypeService {

  constructor(private http: HttpClient, private api: ApiService) { }

  public getData(): Observable<any> {
    console.log("i am in the TripType ser");
    return this.http.get<any>(`${this.api.baseUrl}/TripType`);
  }
}
