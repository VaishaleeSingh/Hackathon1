import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'jobs', pathMatch: 'full' },
  { 
    path: 'auth/login', 
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent) 
  },
  { 
    path: 'auth/register', 
    loadComponent: () => import('./features/auth/register/register.component').then(m => m.RegisterComponent) 
  },
  { 
    path: 'jobs', 
    loadComponent: () => import('./features/jobs/job-list/job-list.component').then(m => m.JobListComponent) 
  },
  { 
    path: 'jobs/:id', 
    loadComponent: () => import('./features/jobs/job-detail/job-detail.component').then(m => m.JobDetailComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'applications', 
    loadComponent: () => import('./features/applications/status/status.component').then(m => m.StatusComponent),
    canActivate: [authGuard],
    data: { roles: ['Candidate'] }
  },
  { 
    path: 'admin/dashboard', 
    loadComponent: () => import('./features/admin/dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard],
    data: { roles: ['Admin', 'Recruiter'] }
  }
];
