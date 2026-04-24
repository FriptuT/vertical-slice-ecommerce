import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetAllProduct } from '../models/GetAllProduct';
import { Observable } from 'rxjs';
import { Product } from '../models/Product';
import { GetAllCategories } from '../models/GetAllCategories';
import { Brands } from '../models/Brands';
import { Subcategories } from '../models/Subcategories';
import { __param } from 'tslib';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  readonly ROOT_URL = 'http://localhost:5126/api';

  constructor(private http: HttpClient) {}

  getProducts(categoryId?: number, subcategoryId?: number, brandId?: number): Observable<GetAllProduct[]> {
    let params = new HttpParams();

    if (categoryId) {
      params = params.set('categoryId', categoryId);
    }

    if (subcategoryId) {
      params = params.set('subcategoryId', subcategoryId);
    }

    if (brandId) {
      params = params.set('brandId', brandId);
    }

    return this.http.get<GetAllProduct[]>(this.ROOT_URL + '/products', { params });
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(this.ROOT_URL + `/products/${id}`);
  }

  getCategories(): Observable<GetAllCategories[]> {
    return this.http.get<GetAllCategories[]>(this.ROOT_URL + '/categories');
  }

  getSubcategories(categoryId: number): Observable<Subcategories[]> {
    return this.http.get<Subcategories[]>(this.ROOT_URL + '/subcategories', {
      params: { categoryId },
    });
  }

  getBrands(): Observable<Brands[]> {
    return this.http.get<Brands[]>(this.ROOT_URL + '/brands');
  }
}
