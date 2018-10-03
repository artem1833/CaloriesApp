import {AfterViewInit, Component, ElementRef, ViewChild} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

interface Links {
  name: string;
  path: string;
}

const LINKS_NOT_AUTH: Links[] = [
  {name: 'Вход', path: 'login'}
];

const LINKS_AUTH: Links[] = [
  {name: 'Калькулятор', path: 'products'},
  {name: 'Данные пользователя', path: 'user'},
  {name: 'Добавить продукт', path: 'addproduct'},
  {name: 'Выйти', path: 'logout'}
];

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent {
  @ViewChild('navigationMenu') menuElement: ElementRef;
  linksNotAuth = LINKS_NOT_AUTH;
  linksAuth = LINKS_AUTH;

  constructor(public authService: AuthService) {
  }
}
