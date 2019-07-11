import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoanApplicationComponent } from './loan-application/loan-application.component';
import { LoanCalculationComponent } from './loan-calculation/loan-calculation.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'start', component: LoanApplicationComponent },
  { path: 'calculation', component: LoanCalculationComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
