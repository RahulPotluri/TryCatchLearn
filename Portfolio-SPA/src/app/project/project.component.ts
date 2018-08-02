import { Component, OnInit } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  projects: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getProjects();
  }

  getProjects()
  {
    this.http.get('http://localhost:5000/api/project').subscribe(response => {
      this.projects = response;
    }, error => {
      console.log(error);
    });
  }

}
