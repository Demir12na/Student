import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentServiceService {
  
  private _url: string = '/api/students';
  constructor(private _http: HttpClient) { }
  
  getList() {
    return this._http.get<Array<any>>(this._url);
  }

  create(req: any) {
    return this._http.post<any>(this._url, req)
  }

  getUpdate(req: any) {
    return this._http.get<any>(this._url + '/get-update?id=' + req);
  }

  update(req: any) {
    return this._http.put(this._url, req)
  }

  delete(req: any) {
    return this._http.delete(this._url + '?id=' + req)
  }
  getDepartments() {
    return this._http.get<Array<any>>(this._url + "/departments");
  }
  
  assignGroup(req: any) {
    return this._http.put(this._url + "/assign-group", req)
  }
  getGroupByStudentId(req: any) {
    return this._http.get<any>(this._url + '/get-group?id=' + req);
  }
}