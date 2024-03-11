import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingPlace } from 'src/app/classes/booking';
import { ApiService } from 'src/app/service/api.service';
import { BookingService } from 'src/app/service/booking.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {
  constructor(@Inject(MAT_DIALOG_DATA) public p: any,
    public u: UserService,
    private b: BookingService,
    private dialog: MatDialog,
    private r: Router) { }

  // משתני הדיאלוג
  end: string = "";
  title: string = "";
  context: string = "";
  cancle: string = ""

  open: boolean = false;
  numOfPlaces: number = 0;

  booking: any;

  // שליפת הפרמטרים שנשלחו בניתוב והצבה במשתנים
  ngOnInit(): void {

    // בדיקה האם מדובר על שליחה מקומפוננטה מסוימת 
    // שידע האם להציג את הכפתור הנוסף
    if (this.p.subject == "Cancle") {
      this.open = true;
      this.cancle = this.p.cancle
    }
    this.title = this.p.title;
    this.end = this.p.end;
    this.context = this.p.context;
  }

  async remove() {
    debugger;
    const user = localStorage.getItem('currentUser');
    const u = JSON.parse(user!);
    try {
      const res = await this.b.getData().toPromise();
      this.booking = res.filter((x: any) => x.tripCode == this.p.tripCode && x.userCode == u.userCode);
      console.log(this.booking);
      this.delete();
    } catch (error) {
      console.log(error);
    }
  }


  // פונקציית מחיקת הזמנה
  async delete() {
    debugger
    console.log(this.booking[0]);
    try {
      const res = await this.b.deleteBooking(this.booking[0].bookingCode!).toPromise();
      console.log(res);
      if (res == true) {
        this.dialog.open(DialogComponent, {
          data: {
            subject: 'Success',
            title: 'Success',
            context: 'Your order has been successfully deleted from the system',
            end: 'Close'
          }
        });
        location.reload();
      }
    } catch (error) {
      console.log(error);
    }
  }
}
