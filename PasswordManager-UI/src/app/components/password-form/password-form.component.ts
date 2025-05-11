import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { PasswordService } from '../../services/password.service';
import { Password } from '../../models/password.model';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule  } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-password-form',
  templateUrl: './password-form.component.html',
  styleUrls: ['./password-form.component.scss'],
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule, FormsModule  ],
})
export class PasswordFormComponent implements OnInit {
  password: Password = { category: '', app: '', userName: '', password: '', encryptedPassword: '', decryptedPassword: '' };
  confirmPassword: string = '';
  isEdit: boolean = false;
  passwordStrength: string = 'Weak';
  passwordsMatch: boolean = true;

  showPassword: boolean = false;
showConfirmPassword: boolean = false;

  constructor(
    private passwordService: PasswordService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    if (id) {
      this.isEdit = true;
      this.passwordService.getPasswordById(id, true).subscribe((data: any) => {
        this.password = data;
        this.confirmPassword = data.decryptedPassword;
        this.validatePassword();
        this.checkPasswordsMatch();
      });
    }
  }

  validatePassword(): void {
    const value = this.password.decryptedPassword;
    const strongRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?#&^_-])[A-Za-z\d@$!%*?#&^_-]{8,}$/;

    this.passwordStrength = strongRegex.test(value) ? 'Strong' : 'Weak';
    this.checkPasswordsMatch();
  }

  checkPasswordsMatch(): void {
    this.passwordsMatch = this.password.decryptedPassword === this.confirmPassword;
  }

  onSubmit(): void {
    if (!this.passwordsMatch || this.passwordStrength === 'Weak') return;
    //debugger;

    const passwordData = {
      category: this.password.category,
      app: this.password.app,
      userName: this.password.userName,
      password: this.password.decryptedPassword,  // Assuming decryptedPassword is the password field
    };

    if (this.isEdit) {
      this.passwordService.updatePassword(this.password.id!, passwordData).subscribe(() => {
        this.router.navigate(['/']);
      });
    } else {
      this.passwordService.addPassword(passwordData).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }
}

