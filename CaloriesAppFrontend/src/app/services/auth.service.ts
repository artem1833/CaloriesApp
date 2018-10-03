import { Inject, Injectable } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {TokenResponse} from '../models/token-response';
import {catchError, combineAll, map} from 'rxjs/operators';
import {User} from '../models/user';
import {BaseService} from './base.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {OperationDataResult} from '../models/operation.model';
import {UserInfo} from '../models/userInfo';
import {environment} from '../../environments/environment';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const AuthMethods = {
  Login: environment.host + 'api/account/login',
  Register: environment.host + 'api/account/register'
};
@Injectable()
export class AuthService extends BaseService {
  authKey = 'auth';
  redirectUrl: string;

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  login(user: User): Observable<boolean> {
    const url = AuthMethods.Login;
    const data = {
      email: user.email,
      password: user.password
    };

    return this.post<OperationDataResult<TokenResponse>>(url, data, httpOptions).pipe(
      map((res) => {
        const token = res.model && res.model.token;
        if (token) {
          this.setAuth(res.model);
        }
        return res.message;
      }), catchError(error => {
        console.log(error);
        return new Observable<any>(error);
      })
    );
  }

  register(user: User): Observable<OperationDataResult<TokenResponse>> {
    return this.post<OperationDataResult<TokenResponse>>(AuthMethods.Register, user, httpOptions).pipe(
      map((res) => {
        const token = res.model && res.model.token;
        if (token) {
          this.setAuth(res.model);
        }
        return res.message;
      }), catchError(error => {
        console.log('error register');
        return new Observable<any>(error);
      })
    );
  }

  logout(): boolean {
    this.setAuth(null);
    return true;
  }

  setAuth(auth: TokenResponse | null): boolean {
    if (auth) {
      localStorage.setItem(
        this.authKey,
        JSON.stringify(auth));
    } else {
      localStorage.removeItem(this.authKey);
    }
    return true;
  }

  getAuth(): TokenResponse | null {
    const i = localStorage.getItem(this.authKey);
    if (i) {
      return JSON.parse(i);
    }
    return null;
  }

  isLoggedIn(): boolean {
    return localStorage.getItem(this.authKey) != null;
  }
}
