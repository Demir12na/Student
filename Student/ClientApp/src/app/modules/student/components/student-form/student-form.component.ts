import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { StudentServiceService } from '../../service/student-service.service';

@Component({
  selector: 'app-student-form',
  templateUrl: './student-form.component.html',
  styleUrls: ['./student-form.component.css']
})
export class StudentFormComponent implements OnInit {

  public newStudentFormGroup: FormGroup;
  public formBuilder: FormBuilder;

  public studentId: number = 0;
  public departments: any [] = [];
  private _id : string = "";

  public showBox: boolean = false;

  constructor(private router: Router, public injector: Injector, public route: ActivatedRoute, private studentApi: StudentServiceService,
    private _activatedRoute: ActivatedRoute) {

    this.formBuilder = injector.get(FormBuilder);

    this.newStudentFormGroup = this.formBuilder.group({
      id: this.formBuilder.control(0, [Validators.required]),
      firstName: this.formBuilder.control('', [Validators.required]),
      lastName: this.formBuilder.control('', [Validators.required]),
      emailAddress: this.formBuilder.control('', [Validators.required, Validators.email]),
      academicPerformance: this.formBuilder.control('', [Validators.required]),
      departments: this.formBuilder.control('', []),
      departmentName: this.formBuilder.control({value: '', disabled: true}, [])
    });
  }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._id = params['id'];
    });
    if (this._id !== "" && this._id != null && typeof this._id != "undefined") {
      this.studentId = +this._id;
      this.studentApi.getUpdate(this.studentId).subscribe({
        next: (data: any) => {
          this.newStudentFormGroup.setValue(
            {
              id: data.id,
              firstName: data.firstName,
              lastName: data.lastName,
              emailAddress: data.emailAddress,
              academicPerformance: data.academicPerformance,
              departments: [],
              departmentName: data.departmentName
            }
          );
        },
        error: (err: any) => {
          console.log(err);
        }
      });
    }
    else{
      this.studentApi.getDepartments().subscribe({
        next : (data: any) => {
          this.departments = data;
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
    if (this.studentId) {
      this.studentApi.update(this.newStudentFormGroup.value).subscribe({
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
      this.studentApi.create(this.newStudentFormGroup.value).subscribe({
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