import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoanCalculatorService {

  constructor() { }
  
  calculateLoan(loanType: string, paymentScheme: string, loanAmount: number, periods: number) : Observable<any[]> {
    return of([
      { periodNumber: 1, amountDue: 1010, interests: 10 }
    ])
  }
}
