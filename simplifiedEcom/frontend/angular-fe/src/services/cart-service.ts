import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartItem } from '../models/CartItem';

@Injectable({
  providedIn: 'root',
})
export class CartService {

  private ROOT_URL = 'http://localhost:5126/api/cart';

  constructor(private http: HttpClient){}

  // GET CART
  getCart(userId: number): Observable<CartItem[]>{
      return this.http.get<CartItem[]>(`${this.ROOT_URL}/${userId}`);
  }

  // ADD TO CART
  addToCart(userId: number, productId: number, quantity: number){
      return this.http.post(`${this.ROOT_URL}/add`, {
        userId,
        productId,
        quantity
      });
  }

  // UPDATE CART ITEM
  updateCartItem(userId: number, productId: number, quantity: number) {
    return this.http.put(`${this.ROOT_URL}/update`, {
      userId,
      productId,
      quantity
    });
  }

  // REMOVE CART ITEM
  removeCartItem(userId: number, productId: number) {
    return this.http.delete(`${this.ROOT_URL}/${userId}/${productId}`);
  }
}
