import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CheckoutService } from '../../services/checkout-service';
import { CheckoutRequest } from '../../models/checkout';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout-page',
  imports: [ReactiveFormsModule],
  templateUrl: './checkout-page.html',
  styleUrl: './checkout-page.css',
})
export class CheckoutPage {
  form!: FormGroup;

  constructor(
    private checkoutService: CheckoutService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      shippingName: ['', Validators.required],
      shippingAddress: ['', Validators.required],
      shippingCity: ['', Validators.required],
      shippingPostalCode: ['', Validators.required],
    });
  }

  placeOrder() {
    if (this.form.invalid) return;

    const request: CheckoutRequest = {
      userId: this.authService.getUserId(),
      ...(this.form.value as any),
    };

    this.checkoutService.placeOrder(request).subscribe({
      next: (response: any) => {
        console.log('Order placed', response);

        const order = response.value ?? response;

        console.log('ORDER: ',order);

        this.router.navigate(['/order-success'], {
          state: {order}
        })

      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
