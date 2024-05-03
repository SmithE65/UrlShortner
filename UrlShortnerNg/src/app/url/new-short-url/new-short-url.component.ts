import { Component } from '@angular/core';
import { ShortenURL } from '../url';
import { GlobalService } from 'src/app/core/global.service';
import { UrlService } from '../url.service';
import { Route, Router} from '@angular/router';
import { NewUrlDTO } from 'src/app/dtos/newUrlDTO';
import { DomSanitizer } from '@angular/platform-browser';
import { Clipboard } from '@angular/cdk/clipboard'

@Component({
  selector: 'app-new-short-url',
  templateUrl: './new-short-url.component.html',
  styleUrls: ['./new-short-url.component.css']
})
export class NewShortUrlComponent {
  newUrlDTO: NewUrlDTO = new NewUrlDTO();
  shortenUrl =  this.globalSvc.config.shortUrl;
  newURL!:ShortenURL;
  message: any;
  constructor(private globalSvc:GlobalService,
    private urlSvc:UrlService,
    private sanitizer :DomSanitizer,
    private clippy: Clipboard
  ){}


  createNewUrl():void{
    this.newUrlDTO.shortURL = this.globalSvc.config.shortUrl;

    this.urlSvc.createNewURL(this.newUrlDTO).subscribe({
      next:(res) => {
        console.debug(res);
        this.newURL = res;
        this.message = this.sanitizer.bypassSecurityTrustHtml(`Short URL: <a href="${this.newURL.shortUrl}">${this.newURL.shortUrl}</a>         <button (click)="copyUrl()"><i class="bi bi-copy"></i></button>`);
      },
      error:(err) => {
        console.error(err);
      }
    });
  }
  copyUrl():void{
    this.clippy.copy(this.newURL.shortUrl);
  }
}
