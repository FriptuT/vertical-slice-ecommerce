import { Component, inject } from '@angular/core';
import { ProductCardComponent } from "../product-card-component/product-card-component";
import { ProductService } from '../../services/product-service';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list-component',
  imports: [ProductCardComponent, AsyncPipe, CommonModule],
  templateUrl: './product-list-component.html',
  styleUrl: './product-list-component.css',
})
export class ProductListComponent {

  private productService = inject(ProductService);

  products$ = this.productService.getProducts();

}
