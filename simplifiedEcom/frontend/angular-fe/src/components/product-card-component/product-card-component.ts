import { Component, Input } from '@angular/core';
import { GetAllProduct } from '../../models/GetAllProduct';
import { RouterLink } from '@angular/router';
import { CartService } from '../../services/cart-service';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-product-card-component',
  imports: [RouterLink],
  templateUrl: './product-card-component.html',
  styleUrl: './product-card-component.css',
})
export class ProductCardComponent {

  @Input() productInput!: GetAllProduct;
  userId!: number;

  constructor(private cartService: CartService, private authService: AuthService){
    this.userId = this.authService.getUserId();
  }

  addToCart(productId: number){
    this.cartService.addToCart(this.userId, this.productInput.id, 1)
                    .subscribe(() => {
                      console.log('Product added to cart');
                    });
  }
}
