import { Component } from '@angular/core';
import { IEmployee } from '../models/employee';
import { IHomeOfficeTime } from '../models/home-office-time';
import { CheckinService } from '../services/checkin.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { CheckinDetailsComponent } from '../checkin-details/checkin-details.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checkin',
  standalone: true,
  imports: [CheckinDetailsComponent, CommonModule],
  templateUrl: './checkin.component.html',
  styleUrl: './checkin.component.css',
})
export class CheckinComponent {
  openModal: boolean = false;
  modalActive: string = '';
  homeOfficeStarted: boolean = false;
  authUser: IEmployee | null = null;
  currentHomeOfficeTime: IHomeOfficeTime | null = null;

  constructor(
    private checkinService: CheckinService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadAuthUser();
    this.loadOpenHomeOfficeTime();
  }

  loadAuthUser() {
    this.authService.currentUser$.subscribe({
      next: (value) => {
        this.authUser = value;
      },
      error: (err) => console.log(err),
    });
  }

  loadOpenHomeOfficeTime() {
    this.checkinService.currentCheckinTime$.subscribe({
      next: (value) => {
        if (value) {
          console.log(value);
          this.currentHomeOfficeTime = value;
          this.homeOfficeStarted = true;
        } else {
          this.currentHomeOfficeTime = null;
          this.homeOfficeStarted = false;
        }
      },
      error: (err) => console.log(err),
      complete: () => console.log('check completed'),
    });
  }

  startHomeOffice() {
    if (!this.authUser) return;
    const date = new Date();
    const currentTime: IHomeOfficeTime = {
      id: 0,
      userId: this.authUser.id,
      createdAt: date,
      updatedAt: null,
      startTime: this.getTime(date),
      endTime: '',
    };

    this.checkinService.startHomeOfficeTime(currentTime).subscribe({
      next: (response) => console.log(response),
      error: (err) => console.log(err),
    });
  }

  stopHomeOffice() {
    if (this.currentHomeOfficeTime) {
      const date = new Date();
      let currentTime = this.currentHomeOfficeTime;
      currentTime.updatedAt = date;
      currentTime.endTime = this.getTime(date);

      this.checkinService.stopHomeOfficeTime(currentTime).subscribe({
        next: (response) => console.log(response),
        error: (err) => console.log(err),
      });
    }
  }

  getTime = (date: Date) => {
    if (!date) return '';
    return date.toLocaleTimeString(navigator.language, {
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  onHomeOfficeTimeDetails() {
    if (this.authUser) {
      this.router.navigateByUrl(`/details/${this.authUser.id}`);
    }
  }

  toggleModal(args: boolean) {
    if (args) {
      this.openModal = true;
      this.modalActive = 'active';
    } else {
      this.openModal = false;
      this.modalActive = '';
    }
  }
}
