import {Component, OnInit} from '@angular/core';
import { UserInfo } from '../../models/userInfo';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserService} from '../../services/user.service';
import {ToastrService} from 'ngx-toastr';
import {ProductService} from '../../services/product.service';
import {Interpretation} from '../../models/interpretation';

@Component({
  selector: 'app-user-info-form',
  templateUrl: './user-info-form.component.html',
  styleUrls: ['./user-info-form.component.css']
})
export class UserInfoFormComponent implements OnInit {

  genders: Interpretation;
  purposes: Interpretation;
  physicalActivities: Interpretation;
  user: UserInfo = new UserInfo;

  userForm = new FormGroup({
    height : new FormControl('', [Validators.required]),
    weight : new FormControl('', [Validators.required]),
    gender : new FormControl('', [Validators.required]),
    age : new FormControl('', [Validators.required]),
    physicalActivity : new FormControl('', [Validators.required]),
    purpose : new FormControl('', [Validators.required])
  });
  spinnerStatus = false;

  error: string;

  constructor(private userService: UserService, private productService: ProductService,
              private toastr: ToastrService) {
  }

  ngOnInit() {
    this.getInterpretation('gender');
    this.getInterpretation('purpose');
    this.getInterpretation('activity');
    this.getUserInfo();
  }


  getInterpretation(type: string) {
    this.spinnerStatus = true;
    return this.productService.getInterpretation(type)
      .subscribe(
        data  => {
          switch (type) {
            case 'gender':
              this.genders = data.model;
              break;
            case 'purpose':
              this.purposes = data.model;
              break;
            case 'activity':
              this.physicalActivities = data.model;
              break;
          }
          this.spinnerStatus = false;
        }
      );
  }

  getUserInfo() {
    this.spinnerStatus = true;
    this.userService.getUserInfo().
    subscribe(data => {
      if (!this.isEmptyObject(data)) {
        this.user = data;
        this.userForm.setValue({
          height: this.user.height,
          weight: this.user.weight,
          gender: this.user.gender,
          age: this.user.age,
          physicalActivity: this.user.physicalActivity,
          purpose: this.user.purpose
        });
      }
        this.spinnerStatus = false;
      },
      (error) => {
        this.error = error;
      }
    );

  }

  isEmptyObject(obj) {
    for (const prop in obj) {
      if (obj.hasOwnProperty(prop)) {
        return false;
      }
    }
    return true;
  }

  updateUserInfo() {
    this.spinnerStatus = true;
    console.log(this.userForm.value);
    this.userService.updateUserInfo(this.userForm.value).subscribe(data => {
      this.getUserInfo();
      this.spinnerStatus = false;
    });
  }

  get height() { return this.userForm.get('height'); }
  get weight() { return this.userForm.get('weight'); }
  get gender() { return this.userForm.get('gender'); }
  get age() { return this.userForm.get('age'); }
  get physicalActivity() { return this.userForm.get('physicalActivity'); }
  get purpose() { return this.userForm.get('purpose'); }

  getErrorMessageHeight() {
    return this.height.hasError('required') ? 'Введите рост' :
        '';
  }

  getErrorMessageWeight() {
    return this.weight.hasError('required') ? 'Введите вес' :
      '';
  }

  getErrorMessageGender() {
    return this.gender.hasError('required') ? 'Введите пол' :
      '';
  }

  getErrorMessageAge() {
    return this.age.hasError('required') ? 'Введите возраст' :
      '';
  }

  getErrorMessageActivity() {
    return this.physicalActivity.hasError('required') ? 'Введите физическую активность' :
      '';
  }

  getErrorMessagePurpose() {
    return this.purpose.hasError('required') ? 'Введите цель' :
      '';
  }
}
