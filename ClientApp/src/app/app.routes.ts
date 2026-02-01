import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { SignInComponent } from './pages/auth/sign-in/sign-in.component';
import { SignUpComponent } from './pages/auth/sign-up/sign-up.component';
import { DocumentsPageComponent } from './pages/documents/documents-page/documents-page.component';
import { StatisticsPageComponent } from './pages/statistics/statistics-page/statistics-page.component';
import { DocumentStatus } from './models/documents-models';

export const routes: Routes = [
    { path: '', redirectTo: 'docs', pathMatch: 'full' },
    { path: 'docs/:id', component: DocumentsPageComponent, canActivate: [authGuard], data: { isReadOnly: false, documentStatus: DocumentStatus.Active } },
    { path: 'docs', component: DocumentsPageComponent, canActivate: [authGuard], data: { isReadOnly: false, documentStatus: DocumentStatus.Active } },
    { path: 'history', component: DocumentsPageComponent, canActivate: [authGuard], data: { isReadOnly: true, documentStatus: DocumentStatus.Expired } },
    { path: 'statistics', component: StatisticsPageComponent, canActivate: [authGuard] },
    { path: 'sign-in', component: SignInComponent },
    { path: 'sign-up', component: SignUpComponent },
];
