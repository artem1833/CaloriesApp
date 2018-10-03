import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {comparePassword} from '../../helpers/compare-password.directive';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm = new FormGroup({
    email : new FormControl('', [Validators.required, Validators.email]),
    password : new FormControl('', [Validators.required, Validators.minLength(8)]),
    passwordConfirmation : new FormControl('', [Validators.required, comparePassword('password')])
});

  hide = true;
  hideConfirm = true;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  register() {
    return this.authService.register(this.registerForm.value)
      .subscribe(data => {
          if (this.authService.isLoggedIn()) {
            const redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/products';
            this.router.navigate([redirect]);
          }
        });
  }

  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get passwordConfirmation() { return this.registerForm.get('passwordConfirmation'); }

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

  getErrorMessagePasswordConfirm() {
    return this.passwordConfirmation.hasError('required') ? 'Подтвердите пароль' :
      this.passwordConfirmation.hasError('appCompare') ? 'Пароли не совпадают' :
        '';
  }

}
