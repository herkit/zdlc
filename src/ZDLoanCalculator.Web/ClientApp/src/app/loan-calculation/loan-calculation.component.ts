import { Component, OnInit } from '@angular/core';
import { LoanCalculatorService } from '../services/loan-calculator.service';
import { ActivatedRoute } from '@angular/router';
import { Payment } from '../types';

@Component({
  selector: 'app-loan-calculation',
  templateUrl: './loan-calculation.component.html',
  styleUrls: ['./loan-calculation.component.scss']
})
export class LoanCalculationComponent implements OnInit {

  constructor(private loanCalculator: LoanCalculatorService, private route: ActivatedRoute) { }

  displayedColumns: string[] = ['periodNumber', 'interests', 'amountDue'];
  payments: Payment[];
  loading: boolean = false;

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.loading = true;
      this
        .loanCalculator
        .calculateLoan(params.loanType, params.paymentScheme, params.loanAmount, params.periods)
        .subscribe(
          payments => { 
            this.payments = payments; 
            this.loading = false; 
          }, 
          error => { 
            this.loading = false; 
          });
    });
  }

  getTotalAmount() {
    return this.payments.map(t => t.amountDue).reduce((acc, value) => acc + value, 0);
  }

  getTotalInterests() {
    return this.payments.map(t => t.interests).reduce((acc, value) => acc + value, 0);
  }

}
