import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { JobService, Job } from '../job.service';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-job-list',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule, RouterLink],
  template: `
    <div class="header">
      <h1>Available Jobs</h1>
      <button mat-raised-button color="primary" *ngIf="authService.hasRole(['Admin', 'Recruiter'])" routerLink="/jobs/create">
        <mat-icon>add</mat-icon> Post New Job
      </button>
    </div>

    <div class="job-grid">
      <mat-card *ngFor="let job of jobs" class="job-card">
        <mat-card-header>
          <mat-card-title>{{ job.title }}</mat-card-title>
          <mat-card-subtitle>{{ job.location }} | Salary: {{ job.salaryRange }}</mat-card-subtitle>
        </mat-card-header>
        <mat-card-content>
          <p>{{ job.description | slice:0:150 }}{{ job.description.length > 150 ? '...' : '' }}</p>
          <div class="meta">
            <span>Posted by: {{ job.createdByUsername }}</span>
            <span>{{ job.createdAt | date:'mediumDate' }}</span>
          </div>
        </mat-card-content>
        <mat-card-actions>
          <button mat-button color="primary" [routerLink]="['/jobs', job.id]">View Details</button>
          <button mat-button color="accent" *ngIf="authService.hasRole(['Candidate'])" (click)="apply(job.id)">Apply Now</button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .header { display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px; }
    .job-grid { display: grid; grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); gap: 20px; }
    .job-card { display: flex; flex-direction: column; justify-content: space-between; }
    .meta { display: flex; justify-content: space-between; font-size: 0.8rem; color: #666; margin-top: 10px; }
  `]
})
export class JobListComponent implements OnInit {
  jobs: Job[] = [];

  constructor(private jobService: JobService, public authService: AuthService) {}

  ngOnInit() {
    this.jobService.getJobs().subscribe(jobs => this.jobs = jobs);
  }

  apply(jobId: number) {
    // Application logic will be in ApplicationService
    alert('Apply feature coming soon!');
  }
}
