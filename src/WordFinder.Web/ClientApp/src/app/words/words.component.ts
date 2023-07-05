import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-words',
  templateUrl: './words.component.html',
  styleUrls: ['./words.component.css']
})
export class WordsComponent implements OnInit {
  public words: string[] = ["Hello", "World", "From", "Angular"];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<string[]>(baseUrl + 'Words').subscribe(result => {
      this.words = result;
    }, error => console.error(error));
   }

  ngOnInit() {
  }

}
