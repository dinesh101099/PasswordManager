import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PasswordService } from '../../services/password.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-password-view',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './password-view.component.html',
  styleUrl: './password-view.component.scss'
})
export class PasswordViewComponent implements OnInit{
  username: string = ''; // To hold the entered username
  passwordDetails : any;

  constructor(
    private passwordService: PasswordService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  getUserDetails(): void {
    if (this.username) {
      this.passwordService.getPasswordByUserName(this.username, true).subscribe(
        (data) => {
          debugger;
          this.passwordDetails = data; // Assuming it returns an array of users
          this.passwordDetails = Array.isArray(data) ? data : [data];
        },
        (error) => {
          alert('Records not found');
        }
      );
    }
  }
}
