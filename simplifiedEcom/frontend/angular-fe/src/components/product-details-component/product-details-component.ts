  import { Component, inject } from '@angular/core';
import { ProductService } from '../../services/product-service';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { CartService } from '../../services/cart-service';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-product-details-component',
  imports: [AsyncPipe],
  templateUrl: './product-details-component.html',
  styleUrl: './product-details-component.css',
})
export class ProductDetailsComponent {

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private cartService = inject(CartService);

  userId!: number;

  product$ = this.productService.getProductById(
    Number(this.route.snapshot.paramMap.get('id'))
  );

  constructor(private authService: AuthService){
    this.userId = authService.getUserId();
  }

  addToCart(productId: number, quantity: number){
    this.cartService.addToCart(this.userId, productId, quantity).subscribe(() => console.log("Product added to cart"));
  }
}
