import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../models/person';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  apiUrl = "https://localhost:7009/api/persons";

  constructor(private http: HttpClient) { }

  getAll(page: number) {
    return this.http.get<Person[]>(
      `${this.apiUrl}?page=${page}`
    );
  }

  add(person: Person) {
    return this.http.post<Person>(
      `${this.apiUrl}`,
      person
    );

  }
}
