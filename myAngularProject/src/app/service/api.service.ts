import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../classes/user';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }

  baseRoute = "../../assets";
  baseUrl = 'https://localhost:7154';
  basePic = "../../assets/15.png";
  baseDate: Date = new Date();
  isConnected: boolean = false;
  isManager: boolean = false;
  currentUser: User = new User();
}
