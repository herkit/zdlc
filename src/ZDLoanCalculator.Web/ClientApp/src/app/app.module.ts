import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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
  MatSliderModule
} from '@angular/material';

import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


const FRAMEWORK_MODULES = [
  FormsModule,
  ReactiveFormsModule,
  BrowserAnimationsModule
]

const MATERIAL_MODULES = [
  MatStepperModule,
  MatRadioModule,
  MatInputModule,
  MatButtonModule,
  MatSliderModule
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoanApplicationComponent
  ],
  imports: [
    ...FRAMEWORK_MODULES,
    ...MATERIAL_MODULES,
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'apply', component: LoanApplicationComponent }
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
