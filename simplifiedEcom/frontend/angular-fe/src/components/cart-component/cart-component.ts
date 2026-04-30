import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../models/CartItem';
import { CartService } from '../../services/cart-service';
import { AuthService } from '../../services/auth-service';
import { AsyncPipe, NgFor } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cart-component',
  imports: [NgFor, AsyncPipe],
  templateUrl: './cart-component.html',
  styleUrl: './cart-component.css',
})
export class CartComponent implements OnInit {

  cartItems: CartItem[] = [];

  userId!: number;

  constructor(private cartService: CartService, private authService: AuthService){}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.loadCart();
  }

  loadCart() {
    this.cartService.getCart(this.userId).subscribe(res => this.cartItems = res);
  }

  increase(item: CartItem){
    this.cartService.updateCartItem(
      this.userId, 
      item.productId, 
      item.quantity + 1
    ).subscribe(() => this.loadCart());
  }

  decrease(item: CartItem){
      if (item.quantity <= 1) return;

      this.cartService.updateCartItem(
        this.userId,
        item.productId,
        item.quantity - 1
      ).subscribe(() => this.loadCart());
  }

  remove(item: CartItem){
    console.log(item);
    this.cartService.removeCartItem(
      this.userId,
      item.productId
    ).subscribe(() => {
        this.loadCart()
    });
  }
}
