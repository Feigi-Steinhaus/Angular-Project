import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Trip } from 'src/app/classes/trip';
import { TripService } from 'src/app/service/trip.service';

@Component({
  selector: 'app-new-trip',
  templateUrl: './new-trip.component.html',
  styleUrls: ['./new-trip.component.css']
})
export class NewTripComponent implements OnInit {

  constructor(public r: Router, public t: TripService) { }

  myForm: FormGroup = new FormGroup({});

  ngOnInit() {
    this.myForm = new FormGroup(
      {
        'typeCode': new FormControl(null, [Validators.required]),
        'tripDestination': new FormControl(null, [Validators.required]),
        'price': new FormControl(null, [Validators.required, Validators.min(0)]),
        'availablePlaces': new FormControl(null, [Validators.required, Validators.min(0)]),
        'tripDurationHours': new FormControl(null, [Validators.required, Validators.min(0)]),
        'departureTime': new FormControl(null, Validators.required),
        'tripDate': new FormControl(null, [Validators.required])
      }
    );
  };

  get myType() { return this.myForm.controls['typeCode'] }
  get myDes() { return this.myForm.controls['tripDestination'] }
  get myPrice() { return this.myForm.controls['price'] }
  get myPlaces() { return this.myForm.controls['availablePlaces'] }
  get myHours() { return this.myForm.controls['tripDurationHours'] }
  get myTime() { return this.myForm.controls['departureTime'] }
  get myDate() { return this.myForm.controls['tripDate'] }

  newTrip: Trip = new Trip();

  send() {
    console.log("i am in the trip!!");
    this.t.addTrip(this.newTrip).subscribe((response) => {
      console.log("add trip:");
      console.log(response);
    });
  }
}
