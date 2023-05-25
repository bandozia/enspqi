import { Injectable, computed, effect, signal } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'
import { ChatConn, HubCallbacks, Room } from './chat.types';
import { ChatHttpService } from './chat-http.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  #activeConnection = signal<HubConnection | null>(null);  
  activeConnection = computed<ChatConn | null>(() => 
    this.#activeConnection()?.connectionId 
      ? {id: this.#activeConnection()!.connectionId!, url: this.#activeConnection()!.baseUrl} 
      : null
  );

  #rooms = signal<Room[]>([]);
  rooms = computed(() => this.#rooms());  

  constructor(private chatHttp: ChatHttpService) { }

  async connectTo(url: string, displayName: string) {    
    let conn = new HubConnectionBuilder()
      //.withUrl(url, { accessTokenFactory: () => "token_aqui" })
      .withUrl(`${url}?displayName=${displayName}`)
      .withAutomaticReconnect()
      .build();
      
      try {        
        await conn.start();                
        let rooms = await this.chatHttp.getAllRooms()        
        
        conn.on(HubCallbacks.GeneralReceived, this.generalMessageReceived.bind(this))     
        conn.on(HubCallbacks.NewRoomCreated, this.newRoomCreated.bind(this))
        conn.on(HubCallbacks.UserJoinedToRoom, this.userJoinedToGroup.bind(this))
        conn.on(HubCallbacks.ReceiveInRoom, this.roomMessageReceived.bind(this))
        
        this.#activeConnection.update(() => conn)
        this.#rooms.update(() => rooms);
      } catch (err) {        
        await conn.stop();
        this.#activeConnection.update(() => null);
        console.error(err)
      }
  }

  async createAndJoin(roomName: string) {
    let newRoom = await this.#activeConnection()!.invoke<Room>('CreateAndJoin', roomName)
    newRoom.isParticipant = true;
    this.#rooms.update(r => [...r, newRoom]);
  }
  
  async join(rommId: string) {
    await this.#activeConnection()!.invoke('Join', rommId)
    this.#rooms.mutate(rms => {
      rms.find(x => x.id == rommId)!.isParticipant = true;
    })
  }

  sendToAll(msg: string) {    
    this.#activeConnection()!.invoke('SendToAll', msg)
  }

  sendToRoom(roomId: string, msg: string) {
    this.#activeConnection()!.invoke('SendToRoom', roomId, msg)
  }

  newRoomCreated(room: Room) {
    this.#rooms.update(r => [...r, room])
  }

  userJoinedToGroup(userName: string, groupId: string) {
    console.log('user joined to group:', userName, groupId)
  }

  generalMessageReceived(msg: string) {
    console.log(msg)
  }

  roomMessageReceived(roomId: string, userName: string, msg: string) {
    console.log(roomId, userName, msg)
  }

}
