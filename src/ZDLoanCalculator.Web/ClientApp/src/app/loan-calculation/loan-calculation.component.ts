import { Component, OnInit } from '@angular/core';
import { LoanCalculatorService } from '../services/loan-calculator.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-loan-calculation',
  templateUrl: './loan-calculation.component.html',
  styleUrls: ['./loan-calculation.component.scss']
})
export class LoanCalculationComponent implements OnInit {

  constructor(private loanCalculator: LoanCalculatorService, private route: ActivatedRoute) { }

  displayedColumns: string[] = ['periodNumber', 'amountDue', 'interests'];
  payments: any[];

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      console.log(params);
      this
        .loanCalculator
        .calculateLoan(params.loanType, params.paymentScheme, params.loanAmount, params.periods)
        .subscribe(payments => this.payments = payments);
    });
  }

}
