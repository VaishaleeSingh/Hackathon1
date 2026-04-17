import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { JobService, Job } from '../job.service';
import { ApplicationService } from '../../applications/application.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-job-detail',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, RouterLink],
  template: `
    <div *ngIf="job" class="detail-container">
      <mat-card>
        <mat-card-header>
          <mat-card-title>{{ job.title }}</mat-card-title>
          <mat-card-subtitle>{{ job.location }} | Salary: {{ job.salaryRange }}</mat-card-subtitle>
        </mat-card-header>
        <mat-card-content>
          <h3>Description</h3>
          <p>{{ job.description }}</p>
          <p><strong>Posted on:</strong> {{ job.createdAt | date }}</p>
          <p><strong>Posted by:</strong> {{ job.createdByUsername }}</p>
        </mat-card-content>
        <mat-card-actions>
          <button mat-raised-button color="primary" *ngIf="authService.hasRole(['Candidate'])" (click)="apply()">Apply for this Job</button>
          <button mat-button routerLink="/jobs">Back to List</button>
        </mat-card-actions>
      </mat-card>
    </div>
  `,
  styles: [`
    .detail-container { max-width: 800px; margin: 40px auto; }
    h3 { margin-top: 20px; }
  `]
})
export class JobDetailComponent implements OnInit {
  job?: Job;

  constructor(
    private route: ActivatedRoute,
    private jobService: JobService,
    private appService: ApplicationService,
    public authService: AuthService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.jobService.getJob(id).subscribe(job => this.job = job);
  }

  apply() {
    if (this.job) {
      this.appService.apply(this.job.id).subscribe({
        next: () => alert('Application submitted successfully!'),
        error: (err: any) => alert(err.error || 'Failed to apply')
      });
    }
  }
}
