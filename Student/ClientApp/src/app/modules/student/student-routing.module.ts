import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssignGroupFormComponent } from './components/assign-group-form/assign-group-form.component';
import { StudentFormComponent } from './components/student-form/student-form.component';
import { StudentViewComponent } from './components/student-view/student-view.component';

const routes: Routes = [
  { path: 'students', component: StudentViewComponent },
  { path: 'student/create', component: StudentFormComponent },
  { path: 'student/update/:id', component: StudentFormComponent },
  { path: 'student/assign-group/:id', component: AssignGroupFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class StudentRoutingModule { }
