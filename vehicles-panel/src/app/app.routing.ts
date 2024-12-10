import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DefaultLayoutComponent } from './containers';
import { AuthGuardService } from './services/http-services/auth-guard.service';
import { LoginComponent } from './views/authentication/login/login.component';
import { CustomerComponent } from './views/customer/customer.component';
import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { VehiclesComponent } from './views/vehicles/vehicles.component';

export const routes: Routes = [
  //{ path: '', redirectTo: 'auth', pathMatch: 'full' }, //default route
  { path: '', redirectTo: 'auth', pathMatch: 'full' }, //default route
  { path: '404', component: P404Component, data: { title: 'Page 404' } },
  { path: '500', component: P500Component, data: { title: 'Page 500' } },
  {
    path: '', component: DefaultLayoutComponent, data: {},
    children: [
   
  { path: 'vehicles', component: VehiclesComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
  { path: 'customer', component: CustomerComponent, pathMatch: 'full', canActivate: [AuthGuardService] },

    ]
  },

  {
    path: 'auth', loadChildren: () => import('./views/authentication/authentication.module').then(m => m.AuthenticationModule), canActivateChild: [AuthGuardService] 
  },
  { path: '**', component: P404Component }
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
