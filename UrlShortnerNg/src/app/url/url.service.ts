import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from '../core/global.service';
import { NewUrlDto } from '../dtos/newUrlDto';
import { Observable } from 'rxjs';
import { ShortenUrl } from './url';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  url: string = `${this.globalSvc.config.baseUrl}/urls/`;

  constructor(private globalSvc: GlobalService, private http: HttpClient) { }

  createNewUrl(newUrlDto: NewUrlDto): Observable<ShortenUrl> {
    return this.http.post(`${this.url}`, newUrlDto) as Observable<ShortenUrl>;
  }

  fetchUrl(key: string): Observable<any> {
    return this.http.get(`${this.url}${key}`) as Observable<any>
  }
}
