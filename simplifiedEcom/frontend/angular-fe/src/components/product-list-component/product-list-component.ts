import { Component } from '@angular/core';
import { ProductCardComponent } from "../product-card-component/product-card-component";

@Component({
  selector: 'app-product-list-component',
  imports: [ProductCardComponent],
  templateUrl: './product-list-component.html',
  styleUrl: './product-list-component.css',
})
export class ProductListComponent {}
