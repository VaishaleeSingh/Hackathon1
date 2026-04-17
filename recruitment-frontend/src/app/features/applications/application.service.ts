import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

export interface Application {
  id: number;
  applicationDate: string;
  status: string;
  jobId: number;
  jobTitle: string;
  candidateId: number;
  candidateName: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private apiUrl = environment.apiUrl + '/applications';

  constructor(private http: HttpClient) {}

  getMyApplications(): Observable<Application[]> {
    return this.http.get<Application[]>(`${this.apiUrl}/my-applications`);
  }

  getJobApplications(jobId: number): Observable<Application[]> {
    return this.http.get<Application[]>(`${this.apiUrl}/job/${jobId}`);
  }

  apply(jobId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/apply/${jobId}`, {});
  }

  updateStatus(id: number, status: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/status`, { status });
  }
}
