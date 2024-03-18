import { Component } from '@angular/core';
import { IEmployee } from '../models/employee';
import { IHomeOfficeTime } from '../models/home-office-time';
import { AuthService } from '../services/auth.service';
import { CheckinService } from '../services/checkin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-checkin-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './checkin-details.component.html',
  styleUrl: './checkin-details.component.css',
})
export class CheckinDetailsComponent {
  selectedDate: any;
  currentDate = '';
  userId: string | null = '';
  authUser: IEmployee | null = null;
  homeOfficeTimes: IHomeOfficeTime[] = [];

  constructor(
    private authService: AuthService,
    private checkinService: CheckinService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.userId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    this.loadAuthUser();
    this.getFormattedCurrentDate();
    this.getHomeOfficeTimes(this.selectedDate);
  }

  onCloseCheckinDetails() {
    this.router.navigateByUrl('/checkin');
  }

  loadAuthUser() {
    this.authService.currentUser$.subscribe({
      next: (value) => {
        this.authUser = value;
      },
      error: (err) => console.log(err),
    });
  }

  loadHomeOfficeTimes() {
    if (!this.userId || !this.selectedDate) return;
    console.log(this.selectedDate);
    this.getHomeOfficeTimes(this.selectedDate);
  }

  getHomeOfficeTimes(date: any) {
    if (!this.userId) return;
    this.checkinService
      .getHomeOfficeTimesByDayAsync(this.userId, date)
      .subscribe({
        next: (response) => {
          this.homeOfficeTimes = response;
        },
        error: (err) => console.log(err),
      });
  }

  getFormattedCurrentDate() {
    const date = new Date();
    const year = date.getFullYear();
    const month = date.getUTCMonth() + 1;
    const day = date.getUTCDate();

    this.selectedDate = `${year}-${month < 10 ? '0' + month : month}-${
      day < 10 ? '0' + day : day
    }`;

    this.currentDate = `${day < 10 ? '0' + day : day}.${
      month < 10 ? '0' + month : month
    }.${year}`;
  }
}
