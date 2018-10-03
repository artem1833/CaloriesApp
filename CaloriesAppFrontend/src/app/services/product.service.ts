import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../models/product';
import { ProductUser } from '../models/productUser';
import { Observable} from 'rxjs';
import {NewProductModel} from '../models/new-product-model';
import {BaseService} from './base.service';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {OperationDataResult} from '../models/operation.model';
import {Interpretation} from '../models/interpretation';
import {environment} from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const ProductMethods = {
  Products: environment.host + 'api/products',
  AddProduct: environment.host + 'api/add-product',
  ProductsUser: environment.host + 'api/productsUser',
  SumProductsUser: environment.host + 'api/sumProductsUser',
  AddProductUser: environment.host + 'api/add-productUser',
  DeleteProductUser: environment.host + 'api/delete-productUser',
  Interpretation: environment.host + 'api/interpretation'
};

@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseService {

  constructor(private _http: HttpClient, router: Router, toastrService: ToastrService) {
    super(_http, router, toastrService);
  }

  getProducts (): Observable<OperationDataResult<Product[]>> {
    return this.get<OperationDataResult<Product[]>>(ProductMethods.Products);
  }

  getProductsUser(): Observable<OperationDataResult<ProductUser[]>> {
    return this.get<OperationDataResult<ProductUser[]>>(ProductMethods.ProductsUser);
  }

  getSumProductsUser(): Observable<OperationDataResult<ProductUser>> {
    return this.get<OperationDataResult<ProductUser>>(ProductMethods.SumProductsUser);
  }

  addProduct(product: NewProductModel): Observable<NewProductModel> {
    return this.post<NewProductModel>(ProductMethods.AddProduct, product, httpOptions);
  }

  addProductUser (productUser: ProductUser): Observable<ProductUser> {
    return this.post<NewProductModel>(ProductMethods.AddProductUser, productUser, httpOptions);
  }

  deleteProductUser (productUser: ProductUser): Observable<ProductUser> {
    const id = productUser.id;
    const url = `${ProductMethods.DeleteProductUser}/${id}`;
    return this.delete<ProductUser>(url, httpOptions);
  }

  getInterpretation (type: number): Observable<Interpretation> {
    const url = `${ProductMethods.Interpretation}/${type}`;
    return this.get<Interpretation>(url);
  }
}
