import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupFormComponent } from './components/group-form/group-form.component';
import { GroupViewComponent } from './components/group-view/group-view.component';

const routes: Routes = [
  { path: 'groups', component: GroupViewComponent },
  { path: 'group/create', component: GroupFormComponent },
  { path: 'group/update/:id', component: GroupFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GroupsRoutingModule { }
