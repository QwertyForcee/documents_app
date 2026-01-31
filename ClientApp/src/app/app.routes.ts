import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { SignInComponent } from './pages/auth/sign-in/sign-in.component';
import { SignUpComponent } from './pages/auth/sign-up/sign-up.component';
import { DocumentsPageComponent } from './pages/documents/documents-page/documents-page.component';
import { StatisticsPageComponent } from './pages/statistics/statistics-page/statistics-page.component';

export const routes: Routes = [
    { path: '', redirectTo: 'docs', pathMatch: 'full' },
    { path: 'docs', component: DocumentsPageComponent, canActivate: [authGuard] },
    { path: 'statistics', component: StatisticsPageComponent, canActivate: [authGuard] },
    { path: 'sign-in', component: SignInComponent },
    { path: 'sign-up', component: SignUpComponent },
];
