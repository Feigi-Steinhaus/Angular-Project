import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { User } from 'src/app/classes/user';
import { ApiService } from 'src/app/service/api.service';
import { UserService } from 'src/app/service/user.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  constructor(public r: Router,
    public u: UserService,
    public a: ApiService,
    private dialog: MatDialog) { }

  myForm: FormGroup = new FormGroup({});

  open: boolean = false;

  passValidation: string = ""

  ngOnInit() {
    if (this.a.isConnected)
      this.open = true;

    this.myForm = new FormGroup(
      {
        'firstName': new FormControl(null, [this.isValidName.bind(this)]),
        'lastName': new FormControl(null, [this.isValidName.bind(this)]),
        'phone': new FormControl(null, [Validators.required, Validators.minLength(9), Validators.maxLength(10)]),
        'email': new FormControl(null, [this.isValidEmail.bind(this)]),
        'pass': new FormControl(null, [this.isValidPassword.bind(this)]),
        'passValidation': new FormControl(null, [this.ismatched.bind(this)])
      }
    );

    // שליפת שם המשתמש הנוכחי
    const ans = this.user;
    if (ans) {
      this.currentUser = JSON.parse(this.user);
      this.a.isConnected = true;
    }
    const user = localStorage.getItem('currentUser');
    if (user)
      this.newUser = JSON.parse(user);
    console.log(this.newUser);
  };

  // משתני הטופס
  get myPass() { return this.myForm.controls['pass'] }
  get myPassV() { return this.myForm.controls['passValidation'] }
  get myFN() { return this.myForm.controls['firstName'] }
  get myLN() { return this.myForm.controls['lastName'] }
  get myPhone() { return this.myForm.controls['phone'] }
  get myMail() { return this.myForm.controls['email'] }

  res: boolean = false;
  newUser: User = new User();
  currentUser: User = new User();
  user: any = localStorage.getItem('currentUser');
  myUser: User = new User();

  //הוספת הלקוח שנרשם לרשימת הלקוחות שלי
  send() {
    debugger
    this.a.isConnected = true;
    //בדיקה האם זה לקוח חדש או לקוח שמשנה פרטים
    console.log(this.newUser);
    const user = localStorage.getItem('currentUser')
    if (user)
      this.myUser = JSON.parse(user!)
    console.log(this.myUser);
    if (this.myUser.email == null)
      this.addUser();
    else
      this.updateUser();
  }

  // פונקציה לעדכון פרטי משתמש
  updateUser() {
    this.newUser.userCode = this.myUser.userCode;
    this.u.updateUser(this.newUser).subscribe((response) => {
      console.log("update user:");
      console.log(response);
      this.res = response;
      if (response == true) {
        const user = JSON.stringify(this.newUser)
        localStorage.setItem('currentUser', user);
      }
      location.reload(); 
    })

    // this.a.currentUser = this.newUser;
  }

  // פונקציה להוספת משתמש
  addUser() {
    debugger
    console.log("i am in the add func");
    this.u.addUserByInt(this.newUser).subscribe((response) => {
      console.log("add user:");
      console.log(response);
      this.newUser.userCode = response;
    });
    const user = JSON.stringify(this.newUser)
    localStorage.setItem('currentUser', user);
    this.a.currentUser = this.newUser;
    this.dialog.open(DialogComponent, {
      data: {
        subject: 'Success',
        title: 'Success!!',
        context: 'You have been successfully entered into the system',
        end: 'Close'
      }
    });
    this.r.navigate(['Home'])
    location.reload(); 
    // this.a.currentUser = this.newUser;
  }

  // מחיקת משתמש
  deleteUser() {
    debugger
    console.log("i am int the delete user func");
    this.u.deleteUser(this.currentUser.userCode).subscribe((response) => {
      console.log("delete User");
      console.log(response);
      if (response == true) {
        this.a.isConnected = false;
        this.a.currentUser = new User();
        localStorage.setItem('currentUser', JSON.stringify(new User()))
        this.newUser = new User()
        this.dialog.open(DialogComponent, {
          data: {
            subject: 'Success',
            title: 'Success',
            context: 'Successfully removed from the system',
            end: 'Close'
          }
        })
      }
      if (response == false) {
        this.dialog.open(DialogComponent, {
          data: {
            subject: 'Error',
            title: 'The deletion failed',
            context: 'Sorry, it is not possible to delete a customer who has trips',
            end: 'Close'
          }
        })
      }
    }
    )
    location.reload(); 
  }

  //תקינות לשם
  isValidName(name: AbstractControl) {
    if (!name.value)
      return { 'req': true };
    if (name.value.length < 3)
      return { 'min': true };
    if (name.value && (!/^[א-ת\s]+$/.test(name.value) && !/^[a-zA-Z]+$/.test(name.value))) {
      return { 'notvalid': true };
    }
    return null;
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

  //תקינות סיסמה
  isValidPassword(password: AbstractControl) {
    if (!password.value)
      return { 'req': true };
    if (password.value.length < 6)
      return { 'min': true };
    if (password && !/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/.test(password.value)) {
      return { 'notvalid': true };
    }
    return null;
  }

  // אימות סיסמה
  ismatched(password: AbstractControl) {
    if (!password.value)
      return { 'req': true };
    if (password.value != this.myPass.value)
      return { 'notvalid': true };
    return null;
  }
}