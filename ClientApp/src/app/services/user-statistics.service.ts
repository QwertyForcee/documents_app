import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserStatisticsModel } from '../models/user-statistics-model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserStatisticsService {
  private apiUrl = `${environment.apiUrl}/UserStatistics`;

  constructor(private http: HttpClient) { }

  getStatistics(): Observable<UserStatisticsModel[]> {
    return this.http.get<UserStatisticsModel[]>(this.apiUrl);
  }
}
