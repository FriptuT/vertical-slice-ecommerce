import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CheckoutRequest, OrderResponse } from '../models/checkout';


@Injectable({
  providedIn: 'root',
})
export class CheckoutService {

  private ROOT_URL = 'http://localhost:5126/api/checkout';

  constructor(private http: HttpClient){}

  placeOrder(checkoutRequest: CheckoutRequest){
    return this.http.post<OrderResponse>(`${this.ROOT_URL}`, checkoutRequest);
  }

  
}
