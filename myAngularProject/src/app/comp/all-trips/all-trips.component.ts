import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Trip } from 'src/app/classes/trip';
import { TripType } from 'src/app/classes/tripType';
import { ApiService } from 'src/app/service/api.service';
import { TripService } from 'src/app/service/trip.service';
import { TripTypeService } from 'src/app/service/trip-type.service';
import { UserService } from 'src/app/service/user.service';
import { BookingService } from 'src/app/service/booking.service';
import { BookingPlace } from 'src/app/classes/booking';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';
import { TripDialogComponent } from '../trip-dialog/trip-dialog.component';

@Component({
  selector: 'app-all-trips',
  templateUrl: './all-trips.component.html',
  styleUrls: ['./all-trips.component.css']
})

export class AllTripsComponent implements OnInit {

  constructor(public s: TripService,
    public a: ApiService,
    public t: TripTypeService,
    public r: Router,
    public ar: ActivatedRoute,
    public u: UserService,
    public b: BookingService,
    public dialog: MatDialog) { }

  // רשימת סוגי הטיולים
  tripTypes: Array<TripType> = new Array<TripType>();
  // רשימת הטיולים המלאה
  allTrips: Array<Trip> = new Array<Trip>();
  // רשימת הטיולים המסוננת
  filterTrips: Array<Trip> = new Array<Trip>();
  // רשימת ההזמנות שלי
  allMyBookings: any;

  // שמירת בחירת הלקוח- איך למיין או לסנן
  selectedType: number = 0;
  selectedTime: number = 0;
  sort: number = 0;

  // משתנים לקומפוננטה
  connected: boolean = false;
  filterMyTrips: boolean = false;
  isOpen: boolean = false;
  openNote: boolean = false;
  isNote: boolean = false;
  openCode: number = 0;

  ngOnInit(): void {
    this.openNote = false;

    // שליפת קוד משתמש שנשלח כפרמטר בניתוב
    const userCode = Number(this.ar.snapshot.paramMap.get('userCode'));
    if (userCode != 0) {
      this.isNote = true;
      this.filterMyTrips = true;
      // שליפת כל הטיולים של המשתמש הנוכחי
      this.u.getTrips(userCode).subscribe((response) => {
        this.filterTrips = response;
      })
    }

    // שליפת רשימת הטיולים
    if (!this.filterMyTrips) {
      this.s.getData().subscribe((response) => {
        // this.data = response;
        this.allTrips = response;
        this.filterTrips = response;
      });
    }

    // בדיקה האם הלקוח מחובר
    const user = localStorage.getItem('currentUser');
    if (user) {
      this.a.currentUser = JSON.parse(user!);
      this.connected = true
    }

    // שליפת כל ההזמנות של הלקוח הנוכחי
    this.u.getTrips(this.a.currentUser.userCode).subscribe((res) => {
      this.allMyBookings = res;
    });

    // שליפת רשימת סוגי הטיולים
    this.t.getData().subscribe((response) => {
      this.tripTypes = response;
    })
  }

  // סינון הרשימה לפי סוג
  filter(selectedType: number) {
    this.openNote = false;
    this.filterTrips = this.allTrips.filter(o => o.typeCode == selectedType);
    // בדיקה האם קיים טיול מסוג זה
    if (this.filterTrips.length == 0)
      this.openNote = true;
  }

  // פתיחת הדיאלוג עם כל הפרטים
  moreDetails(tripCode: number) {
    this.dialog.open(TripDialogComponent, {
      data: {
        code: tripCode
      }
    })
  }

  // סינון לפי זמן
  filterTime(selectedTime: number) {
    this.openNote = false;
    if (selectedTime == 1) {
      this.filterTrips = this.allTrips.filter(x => new Date(x.tripDate!) > new Date())
    }
    else {
      this.filterTrips = this.allTrips.filter(x => new Date(x.tripDate!) < new Date())
    }
  }

  // מיון הרשימה לפי סוג או מחיר
  sortTrips(sort: number) {
    //type
    if (sort == 1) {
      this.filterTrips = this.allTrips.sort((a, b) => a.typeCode! - b.typeCode!);
    }
    //price
    else {
      this.filterTrips = this.allTrips.sort((a, b) => a.price! - b.price!);
    }
    if (this.filterTrips.length == 0)
      this.openNote = true;
  }

  // הוספת טיול חדש-למנהל בלבד
  new() {
    this.r.navigate(['./NewTrip'])
  }

  booking: BookingPlace = new BookingPlace();
  // שליחה לביטול הרישום לטיול
  cancle(tripCode: number) {
    this.dialog.open(DialogComponent, {
      data: {
        subject: 'Cancle',
        title: 'Pay attention',
        tripCode: tripCode,
        context: 'Are yfou sure you want to cancel the registration for this trip?',
        cancle: 'Accept',
        end: 'Close'
      }
    });
    this.u.getTrips(this.a.currentUser.userCode).subscribe((res) => {
      this.allMyBookings = res;
    });
  }

  numOfPlaces: number = 0;
  openPlaces: boolean = false;

  trip: Trip = new Trip();
  res: boolean = false;
  newBooking: BookingPlace = new BookingPlace();
  bookings: any;

  // בדיקה האם הלקוח שלי רשום לטיול הנוכחי
  checkMe(codeTrip: number) {
    this.u.getTrips(codeTrip).subscribe((response) => {
      this.bookings = response;
      if (this.bookings.length > 0)
        return true
      else
        return false;
    })
  }

  isCancle(codeTrip: number): boolean {
    const list = this.allMyBookings.filter((x: BookingPlace) => x.tripCode == codeTrip)
    console.log(list);
    if (list.length > 0)
      return true;
    else
      return false;
  }
}