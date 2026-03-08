import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  apiUrl = "https://localhost:7009/api/persons";

  constructor(private http: HttpClient) { }

  getAll(page: number) {
    return this.http.get(`${this.apiUrl}?page=${page}`);
  }

}
