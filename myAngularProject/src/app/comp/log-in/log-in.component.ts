import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/classes/user';
import { ApiService } from 'src/app/service/api.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  constructor(public r: Router,
    public u: UserService,
    public a: ApiService) { }

  //הגדרה של הטופס
  myForm: FormGroup = new FormGroup({});

  ngOnInit() {

    this.myForm = new FormGroup(
      {
        'name': new FormControl(null, [Validators.required, this.isValidEmail.bind(this)]),
        'pass': new FormControl(null, [Validators.required])
      }
    );
  };

  get myPass() { return this.myForm.controls['pass'] }
  get myName() { return this.myForm.controls['name'] }

  public data: any;
  connected: boolean = false;

  //בדיקה קודם כל האם מדובר במנהל אם לא
  //בדיקה האם המשתמש קיים במערכת
  send() {
    debugger
    //בדיקה האם זה משתמש מנהל
    const manager = localStorage.getItem('Manager');
    if (manager) {
      const m = JSON.parse(manager!);
      if (this.myName.value == m.email && this.myPass.value == m.password) {
        this.a.isManager = true;
        const myUser = JSON.stringify(m);
        localStorage.setItem('currentUser', myUser)
        this.a.isConnected = true;
        this.a.currentUser = m;
        this.r.navigate(['/Trips']);
      }
      else {
        // שליפת משתמש לפי מייל וסיסמה
        this.u.getUser(this.myName.value, this.myPass.value).subscribe((response) => {
          console.log(response);
          this.data = response;
          this.saveMe();
        });
      }
    }
  }

  saveMe() {
    // שליחה לדף ההרשמה אם מדובר בלקוח שאינו קיים במערכת
    if (this.data == null)
      this.r.navigate(['SignIn']);

    // אחרת הכנסה בתור משתמש נוכחי וניתוב לדף הבית
    if (this.data != null) {
      const myUser = JSON.stringify(this.data);
      localStorage.setItem('currentUser', myUser);
      this.a.currentUser = this.data;
      this.a.isConnected = true;
      this.r.navigate(['Home']);
    }
  }

  //תקינות מייל
  isValidEmail(email: AbstractControl) {
    if (!email.value)
      return { 'req': true };
    if (email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value)) {
      return { 'notvalid': true };
    }
    return null;
  }
}