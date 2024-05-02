import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewShortUrlComponent } from './url/new-short-url/new-short-url.component';
import { RedirectComponent } from './url/redirect/redirect.component';


const routes: Routes = [
  {path: '', redirectTo:'/newshorturl', pathMatch:'full'},
  {path: 'newshorturl', component:NewShortUrlComponent},
  {path: ':key', component:RedirectComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
