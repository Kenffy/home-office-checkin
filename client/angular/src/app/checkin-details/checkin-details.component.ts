import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IEmployee } from '../models/employee';
import { IHomeOfficeTime } from '../models/home-office-time';
import { CheckinService } from '../services/checkin.service';
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
  @Input() user: IEmployee | null = null;
  @Output() closeModal = new EventEmitter<boolean>();

  selectedDate: any;
  currentDate = '';
  authUser: IEmployee | null = null;
  homeOfficeTimes: IHomeOfficeTime[] = [];

  constructor(private checkinService: CheckinService) {}

  ngOnInit(): void {
    this.getFormattedCurrentDate();
    this.getHomeOfficeTimes(this.selectedDate);
  }

  loadHomeOfficeTimes() {
    if (!this.user || !this.selectedDate) return;
    this.getHomeOfficeTimes(this.selectedDate);
  }

  getHomeOfficeTimes(date: any) {
    if (!this.user) return;
    this.checkinService
      .getHomeOfficeTimesByDayAsync(this.user.id, date)
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

  onCloseModal() {
    this.closeModal.emit();
  }
}
