import { Time } from "@angular/common";

export class Trip {
    constructor(public tripCode?: number,
        public tripDestination?: String,
        public typeCode?: number,
        public typeName?: String,
        public tripDate?: Date,
        public departureTime?: Time,
        public tripDurationHours?: number,
        public availablePlaces?: number,
        public price?: number,
        public photo?: String,
        public isMedicNeeded?:boolean) { }
}