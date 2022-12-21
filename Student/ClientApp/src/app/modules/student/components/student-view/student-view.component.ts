import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime } from 'rxjs';
import { StudentServiceService } from '../../service/student-service.service';

@Component({
  selector: 'app-student-view',
  templateUrl: './student-view.component.html',
  styleUrls: ['./student-view.component.css']
})
export class StudentViewComponent implements OnInit {

  public students: any;

  public currentPage: number = 1;
  public search: FormControl = new FormControl();
  public filterFormGroup: FormGroup;
  public formBuilder: FormBuilder;
  public status: FormControl = new FormControl();



  constructor(private router: Router, public injector: Injector, private studentApi: StudentServiceService) {

    this.formBuilder = injector.get(FormBuilder);
    this.filterFormGroup = this.formBuilder.group({
      search: this.formBuilder.control(''),
      status: this.formBuilder.control('')
    });

    this.filterFormGroup.valueChanges.pipe(debounceTime(1000)).subscribe((value: any) => {
      this.studentApi.getList().subscribe(
        (data: any) => {
          this.students = data;
        },
        (err: any) => {
          console.log(err);
        }
      );
    });
  }



  ngOnInit(): void {
    this.getAllStudents();
  }

  getAllStudents() {
    this.studentApi.getList().subscribe({
      next: (data: any) => {
        this.students = data;
      },
      error: (err: any) => {
        console.log(err);
      }
    });
  }

  delete(studentId: number) {
    if (confirm("Are you sure you want to delete this student?")) {
      this.studentApi.delete(studentId).subscribe({
        next: (data: any) => {
          this.getAllStudents();
        },
        error: (err: any) => {
          console.log(err);
        }
      });
    }
  }
}