import { Component } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { AuthService } from '../../services/auth-service';
import { CommonModule, NgIf } from '@angular/common';
import { CartService } from '../../services/cart-service';
import { map, Observable } from 'rxjs';

@Component({
  selector: 'app-header-component',
  imports: [RouterLink, NgIf, CommonModule],
  templateUrl: './header-component.html',
  styleUrl: './header-component.css',
})
export class HeaderComponent {
  username: string | null = null;

  cartCount$!: Observable<number>;

  constructor(private authService: AuthService, private route: Router, private cartService: CartService){
    this.authService.currentuser$
      .subscribe(user => {
        this.username = user;
      })
  }

  ngOnInit(): void {
    this.cartCount$ = this.cartService.cartItems$.pipe(
      map(items => items.reduce((total, item) => total + item.quantity, 0))
    );
  }



  logout(){
    this.authService.logout();
    this.route.navigate(['/'])
  }
}
