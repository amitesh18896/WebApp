import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogRef } from '@angular/material/dialog';
import { BookComponent } from '../book/book.component';
interface Book {
  Id: number;
  Title: string;
  Description: string;
  PublishedOn: string;
  Author: string;
}

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class EditBookComponent implements OnInit {
  book: Book = {
    Id: 0,
    Title: '',
    Description: '',
    PublishedOn: '',
    Author: '',
  };

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog,
  
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.getBookDetail(id);
    }
  }

  getBookDetail(id: number) {
    this.http.get<Book>('/api/books/' + id).subscribe(
      (data) => {
        this.book = data;
      },
      (err) => {
        console.error('Error:', err);
       
      }
    );
  }

  updateBook(id: number) {
    this.http.put('/api/books/' + id, this.book).subscribe(
      () => {
      //  this.router.navigate(['/details', id]);
        this.router.navigate(['/books']);
      },
      (err) => {
        console.error('Error:', err);
     
      }
    );
  }

  


  deleteBook(id: number) {
    this.http.delete('/api/books/' + id).subscribe(
      () => {
        this.router.navigate(['/books']);
      },
      (err) => {
        console.error('Error:', err);
      }
    );
  }
}














