import { Component, Signal } from '@angular/core';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-chat-hubs',
  templateUrl: './chat-hubs.component.html'
})
export class ChatHubsComponent {

  activeConnection = this.chatService.activeConnection;
  rooms = this.chatService.rooms;

  constructor(private chatService: ChatService) {}
  
  connectToHub(displayName: string) {      
    this.chatService.connectTo("http://localhost:5104/chat/general", displayName);
  }

  createRoomAndJoin(name: string) {
    this.chatService.createAndJoin(name)
  }
  
  join(roomId: string) {
    this.chatService.join(roomId)
  }

  sendToAll() {
    this.chatService.sendToAll("test test")    
  }

  sendToRoom(roomId: string, msg: string) {
    this.chatService.sendToRoom(roomId, msg)
  }
}
