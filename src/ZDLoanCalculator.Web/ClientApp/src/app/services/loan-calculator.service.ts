import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Payment, LoanType, PaymentScheme } from '../types';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LoanCalculatorService {

  constructor(private httpClient: HttpClient) { }
  
  calculateLoan(loanType: string, paymentScheme: string, loanAmount: number, periods: number) : Observable<Payment[]> {
    return this.httpClient.get<Payment[]>("/api/v1/loan/plan", { 
      params: { 
        loanType: loanType, 
        paymentScheme: paymentScheme,
        loanAmount: loanAmount.toString(),
        periods: periods.toString()
      }})
  }

  getLoanTypes() : Observable<LoanType[]> {
    return this.httpClient.get<LoanType[]>("/api/v1/loan/types");
  }

  getPaymentSchemes() : Observable<PaymentScheme[]> {
    return this.httpClient.get<PaymentScheme[]>("/api/v1/loan/schemes");
  }
}
