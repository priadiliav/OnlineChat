import { Component, OnInit } from '@angular/core';
import { ChatService } from './chat.service';

interface ChatMessage {
  user: string;
  content: string;
}

@Component({
  selector: 'app-chat',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  messages: ChatMessage[] = [];
  messageContent: string = '';
  user: string = 'User1';

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.startConnection();
    this.chatService.addReceiveMessageListener((user, message) => {
      this.messages.push({ user, content: message });
    });
  }

  sendMessage(): void {
    if (this.messageContent.trim()) {
      this.chatService.sendMessage(this.user, this.messageContent);
      this.messageContent = '';
    }
  }
}
