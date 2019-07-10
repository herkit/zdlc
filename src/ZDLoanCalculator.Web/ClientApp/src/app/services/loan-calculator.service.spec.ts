import { TestBed } from '@angular/core/testing';

import { LoanCalculatorService } from './loan-calculator.service';

describe('LoanCalculatorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LoanCalculatorService = TestBed.get(LoanCalculatorService);
    expect(service).toBeTruthy();
  });
});
