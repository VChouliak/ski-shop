import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit{
  baseUrl = environment.apiUrl;
  validationErrors: any;
  
  constructor(private http: HttpClient){}

  ngOnInit(): void {
    
  }

  getNotFoundError(){
    this.http.get(this.baseUrl + 'products/42').subscribe({
      next: response => {console.log(response)},
      error: response=> {console.error(response)}      
    });
  }

  getInternalServerError(){
    this.http.get(this.baseUrl + 'buggy/servererror').subscribe({
      next: response => {console.log(response)},
      error: response=> {console.error(response)}      
    });
  }

  getBadRequestError(){
    this.http.get(this.baseUrl + 'buggy/badrequest').subscribe({
      next: response => {console.log(response)},
      error: response=> {console.error(response) }     
    });
  }

  getValidationError(){
    this.http.get(this.baseUrl + 'products/fortytwo').subscribe({
      next: response => {console.log(response)},
      error: response=> {
        console.error(response)
        this.validationErrors = response.errors;
      }      
    });
  }

}
