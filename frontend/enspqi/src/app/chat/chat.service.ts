import { Injectable, computed, signal } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'
import { ChatConn, HubCallbacks } from './chat.types';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  #activeConnections = signal<HubConnection[]>([])
  activeConnections = computed<ChatConn[]>(() => this.#activeConnections().map(x => ({id: x.connectionId!, url: x.baseUrl}) ))  

  constructor() { }

  async connectTo(url: string) {
    let conn = new HubConnectionBuilder()
      //.withUrl(url, { accessTokenFactory: () => "token_aqui" })
      .withUrl(url)
      .withAutomaticReconnect()      
      .build();
      
      try {        
        await conn.start();        
        
        conn.on(HubCallbacks.GeneralReceived, this.messageReceived)
        
        this.#activeConnections.update(c => [...c, conn])
      } catch (err) {
        console.error(err)
      }
  }

  async sendToAll(msg: string, hub: string) {
    let conn = this.#activeConnections().find(x => x.baseUrl == hub)

    if (conn == null) {
      console.error('deu merda')
    }

    await conn?.invoke('SendToAll', msg)    
  }

  messageReceived(msg: string) {
    console.log(msg)
  }


}
