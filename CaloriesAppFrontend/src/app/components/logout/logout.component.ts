import {AfterViewChecked, Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-logout',
  template: ''
})
export class LogoutComponent implements OnInit, AfterViewChecked {

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  signOut() {
    this.authService.logout()
    this.router.navigate(['']);
  }

  ngAfterViewChecked(): void {
    this.signOut();
  }
}
