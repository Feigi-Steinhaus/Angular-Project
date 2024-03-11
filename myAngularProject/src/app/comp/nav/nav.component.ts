import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/classes/user';
import { ApiService } from 'src/app/service/api.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private r: Router, private u: UserService, public a: ApiService) { }

  isConnented: boolean = true;
  myUsers: Array<User> = new Array<User>()
  open: boolean = false;
  newEdit: boolean = true;
  newUser: User = new User();
  user: any = localStorage.getItem('currentUser');
  currentUser: User = new User();

  ngOnInit(): void {

    // שליפת שם המשתמש הנוכחי
    const ans = this.user;
    if (ans) {
      this.currentUser = JSON.parse(this.user);
      this.a.isConnected = true;
    }
    const manager = localStorage.getItem('Manager');
    const mng = JSON.parse(manager!);
    console.log("i am in the mng");
    console.log(mng);
    // בדיקה האם זה המנהל
    if (this.user.email == mng.email && this.user.password == mng.password)
      this.a.isManager = true;
  }

  // עריכת פרטי משתמש
  details() {
    this.r.navigate(['SignIn']);
  }

  // רשימת הטיולים של המשתמש הנוכחי
  myTrips() {
    const user = localStorage.getItem('currentUser')
    let userCode = JSON.parse(user!).userCode;
    this.r.navigate(['Trips', userCode])
  }

  // התנתקות מהחשבון
  disconnect() {
    localStorage.setItem('currentUser', "");
    this.a.isConnected = false;
    this.a.isManager = false;
    this.r.navigate(['Home']);
  }
}
