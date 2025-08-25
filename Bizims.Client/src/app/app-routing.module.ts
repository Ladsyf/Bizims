import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// project import
import { AdminComponent } from './layout/admin';
import { EmptyComponent } from './layout/empty';

const routes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: "",
        redirectTo: "businesses",
        pathMatch: "full"
      },
      {
        path: "businesses",
        loadComponent: () => import("./pages/businesses/businesses.component").then((c) => c.BusinessesComponent)
      }
    ]
  },
  {
    path: '',
    component: EmptyComponent,
    children: [
      {
        path: 'auth',
        loadChildren: () => import('./pages/auth/auth.module').then((m) => m.AuthModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
