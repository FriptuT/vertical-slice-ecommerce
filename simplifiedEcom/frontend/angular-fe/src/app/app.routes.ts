import { Routes } from '@angular/router';
import { ProductListComponent } from '../components/product-list-component/product-list-component';
import { ProductDetailsComponent } from '../components/product-details-component/product-details-component';
import { Login } from '../components/auth/login/login';
import { Register } from '../components/auth/register/register';

export const routes: Routes = [
    {
        path:"login",
        component: Login
    },
    {
        path:"register",
        component: Register
    },
    {
        path: 'products',
        component: ProductListComponent
    },
    {
        path: 'products/:id',
        component: ProductDetailsComponent
    }
];
