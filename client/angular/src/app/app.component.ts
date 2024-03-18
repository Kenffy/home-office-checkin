import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { AuthService } from './services/auth.service';
import { CheckinService } from './services/checkin.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'HomeOffice-Checkin';

  constructor(
    private authService: AuthService,
    private checkinService: CheckinService
  ) {}

  ngOnInit(): void {
    this.loadCurrentUser();
    this.loadOpenHomeOfficeTime();
  }

  loadCurrentUser() {
    const userId = localStorage.getItem('ho-uid');
    if (userId) {
      this.authService.loadCurrentEmployee(userId).subscribe();
    }
  }

  loadOpenHomeOfficeTime() {
    const userId = localStorage.getItem('ho-uid');
    if (userId)
      this.checkinService.loadCurrentHomeOfficeTime(userId).subscribe();
  }
}
