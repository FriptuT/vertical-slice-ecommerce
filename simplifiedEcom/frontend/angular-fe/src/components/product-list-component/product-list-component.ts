import { Component, inject } from '@angular/core';
import { ProductCardComponent } from '../product-card-component/product-card-component';
import { ProductService } from '../../services/product-service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable, switchMap } from 'rxjs';
import { GetAllProduct } from '../../models/GetAllProduct';
import { Subcategories } from '../../models/Subcategories';

@Component({
  selector: 'app-product-list-component',
  imports: [ProductCardComponent, AsyncPipe, CommonModule, RouterLink],
  templateUrl: './product-list-component.html',
  styleUrl: './product-list-component.css',
})
export class ProductListComponent {
  private productService = inject(ProductService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  products$!: Observable<GetAllProduct[]>;
  categories$ = this.productService.getCategories();
  brands$ = this.productService.getBrands();

  expandedCategoryId: number | null = null;

  // salvam subcategories pe fiecare category
  subcategoriesMap: { [key: number]: Subcategories[] } = {};

  ngOnInit() {
    // load initial products
    this.products$ = this.route.queryParams.pipe(
      switchMap((params) => {
        const categoryId = params['categoryId'];

        const subcategoryId = params['subcategoryId'];

        const brandId = params['brandId'];

        return this.productService.getProducts(categoryId, subcategoryId, brandId);
      }),
    );

    // load subcategories per category
    this.categories$.subscribe((categories) => {
      categories.forEach((category) => {
        this.productService.getSubcategories(category.id).subscribe((subs) => {
          this.subcategoriesMap[category.id] = subs;
        });
      });
    });
  }

  // click pe subcategory
  filterBySubcategory(subcategoryId: number) {
    this.router.navigate([], {
      queryParams: {
        subcategoryId: subcategoryId,
      },
    });
  }

  // click pe brand
  filterByBrand(brandId: number) {
    this.router.navigate([], {
      queryParams: {
        brandId: brandId,
      },
    });
  }


  // la click pe categorie, se inchide sau se deschide
  toggleCategory(categoryId: number) {
    if (this.expandedCategoryId == categoryId) {
      this.expandedCategoryId = null;
    } else {
      this.expandedCategoryId = categoryId;
    }
  }


}
