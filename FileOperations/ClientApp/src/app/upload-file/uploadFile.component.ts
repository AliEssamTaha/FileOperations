import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  templateUrl: './uploadFile.component.html',
})
export class UploadFileComponent {

  title = 'dropzone';

  files: File[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router : Router) { }

  onSelect(event) {
    console.log(event);
    this.files.push(...event.addedFiles);
  }

  save() {
    const formData = new FormData();

    for (var i = 0; i < this.files.length; i++) {
      formData.append("file[]", this.files[i]);
    }

    this.http.post(this.baseUrl + 'file', formData).subscribe(res => {

      this.files = [];
      console.log(res);
      alert('Uploaded Successfully.');
      this.router.navigate(['']); 
    });
  }
}

