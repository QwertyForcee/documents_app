import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserStatisticsService } from '../../../services/user-statistics.service';
import { Subject, takeUntil } from 'rxjs';
import { UserStatisticsModel } from '../../../models/user-statistics-model';

@Component({
  selector: 'app-statistics-page',
  standalone: true,
  imports: [],
  templateUrl: './statistics-page.component.html',
  styleUrl: './statistics-page.component.scss'
})
export class StatisticsPageComponent implements OnInit, OnDestroy {
  userStatistics: UserStatisticsModel[] = [];
  private readonly destroy$ = new Subject<void>();

  constructor(private service: UserStatisticsService) { }

  ngOnInit(): void {
    this.service.getStatistics()
      .pipe(
        takeUntil(this.destroy$)
      )
      .subscribe({
        next: (res) => {
          this.userStatistics = res;
        }
      })
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
