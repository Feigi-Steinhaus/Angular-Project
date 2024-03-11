import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './comp/home/home.component';
import { SignInComponent } from './comp/sign-in/sign-in.component';
import { LogInComponent } from './comp/log-in/log-in.component';
import { AllTripsComponent } from './comp/all-trips/all-trips.component';
import { UsersComponent } from './comp/users/users.component';
import { NewTripComponent } from './comp/new-trip/new-trip.component';
import { DialogComponent } from './comp/dialog/dialog.component';
import { ContactComponent } from './comp/contact/contact.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'Trips', title: 'Sail&Sea-Trips', component: AllTripsComponent },
  { path: 'Trips/:userCode', title: 'Sail&Sea-Trips', component: AllTripsComponent },
  { path: 'NewTrip', component: NewTripComponent },
  { path: 'Home', title: 'Sail&Sea-Home', component: HomeComponent },
  { path: 'SignIn', title: 'Sail&Sea-Register', component: SignInComponent },
  { path: 'LogIn', title: 'Sail&Sea-LogIn', component: LogInComponent },
  { path: 'Users', component: UsersComponent },
  { path: 'Contatct', component: ContactComponent },
  { path: 'Dialog/:title/:context', component: DialogComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
