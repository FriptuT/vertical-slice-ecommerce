import { Routes } from '@angular/router';
import { ProductListComponent } from '../components/product-list-component/product-list-component';
import { ProductDetailsComponent } from '../components/product-details-component/product-details-component';

export const routes: Routes = [
    {
        path: 'products',
        component: ProductListComponent
    },
    {
        path: 'products/:id',
        component: ProductDetailsComponent
    }
];
