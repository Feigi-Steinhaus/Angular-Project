import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { User } from 'src/app/classes/user';
import { ApiService } from 'src/app/service/api.service';
import { TripTypeService } from 'src/app/service/trip-type.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public dialog: MatDialog, private r: Router, public t: TripTypeService, private a: ApiService) { }

  ngOnInit(): void {
    // בדיקה האם השרת שלי מחובר
    this.t.getData().subscribe(
      (rsp) => {
        console.log(rsp);
      },
      // אחרת פותח לי הודעה שאינו מחובר
      (error) => {
        this.dialog.open(DialogComponent, {
          data: {
            subject: 'Error',
            title: 'Error!!',
            context: 'Make sure to connect to the server before continuing browsing',
            end: 'Close'
          }
        });
      }
    );

    // שמירת פרטי המנהל
    const managerString = JSON.stringify({ email: "s@gmail.co", password: "asd1234", userCode: 16 })
    localStorage.setItem('Manager', managerString)
  }
}