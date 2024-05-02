import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from '../core/global.service';
import { NewUrlDTO } from '../dtos/newUrlDTO';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  url:any = `${this.globalSvc.config.baseUrl}/urls/`;
  constructor(private globalSvc:GlobalService, private http: HttpClient) { }

  createNewURL(newURLDTO:NewUrlDTO):Observable<any>{
    return this.http.post(`${this.url}`,newURLDTO) as Observable<any>;
  }
  fetchUrl(key:string):Observable<any>{
    return this.http.get(`${this.url}bykey/${key}`) as Observable<any>
  }
}
