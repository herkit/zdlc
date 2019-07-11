import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoanType, PaymentScheme } from '../types';
import { LoanCalculatorService } from '../services/loan-calculator.service';

@Component({
  selector: 'app-loan-application',
  templateUrl: './loan-application.component.html',
  styleUrls: ['./loan-application.component.scss']
})
export class LoanApplicationComponent implements OnInit {
  public applicationForm: FormGroup;
  public loanTypes: LoanType[];
  public paymentSchemes: PaymentScheme[];

  constructor(private router: Router, private loanCalculatorService: LoanCalculatorService) { }

  ngOnInit() {
    this.loanCalculatorService.getLoanTypes().subscribe(loanTypes => this.loanTypes = loanTypes);
    this.loanCalculatorService.getPaymentSchemes().subscribe(paymentSchemes => this.paymentSchemes = paymentSchemes);

    this.applicationForm = new FormGroup({
      loanType: new FormControl('', [Validators.required]),
      paymentScheme: new FormControl('', [Validators.required]),
      loanAmount: new FormControl('', [Validators.required, Validators.max(100000000), Validators.min(1000)]),
      periods: new FormControl('', [Validators.required, Validators.min(1), Validators.max(360)])
    })
  }

  hasError(controlName: string, errorName: string) {
    return this.applicationForm.controls[controlName].hasError(errorName) && this.applicationForm.controls[controlName].touched;
  }

  calculate() {
    if (!this.applicationForm.invalid) {
      this.router.navigate(["/calculation"], { queryParams: this.applicationForm.value });
    }
  }

  isReady() {
    if (this.loanTypes && this.paymentSchemes)
      return true;
    return false;
  }

}
