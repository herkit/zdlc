import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanCalculationComponent } from './loan-calculation.component';

describe('LoanCalculationComponent', () => {
  let component: LoanCalculationComponent;
  let fixture: ComponentFixture<LoanCalculationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoanCalculationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoanCalculationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
