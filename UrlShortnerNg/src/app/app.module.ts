import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppInitService } from './app-init.service';
import { HttpClient, HttpClientModule} from '@angular/common/http'
import { FormsModule } from '@angular/forms';
import { NewShortUrlComponent } from './url/new-short-url/new-short-url.component';
import { RedirectComponent } from './url/redirect/redirect.component';


const startupServiceFactory = (appinit: AppInitService) => {
  console.debug("starupServiceFactory()");
  return() => appinit.getSettings();
}

@NgModule({
  declarations: [
    AppComponent,
    NewShortUrlComponent,
    RedirectComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [AppInitService, {
    provide:APP_INITIALIZER,
    useFactory:startupServiceFactory,
    deps: [AppInitService],
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
