import { Routes } from '@angular/router';
import { PasswordListComponent } from './components/password-list/password-list.component';
import { PasswordFormComponent } from './components/password-form/password-form.component';
import { PasswordViewComponent } from './components/password-view/password-view.component';

export const routes: Routes = [
  { path: '', component: PasswordListComponent },
  { path: 'edit/:id', component: PasswordFormComponent },
  { path: 'add', component: PasswordFormComponent },
  { path: 'view', component: PasswordViewComponent },
];
