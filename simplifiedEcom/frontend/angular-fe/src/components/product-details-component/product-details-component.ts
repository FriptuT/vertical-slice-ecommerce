import { Component, inject } from '@angular/core';
import { ProductService } from '../../services/product-service';
import { ActivatedRoute } from '@angular/router';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-product-details-component',
  imports: [AsyncPipe],
  templateUrl: './product-details-component.html',
  styleUrl: './product-details-component.css',
})
export class ProductDetailsComponent {

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);

  product$ = this.productService.getProductById(
    Number(this.route.snapshot.paramMap.get('id'))
  );
}
