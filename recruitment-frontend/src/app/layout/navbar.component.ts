import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, MatToolbarModule, MatButtonModule, MatIconModule],
  template: `
    <mat-toolbar color="primary">
      <span>Recruitment System</span>
      <span class="spacer"></span>
      
      <button mat-button routerLink="/jobs">Jobs</button>
      
      <ng-container *ngIf="authService.currentUser() as user; else noUser">
        <button mat-button *ngIf="user.role === 'Admin' || user.role === 'Recruiter'" routerLink="/admin/dashboard">Dashboard</button>
        <button mat-button *ngIf="user.role === 'Candidate'" routerLink="/applications">My Applications</button>
        <span class="user-info">Welcome, {{ user.username }} ({{ user.role }})</span>
        <button mat-icon-button (click)="authService.logout()">
          <mat-icon>logout</mat-icon>
        </button>
      </ng-container>
      
      <ng-template #noUser>
        <button mat-button routerLink="/auth/login">Login</button>
        <button mat-raised-button color="accent" routerLink="/auth/register">Register</button>
      </ng-template>
    </mat-toolbar>
  `,
  styles: [`
    .spacer { flex: 1 1 auto; }
    .user-info { margin: 0 15px; font-size: 0.9rem; opacity: 0.9; }
  `]
})
export class NavbarComponent {
  constructor(public authService: AuthService) {}
}
