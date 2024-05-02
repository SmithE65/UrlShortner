import { Component } from '@angular/core';
import { ShortenURL } from '../url';
import { GlobalService } from 'src/app/core/global.service';
import { UrlService } from '../url.service';
import { Route, Router} from '@angular/router';
import { NewUrlDTO } from 'src/app/dtos/newUrlDTO';


@Component({
  selector: 'app-new-short-url',
  templateUrl: './new-short-url.component.html',
  styleUrls: ['./new-short-url.component.css']
})
export class NewShortUrlComponent {
  newUrlDTO: NewUrlDTO = new NewUrlDTO();
  shortenUrl =  this.globalSvc.config.shortUrl;
  newURL!:ShortenURL;
  message:string = "";
  constructor(private globalSvc:GlobalService,
    private urlSvc:UrlService
  ){}


  createNewUrl():void{
    this.newUrlDTO.shortURL = this.globalSvc.config.shortUrl;

    this.urlSvc.createNewURL(this.newUrlDTO).subscribe({
      next:(res) => {
        console.debug(res);
        this.newURL = res;
        this.message = `<a href="${this.newURL.shortUrl}"></a>`;
      },
      error:(err) => {
        console.error(err);
      }
    });
  }
}
