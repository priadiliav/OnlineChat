import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7044/chat')
      .build();
  }

  startConnection(): void {
    this.hubConnection.start()
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  addReceiveMessageListener(callback: (user: string, message: string) => void): void {
    this.hubConnection.on('ReceiveMessage', callback);
  }

  sendMessage(user: string, message: string): void {
    this.hubConnection.invoke('SendMessage', message)
      .catch(err => console.error('Error while sending message: ' + err));
  }
}
