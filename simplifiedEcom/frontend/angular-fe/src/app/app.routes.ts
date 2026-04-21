import { Routes } from '@angular/router';
import { ProductListComponent } from '../components/product-list-component/product-list-component';

export const routes: Routes = [
    {
        path: 'products',
        component: ProductListComponent
    }
];
