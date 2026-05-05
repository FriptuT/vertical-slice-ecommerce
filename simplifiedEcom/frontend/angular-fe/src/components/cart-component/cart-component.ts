import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../models/CartItem';
import { CartService } from '../../services/cart-service';
import { AuthService } from '../../services/auth-service';
import { AsyncPipe, NgFor } from '@angular/common';
import { Observable } from 'rxjs';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-cart-component',
  imports: [NgFor, AsyncPipe, RouterLink],
  templateUrl: './cart-component.html',
  styleUrl: './cart-component.css',
})
export class CartComponent implements OnInit {

  cartItems$!: Observable<CartItem[]>;

  userId!: number;

  constructor(private cartService: CartService, private authService: AuthService){}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.cartItems$ = this.cartService.cartItems$;
    this.loadCart();
  }

  loadCart() {
    this.cartService.getCart(this.userId).subscribe();
  }


  remove(item: CartItem){
    this.cartService.removeCartItem(
      this.userId,
      item.productId
    ).subscribe();
  }
}
