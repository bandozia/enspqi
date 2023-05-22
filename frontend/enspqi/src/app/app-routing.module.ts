import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatHubsComponent } from './chat/chat-hubs/chat-hubs.component';

const routes: Routes = [
  { path: '', redirectTo: 'chat', pathMatch: 'full'},
  { path: 'chat', component: ChatHubsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
