import { NgModule } from '@angular/core';
import {
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogModule,
  MatDialogTitle,
} from '@angular/material/dialog';
import { Component } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table';
import { AppRoutingModule } from './app-routing.module';
import { MatSelectModule } from '@angular/material/select';
import { JsonPipe } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AppComponent } from './app.component';
import { HomeComponent } from './comp/home/home.component';
import { NavComponent } from './comp/nav/nav.component';
import { SignInComponent } from './comp/sign-in/sign-in.component';
import { LogInComponent } from './comp/log-in/log-in.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AllTripsComponent } from './comp/all-trips/all-trips.component';
import { HttpClientModule } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';
import { AsyncPipe } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { UsersComponent } from './comp/users/users.component';
import { NewTripComponent } from './comp/new-trip/new-trip.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ContactComponent } from './comp/contact/contact.component';
import { AboutComponent } from './comp/about/about.component';
import { DialogComponent } from './comp/dialog/dialog.component';
import { TripDialogComponent } from './comp/trip-dialog/trip-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    SignInComponent,
    LogInComponent,
    AllTripsComponent,
    UsersComponent,
    NewTripComponent,
    ContactComponent,
    AboutComponent,
    DialogComponent,
    TripDialogComponent
  ],
  imports: [
    MatDialogModule,
    MatMenuModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    JsonPipe,
    MatButtonModule,
    MatTooltipModule,
    HttpClientModule,
    MatSlideToggleModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
    MatCardModule,
    MatTabsModule,
    FormsModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    AsyncPipe
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
