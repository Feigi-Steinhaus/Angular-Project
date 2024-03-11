import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/service/api.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  data: any;

  constructor(private u: UserService, private a: ApiService) { }

  ngOnInit(): void {
    // שליפת רשימת המשתמשים
    this.u.getData().subscribe((response) => {
      console.log(response);
      this.data = response;
    });

    const user = localStorage.getItem('currentUser');
    const usr = JSON.parse(user!);
    const manager = localStorage.getItem('Manager');
    const mng = JSON.parse(manager!);
    
    // בדיקה האם זה המנהל
    if (usr.email == mng.email && usr.password == mng.password)
      this.a.isManager = true;
  }

}
