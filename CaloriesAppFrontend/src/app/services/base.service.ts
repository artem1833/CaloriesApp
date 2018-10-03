import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

import {ToastrService} from 'ngx-toastr';
import {Observable, throwError} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

export class BaseService {
  constructor(private http: HttpClient, private router: Router, private toastrService: ToastrService) {
  }

  protected get<T>(method: string, params?: any): Observable<any> {
    return this.http.get(method)
      .pipe(
        map((response) => this.extractData<T>(response)),
        catchError(err => this.handleError(err))
      );
  }

  protected post<T>(method: string, body: any, httpOptions: any): Observable<any> {
    const model = JSON.stringify(body);
    return this.http.post(method, model, httpOptions)
      .pipe(
        map((response) => this.extractData<T>(response)),
        catchError(err => this.handleError(err))
      );
  }

  protected delete<T>(method: string, httpOptions: any): Observable<any> {
    return this.http.delete(method, httpOptions)
      .pipe(
        map((response) => this.extractData<T>(response)),
        catchError(err => this.handleError(err))
      );
  }

  private extractData<T>(res: any) {
    let body;
    try {
      body = res;
      if (body && body.success && body.message) {
        this.toastrService.success(body.message);
      } else if (body && !body.success && body.message) {
        this.toastrService.error(body.message);
      }
    } catch (e) {
      return {};
    }
    return <T>body || {};
  }


  private logOutWhenTokenFalse() {
    localStorage.clear();
    this.router.navigate(['/auth/login']).then();
  }


  private handleError(error: Response | any) {
    console.log('ERROR');

    let errorMessage = '';
    if (error.status === 400) {
      let errors;
      if (error._body) {
        errors = error._body;
      } else {
        errors = error.error;
      }
      if (errors && errors.length > 0) {
        errors.forEach(errorItem => {
          this.toastrService.error(errorItem.errorMessage, 'Error', {timeOut: 10000});
          console.error(errorItem);
        });
      }
      return throwError(error);
    }
    if (error.status === 401 || error.status === 403) {
      this.toastrService.warning('401(403) - Invalid token');
      console.error('401(403) - Invalid token');
      this.logOutWhenTokenFalse();
      console.error(error);
      return throwError(errorMessage);
    }
    const body = error._body || '';
    errorMessage = `${error.status} - ${error.statusText || ''} ${body}`;
    console.error(errorMessage);
    this.toastrService.error(errorMessage, 'Error', {timeOut: 10000});
    return throwError(errorMessage);
  }
}
