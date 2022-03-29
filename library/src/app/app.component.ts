import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Book } from './book.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  books: Book[] = [];

  title: string = "";
  author: string = "";
  description: string = "";
  
  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get<Book[]>("https://localhost:44361/api/Book/GetAll").subscribe((books: Book[]) => {
      this.books = books;
    });
  }

  onBookAdd(): void {
    const book = new Book(0, this.author, this.title, this.description);

    this.httpClient.post<Book>("https://localhost:44361/api/Book/Add", book).subscribe((book: Book) => {
      this.books.push(book);
    })
  }
}
