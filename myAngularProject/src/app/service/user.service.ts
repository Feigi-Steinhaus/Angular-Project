import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { User } from '../classes/user';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private http: HttpClient, private api: ApiService) { }

  public getData(): Observable<any> {
    console.log("i am in the user ser-getting");
    return this.http.get<any>(`${this.api.baseUrl}/User`);
  }

  public getUser(mail: string, pass: string): Observable<any> {
    const url = `${this.api.baseUrl}/User/GetByMailAndPass`;
    let params = new HttpParams();
    params = params.set('mail', mail);
    params = params.set('pass', pass);
    return this.http.get<any>(url, { params });
  }

  public getById(id: number): Observable<any> {
    const url = `${this.api.baseUrl}/User`;
    return this.http.get<any>(`${url}/${id}`);
  }

  public getTrips(id: number): Observable<any> {
    const url = `${this.api.baseUrl}/User/GetAllTrips/${id}`;
    return this.http.get<any>(url);
  }

  public addUser(u: User): Observable<any> {
    const url = `${this.api.baseUrl}/User`;
    return this.http.post<any>(url, u);
  }

  public addUserByInt(u: User): Observable<any> {
    const url = `${this.api.baseUrl}/User/AddByInt`;
    return this.http.post<any>(url, u);
  }

  public updateUser(u: User): Observable<boolean> {
    const url = `${this.api.baseUrl}/User`;
    return  this.http.put<boolean>(url, u);
  }

  public deleteUser(code: number): Observable<boolean> {
    const url = `${this.api.baseUrl}`;
    return this.http.delete<boolean>(`${url}/${code}`)
  }
}