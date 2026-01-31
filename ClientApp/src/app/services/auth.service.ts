import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';
import { AuthResponse } from '../models/auth-response';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/auth';

  private isUserSignedInSub$ = new BehaviorSubject<boolean>(this.isSignedIn());
  isUserSignedIn$ = this.isUserSignedInSub$.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  signIn(data: { email: string; password: string }): Observable<any> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/sign-in`, data)
      .pipe(
        tap({
          next: (result: AuthResponse) => {
            this.setToken(result?.accessToken);
            this.isUserSignedInSub$.next(true);
          }
        }
        )
      );
  }

  signUp(user: any) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/sign-up`, user)
      .pipe(
        tap({
          next: (result: AuthResponse) => {
            this.setToken(result?.accessToken);
            this.isUserSignedInSub$.next(true);
          }
        }
        )
      );
  }

  private setToken(token: string) {
    localStorage.setItem('jwt', token);
  }

  getToken() {
    return localStorage.getItem('jwt');
  }

  logout() {
    this.isUserSignedInSub$.next(false);
    localStorage.removeItem('jwt');
    this.router.navigate(['/sign-in']);
  }

  private isSignedIn(): boolean {
    return !!localStorage.getItem('jwt');
  }
}
