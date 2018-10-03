import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {User} from '../models/user';
import { map } from 'rxjs/operators';
import {ProductUser} from '../models/productUser';
import {UserInfo} from '../models/userInfo';
import {CaloriesRecommended} from '../models/calories-recommended';
import {OperationDataResult} from '../models/operation.model';
import {BaseService} from './base.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Product} from '../models/product';
import {environment} from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const UserMethods = {
  UserInfo: environment.host + 'api/account/user-info',
  UserCaloriesRecommended: environment.host + 'api/account/user-calories-recommended'
};

@Injectable({
  providedIn: 'root'
})

export class UserService extends BaseService {
  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }
  getUserInfo(): Observable<UserInfo> {
    return this.get<UserInfo>(UserMethods.UserInfo);
  }

  updateUserInfo(userInfo: UserInfo): Observable<UserInfo> {
    return this.post<UserInfo>(UserMethods.UserInfo, userInfo, httpOptions);
  }

  getUserCaloriesRecommended(): Observable<OperationDataResult<CaloriesRecommended>> {
    return this.get<OperationDataResult<CaloriesRecommended>>(UserMethods.UserCaloriesRecommended);
  }

  getAllProducts (): Observable<OperationDataResult<Product[]>> {
    return this.get<OperationDataResult<Product[]>>('api/all-products');
  }
}
