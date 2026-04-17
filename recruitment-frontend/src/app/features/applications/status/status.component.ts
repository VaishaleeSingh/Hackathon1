import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { ApplicationService, Application } from '../application.service';

@Component({
  selector: 'app-status',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatChipsModule],
  template: `
    <h1>My Job Applications</h1>
    <table mat-table [dataSource]="applications" class="mat-elevation-z8">
      <ng-container matColumnDef="jobTitle">
        <th mat-header-cell *ngAtTh> Job Title </th>
        <td mat-cell *matCellDef="let app"> {{app.jobTitle}} </td>
      </ng-container>

      <ng-container matColumnDef="date">
        <th mat-header-cell *atTh> Applied Date </th>
        <td mat-cell *matCellDef="let app"> {{app.applicationDate | date}} </td>
      </ng-container>

      <ng-container matColumnDef="status">
        <th mat-header-cell *atTh> Status </th>
        <td mat-cell *matCellDef="let app"> 
          <mat-chip [color]="getStatusColor(app.status)" highlighted>{{app.status}}</mat-chip>
        </td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
    </table>
  `,
  styles: [`
    table { width: 100%; margin-top: 20px; }
  `]
})
export class StatusComponent implements OnInit {
  applications: Application[] = [];
  displayedColumns: string[] = ['jobTitle', 'date', 'status'];

  constructor(private appService: ApplicationService) {}

  ngOnInit() {
    this.appService.getMyApplications().subscribe(apps => this.applications = apps);
  }

  getStatusColor(status: string) {
    switch (status) {
      case 'Selected': return 'primary';
      case 'Rejected': return 'warn';
      case 'Interview': return 'accent';
      default: return '';
    }
  }
}
