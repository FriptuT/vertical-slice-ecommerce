import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { CartItem } from '../models/CartItem';

@Injectable({
  providedIn: 'root',
})
export class CartService {

  private ROOT_URL = 'http://localhost:5126/api/cart';

  private cartItems =  new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartItems.asObservable();

  constructor(private http: HttpClient){}

  // GET CART
  getCart(userId: number): Observable<CartItem[]>{
      return this.http.get<CartItem[]>(`${this.ROOT_URL}/${userId}`)
      .pipe(tap(items => this.cartItems.next(items)));
  }

  // ADD TO CART
  addToCart(userId: number, productId: number, quantity: number){
      return this.http.post(`${this.ROOT_URL}/add`, {
        userId,
        productId,
        quantity
      }).pipe(
        tap(() => this.getCart(userId).subscribe())
      );
  }

  // UPDATE CART ITEM
  updateCartItem(userId: number, productId: number, quantity: number) {
    return this.http.put(`${this.ROOT_URL}/update`, {
      userId,
      productId,
      quantity
    }).pipe(
        tap(() => this.getCart(userId).subscribe())
      );
  }

  // REMOVE CART ITEM
  removeCartItem(userId: number, productId: number) {
    return this.http.delete(`${this.ROOT_URL}/${userId}/${productId}`)
    .pipe(
        tap(() => this.getCart(userId).subscribe())
      );
  }
}
