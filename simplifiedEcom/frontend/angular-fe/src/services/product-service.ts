import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetAllProduct } from '../models/GetAllProduct';
import { Observable } from 'rxjs';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  readonly ROOT_URL = 'http://localhost:5126/api';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<GetAllProduct[]> {
    return this.http.get<GetAllProduct[]>(this.ROOT_URL + '/products');
  }

  getProductById(id: number): Observable<Product>{
    return this.http.get<Product>(this.ROOT_URL + `/products/${id}`);
  }
}
