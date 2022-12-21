import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { StudentServiceService } from '../../service/student-service.service';

@Component({
  selector: 'app-assign-group-form',
  templateUrl: './assign-group-form.component.html',
  styleUrls: ['./assign-group-form.component.css']
})
export class AssignGroupFormComponent implements OnInit {

  public assignGroupFormGroup: FormGroup;
  public formBuilder: FormBuilder;

  private _id: string = "";
  private studentId: number = 0;
  public groups: any [] = [];

  public showBox: boolean = false;

  constructor(private router: Router, public injector: Injector, public route: ActivatedRoute, private studentApi: StudentServiceService,
    private _activatedRoute: ActivatedRoute) {

    this.formBuilder = injector.get(FormBuilder);

    this.assignGroupFormGroup = this.formBuilder.group({
      groups: this.formBuilder.control('', []),
      groupName: this.formBuilder.control('', []),
      studentId: this.formBuilder.control('', []),
      id: this.formBuilder.control('', []),
      groupId: this.formBuilder.control('', [])
    });
  }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._id = params['id'];
    });
    if (this._id !== "" && this._id != null && typeof this._id != "undefined") {
      this.studentId = +this._id;
      this.studentApi.getGroupByStudentId(this.studentId).subscribe({
        next: (data: any) => {
          this.groups = data.groups;
          this.assignGroupFormGroup.setValue(
            {
              id: data.id,
              groupName: data.groupName,
              groups: data.groups,
              studentId: data.id,
              groupId: null
            }
          );
        },
        error: (err: any) => {
          console.log(err);
        }
      });
    }
  }

  returnBack() {
    this.router.navigate(['']);
  }

  onSubmit() {
    var req : any = {
      id: this.assignGroupFormGroup.value.id,
      groupId: this.assignGroupFormGroup.value.groups.id
    }
    this.studentApi.assignGroup(req).subscribe({
      next: (data: any) => {
        this.showBox = true;
        setTimeout(() => {
          this.showBox = false;
        }, 2000);

        setTimeout(() => {
          this.returnBack();
        }, 2000);
      },
      error: (err: any) => {
        console.log(err);
      }
    }

    );
  }

}