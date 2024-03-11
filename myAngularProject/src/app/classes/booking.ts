import { Time } from "@angular/common";

export class BookingPlace {

    constructor(public bookingCode?: number,
        public userCode?: number,
        public bookingDate?: Date,
        public bookingTime?: number,
        public tripCode?: number,
        public numOfPlaces?: number) { }
}