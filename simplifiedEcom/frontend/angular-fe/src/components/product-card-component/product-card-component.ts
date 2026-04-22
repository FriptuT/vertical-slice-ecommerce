import { Component, Input } from '@angular/core';
import { GetAllProduct } from '../../models/GetAllProduct';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-card-component',
  imports: [RouterLink],
  templateUrl: './product-card-component.html',
  styleUrl: './product-card-component.css',
})
export class ProductCardComponent {

  @Input() productInput!: GetAllProduct;
}
