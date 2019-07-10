import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loan-application',
  templateUrl: './loan-application.component.html',
  styleUrls: ['./loan-application.component.scss']
})
export class LoanApplicationComponent implements OnInit {

  constructor() { }

  loanTypes: any[] = [
    { key: "house", description: "House loan", interestRate: 0.035 },
    { key: "car", description: "Car loan", interestRate: 0.035 },
  ];

  paymentSchemes: any[] = [
    { key: "series", value: "Series loan" },
    { key: "annuity", value: "Annuity loan" },
  ]

  ngOnInit() {
  }

}
