import { Injectable } from '@angular/core';
import { AppInitService } from '../app-init.service';
@Injectable({
  providedIn: 'root'
})
export class GlobalService {
 
  get config() {return this.init.config};
  
  constructor(private init:AppInitService) { 
    
  }
}
