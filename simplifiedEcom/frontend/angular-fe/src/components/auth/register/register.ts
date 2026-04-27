import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../services/auth-service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  registerForm: FormGroup;
  message = '';

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private authService: AuthService,
    private route: Router){

      this.registerForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]],
        name: ['', [Validators.required]]
      });
    }

    onSubmit() {
      console.log(this.registerForm.value);
      if (this.registerForm.invalid) return;

      this.authService.register(this.registerForm.value).subscribe({
        next: (response) => {
          this.message = 'Registered successful';
          console.log(response);
          this.route.navigate(['/login']);
        },
        error: (error) => {
          this.message = 'Register failed';
          console.error(error);
        }
      })
    }
}
