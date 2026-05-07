import { Component, Inject } from '@angular/core';
import { OrderResponse } from '../../models/checkout';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-success',
  imports: [CommonModule],
  templateUrl: './order-success.html',
  styleUrl: './order-success.css',
})
export class OrderSuccess {

  order?: OrderResponse;

  constructor(private router: Router){

    this.order = history.state['order'];

    console.log('ORDER IN PAGE', this.order);

  }
}
