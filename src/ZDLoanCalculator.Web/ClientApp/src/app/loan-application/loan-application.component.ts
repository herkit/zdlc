import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loan-application',
  templateUrl: './loan-application.component.html',
  styleUrls: ['./loan-application.component.scss']
})
export class LoanApplicationComponent implements OnInit {
  public applicationForm: FormGroup;

  constructor(private router: Router) { }

  loanTypes: any[] = [
    { key: "house", description: "House loan", interestRate: 0.035 },
    { key: "car", description: "Car loan", interestRate: 0.035 },
  ];

  paymentSchemes: any[] = [
    { key: "series", value: "Series loan" },
    { key: "annuity", value: "Annuity loan" },
  ]

  ngOnInit() {
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

    console.log(this.applicationForm.value);
    if (!this.applicationForm.invalid) {
      this.router.navigate(["/calculation"], { queryParams: this.applicationForm.value });
    }
  }

}
