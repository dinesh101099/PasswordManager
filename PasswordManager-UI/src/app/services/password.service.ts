import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Password } from '../models/password.model';

@Injectable({
  providedIn: 'root',
})
export class PasswordService {
  private apiUrl = 'http://localhost:5066/api/Passwords';  // Adjust the API URL

  constructor(private http: HttpClient) {}

  getPasswords(decrypt: boolean): Observable<Password[]> {
    return this.http.get<Password[]>(`${this.apiUrl}/?decrypt=${decrypt}`).pipe(
      catchError(error => {
        console.error('Error fetching passwords:', error);
        return throwError(() => error);
      })
    );
  }

  getPasswordById(id: number, decrypt: boolean): Observable<Password> {
    return this.http.get<Password>(`${this.apiUrl}/${id}/?decrypt=${decrypt}`).pipe(
      catchError(error => {
        console.error(`Error fetching password with ID ${id}:`, error);
        return throwError(() => error);
      })
    );
  }

  getPasswordByUserName(id: string, decrypt: boolean): Observable<Password> {
    debugger;
    return this.http.get<Password>(`${this.apiUrl}/by-username/${id}/?decrypt=${decrypt}`).pipe(
      catchError(error => {
        console.error(`Error fetching password with UserName ${id}:`, error);
        return throwError(() => error);
      })
    );
  }

  addPassword(password: any): Observable<Password> {
    return this.http.post<Password>(this.apiUrl, password).pipe(
      catchError(error => {
        console.error('Error adding password:', error);
        return throwError(() => error);
      })
    );
  }

  updatePassword(id: number, password: any): Observable<Password> {
    return this.http.put<Password>(`${this.apiUrl}/${id}`, password).pipe(
      catchError(error => {
        console.error(`Error updating password with ID ${id}:`, error);
        return throwError(() => error);
      })
    );
  }

  deletePassword(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(error => {
        console.error(`Error deleting password with ID ${id}:`, error);
        return throwError(() => error);
      })
    );
  }
}
