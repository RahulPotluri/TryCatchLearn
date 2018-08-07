import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }

// using observables and RxJs operators
login(model: any) {
  console.log('Entered Auth SErvice');
  return this.http.post(this.baseUrl + 'login', model)
        .pipe(map((response: any) => {
          const user = response;
          localStorage.setItem('token', user.token);
        }) );
}

register(model: any) {
  return this.http.post(this.baseUrl + 'register', model);
}

}
