import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonService } from '../../services/person';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-person-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './person-list.html',
  styleUrls: ['./person-list.css']
})

export class PersonListComponent implements OnInit {

  persons: any[] = [];
  page = 1;

  constructor(
    private personService: PersonService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {

    this.loadPersons();

  }

  loadPersons() {

    this.personService.getAll(this.page)
      .subscribe((res: any) => {

        this.persons = res;

        this.cdr.detectChanges();

      });
       
  }

  getAge(birthDate: string) {

    const birth = new Date(birthDate);
    const today = new Date();

    return today.getFullYear() - birth.getFullYear();

  }

  openAdd() {
    console.log("add modal");
  }

  viewPerson(p: any) {
    console.log("view", p);
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
