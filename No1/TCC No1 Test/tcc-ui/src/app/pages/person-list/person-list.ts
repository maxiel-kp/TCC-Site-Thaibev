import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonService } from '../../services/person';
import { ChangeDetectorRef } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Person } from '../../models/person';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-person-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './person-list.html',
  styleUrls: ['./person-list.css']
})

export class PersonListComponent implements OnInit {

  modal = 1;
  page = 1;
  showModal = false;
  modalMode: 'view' | 'add' = 'view';

  persons$ = new BehaviorSubject<Person[]>([]);
  selectedPerson: Person | null = null;
  newPerson: Person = {
    firstName: '',
    lastName: '',
    birthDate: '',
    address: ''
  };

  constructor(
    private personService: PersonService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    //console.log("page: ",this.page);
    this.loadPersons();
  }

  loadPersons() {

    this.personService.getAll(this.page)
      .subscribe((res: Person[]) => {

        this.persons$.next(res);

      });

  }

  getRunning(i: number) {
    console.log("page: ", this.page);
    console.log("i: ", i);
    return (this.page - 1) * 10 + i + 1;
  }

  getAge(birthDate?: string) {

    if (!birthDate) return 0;

    const birth = new Date(birthDate);
    const today = new Date();

    return today.getFullYear() - birth.getFullYear();

  }

  openAdd() {
    this.modal = 2;
    this.showModal = true;
    this.modalMode = 'add';
    this.newPerson = {
      firstName: '',
      lastName: '',
      birthDate: '',
      address: ''
    };

  }

  closeAdd() {
    this.modal = 1;
    this.showModal = false;

  }

  savePerson() {

    this.modal = 1;
    this.showModal = false;

    this.personService.add(this.newPerson)
      .subscribe({

        next: (savedPerson: Person) => {

          const current = this.persons$.value;

          this.persons$.next([
            savedPerson,
            ...current
          ]);

          this.closeModal();

          this.loadPersons();

          this.newPerson = {
            firstName: '',
            lastName: '',
            birthDate: '',
            address: ''
          };


        },

        error: (err) => {
          console.error("save error", err);
        },

      });
  }

  viewPerson(p: any) {
    this.modal = 3;
    this.modalMode = 'view';
    this.selectedPerson = p;
    this.showModal = true;
  }

  closeModal() {
    this.modal = 1;
    this.showModal = false;
  }

  nextPage() {
    this.page++;
    this.loadPersons();
  }

  prevPage() {

    if (this.page > 1) {
      this.page--;
      this.loadPersons();
    }

  }

}
