import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {

  constructor(public dialog: MatDialog) { }

// שליחה לפתיחת הדיאלוג - הודעה שהודעה התקבלה
  openDialog() {
    this.dialog.open(DialogComponent, {
      data: {
        subject: 'Contact',
        title: 'Thank you for contacting us',
        context: 'We will make every effort to respond to you as soon as possible',
        end: 'Close'
      }
    });
  }
}
