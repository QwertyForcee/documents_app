import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Observable, takeUntil } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AsyncPipe,
    RouterModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';
  isUserLoggedIn$: Observable<boolean>;

  constructor(private authService: AuthService) {
    this.isUserLoggedIn$ = this.authService.isUserSignedIn$;
  }

  onLogOut(): void {
    this.authService.logout();
  } 
}
