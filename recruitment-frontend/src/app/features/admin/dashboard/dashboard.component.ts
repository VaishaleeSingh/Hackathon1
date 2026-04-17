import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { JobService } from '../../jobs/job.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatGridListModule, MatCardModule, MatIconModule],
  template: `
    <h1>Admin Dashboard</h1>
    <mat-grid-list cols="3" rowHeight="200px" gutterSize="20px">
      <mat-grid-tile>
        <mat-card class="stat-card">
          <mat-card-header>
            <mat-icon color="primary">work</mat-icon>
            <mat-card-title>Total Jobs</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <span class="stat-value">{{ totalJobs }}</span>
          </mat-card-content>
        </mat-card>
      </mat-grid-tile>
      
      <mat-grid-tile>
        <mat-card class="stat-card">
          <mat-card-header>
            <mat-icon color="accent">people</mat-icon>
            <mat-card-title>Candidates</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <span class="stat-value">12</span>
          </mat-card-content>
        </mat-card>
      </mat-grid-tile>

      <mat-grid-tile>
        <mat-card class="stat-card">
          <mat-card-header>
            <mat-icon color="warn">description</mat-icon>
            <mat-card-title>Applications</mat-card-title>
          </mat-card-header>
          <mat-card-content>
            <span class="stat-value">45</span>
          </mat-card-content>
        </mat-card>
      </mat-grid-tile>
    </mat-grid-list>
  `,
  styles: [`
    .stat-card { width: 100%; height: 100%; display: flex; flex-direction: column; align-items: center; justify-content: center; }
    .stat-value { font-size: 3rem; font-weight: bold; margin-top: 10px; }
    mat-icon { font-size: 2.5rem; width: 40px; height: 40px; margin-right: 10px; }
  `]
})
export class DashboardComponent implements OnInit {
  totalJobs = 0;

  constructor(private jobService: JobService) {}

  ngOnInit() {
    this.jobService.getJobs().subscribe(jobs => this.totalJobs = jobs.length);
  }
}
