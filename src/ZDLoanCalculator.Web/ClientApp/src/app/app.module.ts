import { BrowserModule } from '@angular/platform-browser';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgModule } from '@angular/core';
import { HttpClientModule }    from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { LoanApplicationComponent } from './loan-application/loan-application.component';

import {
  MatStepperModule,
  MatRadioModule,
  MatInputModule,
  MatButtonModule,
  MatSliderModule,
  MatTableModule,
  MatCardModule,
  MatSidenavModule,
  MatProgressSpinnerModule
} from '@angular/material';

import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoanCalculationComponent } from './loan-calculation/loan-calculation.component';


const FRAMEWORK_MODULES = [
  ReactiveFormsModule,
  BrowserAnimationsModule,
  FlexLayoutModule,
  HttpClientModule
]

const MATERIAL_MODULES = [
  MatStepperModule,
  MatRadioModule,
  MatInputModule,
  MatButtonModule,
  MatSliderModule,
  MatTableModule,
  MatCardModule,
  MatSidenavModule,
  MatProgressSpinnerModule
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoanApplicationComponent,
    LoanCalculationComponent
  ],
  imports: [
    ...FRAMEWORK_MODULES,
    ...MATERIAL_MODULES,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
