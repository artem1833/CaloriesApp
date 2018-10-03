import { Directive, Input } from '@angular/core';
import { Validator, AbstractControl, ValidationErrors, NG_VALIDATORS, ValidatorFn } from '@angular/forms';
import { Subscription } from 'rxjs';

export function comparePassword(controlNameToCompare: string): ValidatorFn {
  return (c: AbstractControl): ValidationErrors | null => {
    if (c.value === null || c.value.length === 0) {
      return null; // don't validate empty value
    }
    const controlToCompare = c.root.get(controlNameToCompare);
    if (controlToCompare) {
      const subscription: Subscription = controlToCompare.valueChanges.subscribe(() => {
        c.updateValueAndValidity();
        subscription.unsubscribe();
      });
    }
    return controlToCompare && controlToCompare.value !== c.value ? { 'appCompare': true } : null;
  };
}

@Directive({
  selector: '[appCompare]',
  providers: [{ provide: NG_VALIDATORS, useExisting: ComparePasswordDirective, multi: true }]
})
export class ComparePasswordDirective implements Validator {
  @Input('appCompare') controlNameToCompare: string;

  validate(c: AbstractControl): ValidationErrors | null {
    return comparePassword(this.controlNameToCompare)(c);
  }

}

