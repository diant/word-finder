import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-words',
  templateUrl: './words.component.html',
  styleUrls: ['./words.component.css']
})
export class WordsComponent implements OnInit {

  private _url: string = 'https://localhost:32768/api/wordfinder';
  private _http: HttpClient;

  public words: string[] = [];

  constructor(private http: HttpClient) {
    this._http = http;
   }

  ngOnInit() {
    this._http.get<string[]>(this._url).subscribe({
      next: (data) => {
        console.log('Data received');
        this.words = data;
        console.log('Data loaded');
      },
      error: (err) => console.log(err),
      complete: () => console.log('complete')
    });

  }

}
