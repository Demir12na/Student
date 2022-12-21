import { Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime } from 'rxjs';
import { GroupService } from '../../services/group.service';

@Component({
  selector: 'app-group-view',
  templateUrl: './group-view.component.html',
  styleUrls: ['./group-view.component.css']
})
export class GroupViewComponent implements OnInit {

  public groups: any;

  public search: FormControl = new FormControl();
  public filterFormGroup: FormGroup;
  public formBuilder: FormBuilder;
  public status: FormControl = new FormControl();



  constructor(private router: Router, public injector: Injector, private groupApi: GroupService) {

    this.formBuilder = injector.get(FormBuilder);
    this.filterFormGroup = this.formBuilder.group({
      search: this.formBuilder.control(''),
      status: this.formBuilder.control('')
    });
  }

  ngOnInit(): void {
    this.getAllGroups();
  }

  getAllGroups() {
    this.groupApi.getList().subscribe({
      next: (data: any) => {
        this.groups = data;
      },
      error: (err: any) => {
        console.log(err);
      }
    });
  }

  delete(groupId: number) {
    if (confirm("Are you sure you want to delete this group?")) {
      this.groupApi.delete(groupId).subscribe({
        next: (data: any) => {
          this.getAllGroups();
        },
        error: (err: any) => {
          console.log(err);
        }
      });
    }
  }
}