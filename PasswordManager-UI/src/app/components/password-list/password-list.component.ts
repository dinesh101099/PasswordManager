import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { PasswordService } from '../../services/password.service';
import { Password } from '../../models/password.model';
import { HttpClientModule } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-password-list',
  templateUrl: './password-list.component.html',
  styleUrls: ['./password-list.component.scss'],
  standalone: true,
  imports: [MatTableModule, MatButtonModule, CommonModule], // Include the pipe here
})
export class PasswordListComponent implements OnInit {
  passwords: Password[] = [];
  displayedColumns: string[] = ['id', 'category', 'app', 'userName', 'password', 'actions'];
  sortDirection: 'asc' | 'desc' = 'asc';
  decryptMode: boolean = false; // default to false (i.e., encrypt)

  constructor(private passwordService: PasswordService,
    private router: Router
  ) {}

  ngOnInit(): void {
    debugger;
    this.fetchPasswordWithDecryptFlag();
  }

  toggleDecrypt() {
    this.decryptMode = !this.decryptMode;
    this.fetchPasswordWithDecryptFlag();
  }

  fetchPasswordWithDecryptFlag(): void {
    this.passwordService.getPasswords(this.decryptMode).subscribe((data) => {
      this.passwords = data;
    });
  }

  sortData(column: keyof Password): void {
    this.passwords.sort((a: Password, b: Password) => {
      const aValue = a[column];
      const bValue = b[column];
      if (aValue == null || bValue == null) return 0;
      const result = aValue > bValue ? 1 : aValue < bValue ? -1 : 0;
      return this.sortDirection === 'asc' ? result : -result;
    });
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  }

  
  // Mask password method
  maskPassword(password: string): string {
    if (password) {
      const visibleChars = 3; // Show the first 3 characters
      const maskedChars = password.length - visibleChars;
      const mask = '*'.repeat(maskedChars);
      return password.slice(0, visibleChars) + mask;
    }
    alert(password);
    return password;
  }

  deletePassword(id: number): void {
    if (confirm('Are you sure you want to delete this password?')) {
      this.passwordService.deletePassword(id).subscribe(() => {
        this.fetchPasswordWithDecryptFlag();
      });
    }
  }

  navigateToUpdate(id: number): void {
    this.router.navigate(['/edit', id]);
  }
}