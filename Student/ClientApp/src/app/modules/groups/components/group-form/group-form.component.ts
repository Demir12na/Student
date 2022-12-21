import { Component, Injector, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { GroupService } from '../../services/group.service';

@Component({
  selector: 'app-group-form',
  templateUrl: './group-form.component.html',
  styleUrls: ['./group-form.component.css']
})
export class GroupFormComponent implements OnInit {

  public newGroupFormGroup: FormGroup;
  public formBuilder: FormBuilder;

  public groupId: number = 0;
  public departments: any [] = [];
  private _id : string = "";

  public showBox: boolean = false;

  constructor(private router: Router, public injector: Injector, public route: ActivatedRoute, private groupApi: GroupService,
    private _activatedRoute: ActivatedRoute) {

    this.formBuilder = injector.get(FormBuilder);

    this.newGroupFormGroup = this.formBuilder.group({
      id: this.formBuilder.control(0, [Validators.required]),
      name: this.formBuilder.control('', [Validators.required]),
      departments: this.formBuilder.control('', [])
    });
  }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._id = params['id'];
    });
    this.groupApi.getDepartments().subscribe({
      next : (data: any) => {
        this.departments = data;
      },
      error: (err: any) => {
        console.log(err);
      }
    });
    if (this._id !== "" && this._id != null && typeof this._id != "undefined") {
      this.groupId = +this._id;
      this.groupApi.getUpdate(this.groupId).subscribe({
        next: (data: any) => {
          this.newGroupFormGroup.setValue(
            {
              id: data.id,
              name: data.name,
              departments: []
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
    if (this.groupId) {
      this.groupApi.update(this.newGroupFormGroup.value).subscribe({
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

    } else {
      this.groupApi.create(this.newGroupFormGroup.value).subscribe({
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
      });
    }
  }

}