import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.dev';
import { ReplaySubject, map } from 'rxjs';
import { IEmployee } from '../models/employee';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { IResponse } from '../models/response';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = environment.apiUrl;
  private employeeSource = new ReplaySubject<IEmployee | null>();
  currentUser$ = this.employeeSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(creds: any) {
    return this.http
      .post<IResponse>(this.baseUrl + '/employees/login', creds)
      .pipe(
        map((response) => {
          const employee: IEmployee = response.result;
          localStorage.setItem('ho-uid', employee.id);
          this.employeeSource.next(employee);
        })
      );
  }

  logout() {
    localStorage.removeItem('ho-uid');
    this.employeeSource.next(null);
    this.router.navigateByUrl('/login');
  }

  loadCurrentEmployee(id: string) {
    return this.http.get<IResponse>(this.baseUrl + '/employees/' + id).pipe(
      map((response) => {
        const employee: IEmployee = response.result;
        localStorage.setItem('ho-uid', employee.id);
        this.employeeSource.next(employee);
      })
    );
  }

  isLoggedIn(): boolean {
    const userId = localStorage.getItem('ho-uid');
    if (userId) {
      return true;
    } else {
      return false;
    }
  }
}
