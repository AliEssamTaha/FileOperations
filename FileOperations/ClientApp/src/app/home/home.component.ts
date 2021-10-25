import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public data: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
    this.getData();
  }

  getData() {
    this.http.get(this.baseUrl + 'file').subscribe(result => {
      this.data = result;
    }, error => console.error(error));
  }

  delete(id) {
    if (confirm('Are you sure you want to delete this file ? ')) {
      this.http.delete(this.baseUrl + 'file/' + id).subscribe(res => {
        this.getData();
        alert('Deleted Successfully.');
      });
    }
  }
}

