import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ProductService} from '../../services/product.service';
import {ToastrService} from 'ngx-toastr';
import {Interpretation} from '../../models/interpretation';

@Component({
  selector: 'app-add-product-form',
  templateUrl: './add-product-form.component.html',
  styleUrls: ['./add-product-form.component.css']
})
export class AddProductFormComponent implements OnInit {
  productForm = new FormGroup({
    name : new FormControl('', [Validators.required]),
    unitOfMeasure : new FormControl('', [Validators.required]),
    calorie : new FormControl('', [Validators.required]),
    protein : new FormControl('', [Validators.required]),
    fat : new FormControl('', [Validators.required]),
    carbohydrate : new FormControl('', [Validators.required]),
    glycemicIndex : new FormControl(''),
    weight: new FormControl(100)
  });
  unitsOfMeasure: Interpretation;
  spinnerStatus = false;

  constructor(private productService: ProductService, private toastr: ToastrService) { }

  ngOnInit() {
    this.getInterpretation();
  }

  getInterpretation() {
    this.spinnerStatus = true;
    return this.productService.getInterpretation('unitOfMeasure')
      .subscribe(
        data  => {
          this.unitsOfMeasure = data.model;
          this.spinnerStatus = false;
        }
      );
  }

  addProduct() {
    this.spinnerStatus = true;
    return this.productService.addProduct(this.productForm.value)
      .subscribe(
        data  => {
          this.spinnerStatus = false;
        }
      );
  }

  get name() { return this.productForm.get('name'); }
  get unitOfMeasure() { return this.productForm.get('unitOfMeasure'); }
  get calorie() { return this.productForm.get('calorie'); }
  get protein() { return this.productForm.get('protein'); }
  get fat() { return this.productForm.get('fat'); }
  get carbohydrate() { return this.productForm.get('carbohydrate'); }
  get glycemicIndex() { return this.productForm.get('glycemicIndex'); }
  get weight() { return this.productForm.get('weight'); }

  getErrorMessageName() {
    return this.name.hasError('required') ? 'Введите название продукта' :
      '';
  }

  getErrorMessageUnitOfMeasure() {
    return this.unitOfMeasure.hasError('required') ? 'Выберите единицу измерения' :
      '';
  }

  getErrorMessageWeight() {
    return this.weight.hasError('required') ? 'Введите вес' :
      '';
  }

  getErrorMessageCalorie() {
    return this.calorie.hasError('required') ? 'Введите количество калорий' :
      '';
  }

  getErrorMessageProtein() {
    return this.protein.hasError('required') ? 'Введите количество белков' :
      '';
  }

  getErrorMessageFat() {
    return this.fat.hasError('required') ? 'Введите количество жиров' :
      '';
  }

  getErrorMessageCarbohydrate() {
    return this.carbohydrate.hasError('required') ? 'Введите количество углеводов' :
      '';
  }

  getErrorMessageGlycemicIndex() {
    return this.glycemicIndex.hasError('min') ? 'Гликемический индекс не может быть отрицательным' :
      '';
  }
}
