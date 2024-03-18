import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CheckinComponent } from './checkin/checkin.component';
import { CheckinDetailsComponent } from './checkin-details/checkin-details.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'checkin', component: CheckinComponent },
  {
    path: 'details/:id',
    component: CheckinDetailsComponent,
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
