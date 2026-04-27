import { Component } from '@angular/core';
import { Router, RouterLink } from "@angular/router";
import { AuthService } from '../../services/auth-service';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-header-component',
  imports: [RouterLink, NgIf, CommonModule],
  templateUrl: './header-component.html',
  styleUrl: './header-component.css',
})
export class HeaderComponent {
  username: string | null = null;

  constructor(private authService: AuthService, private route: Router){
    this.authService.currentuser$
      .subscribe(user => {

        this.username = user;
      
      })
  }

  logout(){
    this.authService.logout();
    this.route.navigate(['/'])
  }
}
