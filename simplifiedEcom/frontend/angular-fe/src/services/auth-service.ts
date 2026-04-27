import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private ROOT_URL = 'http://localhost:5126/api/auth';
  private token_key = "auth_token";

  // user curent
  private currentUserSubject = new BehaviorSubject<string | null>(null);

  // observable pentru componente
  currentuser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {

    const token = this.getToken();

    if (token){
      const name = this.getUsernameFromToken(token);

      this.currentUserSubject.next(name);
    }
  }


  login(data: any): Observable<any>{
    return this.http.post(`${this.ROOT_URL}/login`, data);
  }

  logout(){
    localStorage.removeItem(this.token_key);

    this.currentUserSubject.next(null);
  }

  register(data: any): Observable<any> {
    return this.http.post(`${this.ROOT_URL}/register`, data);
  }

  saveToken(token: string){
    localStorage.setItem(this.token_key, token);

    const name = this.getUsernameFromToken(token);

    this.currentUserSubject.next(name);
  }

  getToken(): string | null{
    return localStorage.getItem(this.token_key);
  }

  private getUsernameFromToken(token: string): string{
    const payloadBase64 = token.split('.')[1];

    // atob = ascii to binary
    const decodedPayload = JSON.parse(atob(payloadBase64));

    return decodedPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  }
}
