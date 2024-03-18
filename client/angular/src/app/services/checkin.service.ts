import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { ReplaySubject, map } from 'rxjs';
import { IHomeOfficeTime } from '../models/home-office-time';
import { HttpClient } from '@angular/common/http';
import { IResponse } from '../models/response';

@Injectable({
  providedIn: 'root',
})
export class CheckinService {
  baseUrl: string = environment.apiUrl;
  private checkinTimeSource = new ReplaySubject<IHomeOfficeTime | null>();
  currentCheckinTime$ = this.checkinTimeSource.asObservable();

  constructor(private http: HttpClient) {}

  loadCurrentHomeOfficeTime(id: string) {
    return this.http.get<IResponse>(this.baseUrl + '/checkin/' + id).pipe(
      map((response) => {
        const homeOffice: IHomeOfficeTime = response.result;
        localStorage.setItem('ho-id', homeOffice.id.toString());
        this.checkinTimeSource.next(homeOffice);
      })
    );
  }

  startHomeOfficeTime(data: any) {
    return this.http.post<IResponse>(this.baseUrl + '/checkin/', data).pipe(
      map((response) => {
        const homeOffice: IHomeOfficeTime = response.result;
        localStorage.setItem('ho-id', homeOffice.id.toString());
        this.checkinTimeSource.next(homeOffice);
      })
    );
  }

  stopHomeOfficeTime(data: any) {
    return this.http
      .put<IResponse>(this.baseUrl + '/checkin/' + data.id, data)
      .pipe(
        map(() => {
          localStorage.removeItem('ho-id');
          this.checkinTimeSource.next(null);
        })
      );
  }

  getHomeOfficeTimesByDayAsync(userId?: string, date?: string) {
    return this.http
      .get<IResponse>(this.baseUrl + `/checkin/${userId}/${date}`)
      .pipe(
        map((response) => {
          const homeOfficeTimes: IHomeOfficeTime[] = response.result;
          return homeOfficeTimes;
        })
      );
  }
}
