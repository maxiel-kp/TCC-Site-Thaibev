import { Component, signal } from '@angular/core';
import { PersonListComponent } from './pages/person-list/person-list';

@Component({
  selector: 'app-root',
  imports: [PersonListComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  //protected readonly title = signal('tcc-ui');
}
