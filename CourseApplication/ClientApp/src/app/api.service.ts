import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { Course } from './course';
import { CourseDate } from './coursedate';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = 'https://localhost:44346/api';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);

      return of(result as T);

    };
  }

  getCourses(): Observable<Course[]> {
    const url = `${apiUrl}/course/`;
    return this.http.get<Course[]>(url)
      .pipe(
        tap(cases => console.log('fetched courses')),
        catchError(this.handleError('getCourses', []))
      );
  }

  getCourseDates(id: number): Observable<CourseDate[]> {
    const url = `${apiUrl}/course/dates/${id}`;
    return this.http.get<CourseDate[]>(url).pipe(
      tap(_ => console.log(`fetched course dates courseid=${id}`)),
      catchError(this.handleError<CourseDate[]>('getCourseDates', []))
    )
  }

  submitApplication(application): Observable<any> {
    const url = `${apiUrl}/courseapplication`;
    return this.http.post(url, JSON.stringify(application), httpOptions).pipe(
      tap((res: any) => {
        console.log(`added application with id=${res.resource}`);
        return res;
      }),
      catchError(err => {
        console.log(err);
        if (err.status === 400) {
          alert("Bad request");
        }
        return Observable.throw(err);
      })
    );
  }

}
