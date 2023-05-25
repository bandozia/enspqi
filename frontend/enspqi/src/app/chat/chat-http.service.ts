import { Injectable } from '@angular/core';
import { Room } from './chat.types';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatHttpService {

  constructor(private http: HttpClient) { }

  getAllRooms() {
    return lastValueFrom(this.http.get<Room[]>("http://localhost:5104/general/rooms"));
  }
  
}
