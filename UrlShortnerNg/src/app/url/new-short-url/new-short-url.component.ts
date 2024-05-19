import { Component } from '@angular/core';
import { ShortenUrl } from '../url';
import { GlobalService } from 'src/app/core/global.service';
import { UrlService } from '../url.service';
import { NewUrlDto } from 'src/app/dtos/newUrlDto';
import { Clipboard } from '@angular/cdk/clipboard'

@Component({
  selector: 'app-new-short-url',
  templateUrl: './new-short-url.component.html',
  styleUrls: ['./new-short-url.component.css']
})
export class NewShortUrlComponent {
  newUrlDto: NewUrlDto = new NewUrlDto;
  shortenUrl = this.globalSvc.config.shortUrl;
  newUrl: ShortenUrl = new ShortenUrl;
  message: any;

  constructor(private globalSvc: GlobalService,
    private urlSvc: UrlService,
    private clippy: Clipboard
  ) { }


  createNewUrl(): void {
    this.newUrlDto.shortURL = this.globalSvc.config.shortUrl;

    this.urlSvc.createNewUrl(this.newUrlDto).subscribe({
      next: (res) => {
        console.debug(res);
        this.newUrl = res;
        this.message = this.newUrl.shortUrl;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  copyUrl(): void {
    this.clippy.copy(this.newUrl.shortUrl);
  }
}
