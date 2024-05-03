import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppInitService {
  config: any = null;
  constructor(private http: HttpClient) { }
  getSettings():Promise<void> {
    return new Promise<void>((resolve, reject) =>{
    this.http.get("/assets/config.json").subscribe(
      (cfg:any) => {
        this.config = cfg;
        console.debug("assets/config.json", this.config);
        console.log(this.config);
        resolve();
      },
      err => {
        console.error(err);
        reject();
      }
    );
  });
  }
}
