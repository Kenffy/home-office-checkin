import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { CheckinComponent } from './checkin/checkin.component';
import { CheckinDetailsComponent } from './checkin-details/checkin-details.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/checkin', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'checkin', component: CheckinComponent, canActivate: [AuthGuard] },
  {
    path: 'details/:id',
    component: CheckinDetailsComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
