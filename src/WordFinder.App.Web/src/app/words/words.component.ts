import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Word, WordGroup } from './word';

@Component({
  selector: 'app-words',
  templateUrl: './words.component.html',
  styleUrls: ['./words.component.css']
})
export class WordsComponent implements OnInit {

  private _url: string = 'https://localhost:32768/api/wordfinder/';
  
  public wordGroups: WordGroup[] = [];
  public letters: string = '';

  constructor(private http: HttpClient) { }

  ngOnInit() { }

  public findWords() {
    console.log('findWords() called with letters: ' + this.letters);
    this.http.get<Word[]>(this._url + this.letters).subscribe({
      next: (data: any) => {
        console.log('Data received');
        this.wordGroups = this.groupWordsByLength(data.words);
        console.log('Data loaded');
      },
      error: (err) => console.log(err),
      complete: () => console.log('complete')
    });
  }

  private groupWordsByLength(words: Word[]): WordGroup[] {
    let wordGroups: WordGroup[] = [];
    words.forEach(word => {
      let wordGroup: any = wordGroups.find(wg => wg.group === word.length.toString());
      if (wordGroup === undefined) {
        wordGroup = new WordGroup();
        wordGroup.group = word.length.toString();
        wordGroup.words = [];
        wordGroups.push(wordGroup);
      }
      wordGroup.words.push(word.value);
    });
    return wordGroups;
  }

}
