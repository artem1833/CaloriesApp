import {Component, OnInit, ViewChild} from '@angular/core';
import { Product } from '../../models/product';
import { ProductUser } from '../../models/productUser';
import { ProductService } from '../../services/product.service';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import {UserService} from '../../services/user.service';
import {CaloriesRecommended} from '../../models/calories-recommended';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumnsProductsUser: string[] = ['name', 'count', 'calorie', 'protein', 'fat', 'carbohydrate', 'options'];
  displayedColumnsProducts: string[] = ['name', 'count', 'unitOfMeasure', 'calorie', 'protein', 'fat', 'carbohydrate', 'glycemicIndex'];
  displayedHeaderRecommendation: string[] = ['description', 'emptyOne', 'caloriesRecommended', 'proteinsRecommended',
    'fatsRecommended', 'carbohydratesRecommended', 'emptyTwo'];
  products = new MatTableDataSource<Product>();
  productsUser: ProductUser[];
  newProductUser: ProductUser = new ProductUser;
  sumProductsUser = new ProductUser();
  caloriesRecommended = new CaloriesRecommended();
  spinnerStatus = false;
  constructor(private productService: ProductService, private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
    this.getProducts();
    this.getProductsUser();
    if (this.authService.isLoggedIn()) {
      this.getUserCaloriesRecommended();
    }
  }

  getProducts(): void {
    this.spinnerStatus = true;
    this.productService.getProducts()
      .subscribe(products => {
        this.products = new MatTableDataSource<Product>(products.model);
        this.products.paginator = this.paginator;
        this.spinnerStatus = false;
      });
  }

  applyFilter(filterValue: string) {
    this.products.filter = filterValue.trim().toLowerCase();
  }

  getProductsUser(): void {
    this.spinnerStatus = true;
    this.productService.getProductsUser()
      .subscribe(productsUser => {
        this.productsUser = productsUser.model;
        this.getSumProductUser();
        this.spinnerStatus = false;
      });
  }

  getSumProductUser(): void {
    this.productService.getSumProductsUser()
      .subscribe(productsUser => {
        this.sumProductsUser = productsUser.model;
        this.spinnerStatus = false;
      });
  }

  add(product: Product[]): void {
    this.spinnerStatus = true;
    if (!product) { return; }
    for (let i = 0; i < product.length; i++) {
      this.newProductUser = new ProductUser;
      this.newProductUser.count = product[i].count;
      this.newProductUser.productId = product[i].id;
      this.productService.addProductUser(this.newProductUser).subscribe(prodUser => {
        this.productsUser.push(prodUser);
        this.getProductsUser();
        this.getSumProductUser();
        product[i].count = undefined;
      });
    }
    this.spinnerStatus = false;
  }

  filterProduct(products: Product[]) {
    return products.filter(item => item.count > 0);
  }

  getUserCaloriesRecommended() {
    this.spinnerStatus = true;
    this.userService.getUserCaloriesRecommended()
      .subscribe(calories => {
        if (calories.model) {
          this.caloriesRecommended = calories.model;
        }
        this.spinnerStatus = false;
      });
  }

  delete(productUser: ProductUser): void {
    this.spinnerStatus = true;
    if (!productUser) { return; }
    this.productService.deleteProductUser(productUser)
      .subscribe( data => {
        this.productsUser = this.productsUser.filter(h => h !== productUser);
        this.getSumProductUser();
        this.spinnerStatus = false;
    });
  }
}
