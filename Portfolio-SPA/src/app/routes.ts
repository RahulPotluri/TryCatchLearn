import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProjectsComponent } from './Projects/Projects.component';
import { ContactComponent } from './Contact/Contact.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
  /// need to specify empty path '', which redirects to Home
  { path: '', component: HomeComponent },
  /// Adding Authguard to all children nodes
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'projects', component: ProjectsComponent },
      { path: 'contact', component: ContactComponent },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

export const RoutesRoutes = RouterModule.forChild(appRoutes);
