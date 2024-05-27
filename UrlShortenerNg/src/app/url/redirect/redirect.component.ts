import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { UrlService } from '../url.service';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css']
})
export class RedirectComponent {

  constructor(private urlSvc:UrlService, private route:Router, private router:ActivatedRoute){}

  ngOnInit():void {
    let key:string = this.router.snapshot.params['key'];
    console.log(key);
    this.urlSvc.fetchUrl(key).subscribe({
      next:(res) => {
        console.log(res);
        window.location.href = res.longUrl;
      },
      error(err) {
        console.error(err);
      },
    })
  }
}
