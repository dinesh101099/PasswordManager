import { Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { routes } from './app.routes';
import { PasswordListComponent } from './components/password-list/password-list.component';
import { PasswordFormComponent } from './components/password-form/password-form.component';
import { PasswordViewComponent } from './components/password-view/password-view.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, HttpClientModule, PasswordListComponent, PasswordFormComponent, PasswordViewComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'PasswordManager-UI';
}
