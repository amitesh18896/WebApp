
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { EditBookComponent } from '../edit-book/edit-book.component';
import { ActivatedRoute, Router } from '@angular/router';


interface Book {
  Id: number;
  Title: string;
  Description: string;
  PublishedOn: string;
  Author: string;
}



@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css'],
})
export class BookComponent implements OnInit {

  book: Book = {
    Id: 0,
    Title: '',
    Description: '',
    PublishedOn: '',
    Author: '',
  };

  books: any;
  isLoading: boolean = true;
  output: any;
  private _Route: any;


  constructor(private http: HttpClient, private dialog: MatDialog,


    private router: Router,
    private route: ActivatedRoute,



  ) { }

  ngOnInit() {
    this.http.get('/api/books').subscribe((data) => {
      this.books = data;
      this.isLoading = false;

    });
  }


  openDialog() {
    this.dialog.open(EditBookComponent, {
      width: '550px',
    });
  }

 


  updateBook(id: number) {
    this.http.put('/api/books/' + id, this.book).subscribe(
      () => {
        this.router.navigate(['/details', id]);
        this.router.navigate(['/books']);
      },
      (err) => {
        console.error('Error:', err);

      }
    );
  }

}
