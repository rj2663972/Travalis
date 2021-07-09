import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const appRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    // redirectTo: '/home/login'
    redirectTo: '/sales'
  },
  {
    path: 'home',
    loadChildren: () => import('../components/login/components/login.module').then(mod => mod.LoginModule)
  },
  {
    path: 'sales',
    loadChildren: () => import('./travalis/components/sales/sales.module').then(mod => mod.SalesModule)
  },
  {
    path: '**',
    redirectTo: 'admin/login'
  }

];

@NgModule({
  imports: [
    BrowserModule,

    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
