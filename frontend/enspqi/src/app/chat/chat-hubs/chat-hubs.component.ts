import { Component, Signal } from '@angular/core';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-chat-hubs',
  templateUrl: './chat-hubs.component.html'
})
export class ChatHubsComponent {

  activeConnections = this.chatService.activeConnections;
  
  constructor(private chatService: ChatService) {}
  
  async connectToHub() {      
    await this.chatService.connectTo("http://localhost:5104/chat/general");
  }

  async sendToAll() {
    await this.chatService.sendToAll("test test", "http://localhost:5104/chat/general")
  }
}
