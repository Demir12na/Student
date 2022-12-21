import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudentRoutingModule } from './student-routing.module';
import { StudentViewComponent } from './components/student-view/student-view.component';
import { StudentFormComponent } from './components/student-form/student-form.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AssignGroupFormComponent } from './components/assign-group-form/assign-group-form.component';


@NgModule({
  declarations: [
    StudentViewComponent,
    StudentFormComponent,
    AssignGroupFormComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    StudentRoutingModule
  ]
})
export class StudentModule { }
