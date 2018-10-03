import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email : new FormControl('', [Validators.required, Validators.email]),
    password : new FormControl('', [Validators.required, Validators.minLength(8)]),
  });

  hide = true;
  constructor(private authService: AuthService, private router: Router) { }
  ngOnInit() {
  }

  onSubmit() {
    this.authService.login(this.loginForm.value)
      .subscribe(res => {
          if (this.authService.isLoggedIn()) {
            const redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/products';
            this.router.navigate([redirect]);
          }
        });
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

  getErrorMessageEmail() {
    return this.email.hasError('required') ? 'Введите email' :
      this.email.hasError('email') ? 'Невалидный email' :
        '';
  }

  getErrorMessagePassword() {
    return this.password.hasError('required') ? 'Введите пароль' :
      this.password.hasError('minlength') ? 'Пароль должен содержать не менее 8 символов' :
        '';
  }
}
