import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { from, of } from 'rxjs';
import { delay } from 'rxjs/internal/operators';
import { concatMap } from 'rxjs/internal/operators';

import { Payment, LoanType, PaymentScheme } from '../types';

const sampleLoanTypes: LoanType[] = [
  { key: "house", description: "House loan", interestRate: 0.035 },
  { key: "car", description: "Car loan", interestRate: 0.055 },
];

const samplePaymentSchemes: PaymentScheme[] = [
  { key: "series", value: "Series downpayments" }
];


@Injectable({
  providedIn: 'root'
})
export class LoanCalculatorService {

  constructor() { }
  
  calculateLoan(loanType: string, paymentScheme: string, loanAmount: number, periods: number) : Observable<Payment[]> {
    return of([{ periodNumber: 1, amountDue: 1010, interests: 10 }])
      .pipe(delay(1000));
  }

  getLoanTypes() : Observable<LoanType[]> {
    return of(sampleLoanTypes)
      .pipe(delay(1000));
  }

  getPaymentSchemes() : Observable<PaymentScheme[]> {
    return of([
      { key: "series", value: "Series downpayments" }
    ]).pipe(delay(1000));  
  }
}
