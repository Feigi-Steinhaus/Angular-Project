import { Component, Inject, OnInit } from '@angular/core';
import { validateBasis } from '@angular/flex-layout';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { BookingPlace } from 'src/app/classes/booking';
import { Trip } from 'src/app/classes/trip';
import { ApiService } from 'src/app/service/api.service';
import { BookingService } from 'src/app/service/booking.service';
import { TripService } from 'src/app/service/trip.service';
import { DialogComponent } from '../dialog/dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trip-dialog',
  templateUrl: './trip-dialog.component.html',
  styleUrls: ['./trip-dialog.component.css']
})
export class TripDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public p: any,
    private t: TripService,
    public a: ApiService,
    private b: BookingService,
    private dialog: MatDialog,
    private r: Router) { }

  tripCode: number = 0;
  data: Trip = new Trip();
  open: boolean = false;
  numOfPlaces: number = 0;
  newBooking: BookingPlace = new BookingPlace();

  myForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    // שליפת קוד טיול
    this.tripCode = this.p.code;

    // שליפת הטיול עצמו
    this.t.getTrip(this.p.code).subscribe((res) => {
      console.log(res);
      this.data = res;
    })
    this.data.photo = this.a.basePic;

    this.myForm = new FormGroup({
      'count': new FormControl(null, this.check.bind(this)),
    })
  }

  get myCount() { return this.myForm.controls['count'] }

  //בדיקת תקינות לכמות הזמנת מקומות
  check(count: AbstractControl) {
    console.log("i am in the check");
    if (!count.value)
      return { 'req': true };
    if (count.value < 1)
      return { 'min': true };
    if (count.value > this.data.availablePlaces!) {
      return { 'max': true };
    }
    return null;
  }

  // פתיחת האפשרות להזמנת מקומות
  openOrder() {
    this.open = true;
  }

  // הוספת הזמנה
  async addBooking() {
    debugger
    const user = localStorage.getItem('currentUser');
    const u = JSON.parse(user!);
    console.log(user);
    this.newBooking.numOfPlaces = this.myCount.value;
    this.newBooking.userCode = u.userCode;
    this.newBooking.tripCode = this.data.tripCode;
    console.log(this.newBooking);
    try {
      const res = await this.b.getData().toPromise();
      console.log(res);
      const exist = res.filter((x: BookingPlace) => x.userCode == this.newBooking.userCode && x.tripCode == this.newBooking.tripCode);
      console.log(exist);
      if (exist.length > 0) {
        console.log(exist[0].bookingCode);
        this.newBooking.bookingCode = exist[0].bookingCode;
        this.dialog.open(DialogComponent, {
          data: {
            subject: 'Pay attention',
            title: 'Success',
            context: 'There is already a reservation in your name for this trip, the number of places has been updated in the existing reservation',
            end: 'Close'
          }
        });
        console.log("this is the num");
        console.log(this.newBooking);
        this.b.updateBooking(this.newBooking).subscribe((res) => {
          console.log(res);
          if (res) {
            const count = this.data.availablePlaces! - this.numOfPlaces;
            const updateResponse = this.t.updateTrip(this.data.tripCode!, count - exist.count).toPromise();
            console.log(updateResponse);
          }
        })
      }
      // בדיקה האם קיים כבר הזמנה עם הלקוח והטיול הנוכחי, אם כן משנה רק את הכמות        
      else {
        const res = await this.b.addBooking(this.newBooking).toPromise();
        console.log("add booking:");
        console.log(res);
        if (res == true) {
          console.log(this.newBooking);
          console.log(this.data.availablePlaces);
          console.log(this.newBooking.numOfPlaces);
          console.log(this.data.availablePlaces! - this.newBooking.numOfPlaces!);

          const count = this.data.availablePlaces! - this.newBooking.numOfPlaces!;
          const updateResponse = this.t.updateTrip(this.data.tripCode!, count).toPromise();
          // הודעה שההזמנה נקלטה בהצלחה
          this.dialog.open(DialogComponent, {
            data: {
              subject: 'Success',
              title: 'Success',
              context: 'Your order has been successfully received',
              end: 'Close'
            }
          });
          location.reload(); 
          this.r.navigate(['Trips']);
          this.t.getTrip(this.p.code).subscribe((res) => {
            console.log(res);
            this.data = res;
          })
          console.log(updateResponse);
        }
      }
    }
    catch (exe) {
      this.dialog.open(DialogComponent, {
        data: {
          subject: 'Error',
          title: 'Error',
          context: 'Your order could not be received, please try again later',
          end: 'Close'
        }
      });
    }
  }
}