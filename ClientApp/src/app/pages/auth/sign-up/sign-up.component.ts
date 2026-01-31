import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent {
  email = '';
  name = '';
  password = '';
  confirmPassword = '';

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match');
      return;
    }

    this.auth.signUp({ email: this.email, password: this.password, name: this.name })
      .subscribe({
        next: (_) => {
          this.router.navigate(['/docs']);
        },
        error: (err) => {
          console.log('Sign up error', err);
        }
      });
  }
}
