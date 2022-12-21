import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StudentViewComponent } from './modules/student/components/student-view/student-view.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StudentModule } from './modules/student/student.module';
import { GroupsModule } from './modules/groups/groups.module';
import { GroupViewComponent } from './modules/groups/components/group-view/group-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    StudentModule,
    GroupsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: StudentViewComponent, pathMatch: 'full' },
      { path: 'groups', component: GroupViewComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
