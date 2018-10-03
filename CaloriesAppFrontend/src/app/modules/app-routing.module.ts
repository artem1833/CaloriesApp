import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from '../components/products/products.component';
import {UserInfoFormComponent} from '../components/user-info-form/user-info-form.component';
import {RegisterComponent} from '../components/register/register.component';
import {LoginComponent} from '../components/login/login.component';
import {AddProductFormComponent} from '../components/add-product-form/add-product-form.component';
import {AuthGuard} from '../services/auth-guard.service';
import {PageNotFoundComponent} from '../components/page-not-found/page-not-found.component';
import {LogoutComponent} from '../components/logout/logout.component';


const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: ProductsComponent, canActivate: [AuthGuard] },
  { path: 'user', component: UserInfoFormComponent, canActivate: [AuthGuard] },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'addproduct', component: AddProductFormComponent, canActivate: [AuthGuard] },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes, {useHash: true}) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
