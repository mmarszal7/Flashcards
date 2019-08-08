import { logger } from 'codelyzer/util/logger';
import { Component } from '@angular/core';
import { AngularFireDatabase } from 'angularfire2/database';
import { Card } from './card/Card';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  allCards    = new Array<Card>();
  cards       = new Array<Card>();
  showEnglish = new Array<boolean>(true, true, true, true, true);
  bgColor     = new Array<string>('white', 'white', 'white', 'white', 'white');
  listIterator:number   = 0;

  constructor(private db: AngularFireDatabase){
  };

  ngOnInit(){
    this.db.list('/words', ref => ref.orderByChild('repeat').equalTo(true)).valueChanges().map(response => response as Card[]).subscribe((c) => { this.allCards = c; this.shuffle(this.allCards); this.draw(); });
  };

  draw(){
    this.listIterator += 5;
    if(this.listIterator > this.allCards.length)
      this.listIterator = 0;

    for(let i=0; i < 5; i++){ this.setBg(i); }
    this.showEnglish = new Array<boolean>(true, true, true, true, true);
  };

  shuffle(a) {
    for (let i = a.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [a[i], a[j]] = [a[j], a[i]];
    }
  }

  plusCount(index){
    if(this.allCards[this.listIterator+index].count < 3){
      this.allCards[this.listIterator+index].count++;
      this.setBg(index);
    }
  }

  minusCount(index){
    if(this.allCards[this.listIterator+index].count > 0){
      this.allCards[this.listIterator+index].count--;
      this.setBg(index);
    }
  }

  setBg(index){
    switch(this.allCards[this.listIterator+index].count) { 
      case 3: { 
         this.bgColor[index] = 'red';
         break; 
      } 
      case 2: { 
        this.bgColor[index] = 'orange';
         break; 
      } 
      case 1: { 
        this.bgColor[index] = 'yellow';
        break; 
      }
      case 0: { 
        this.bgColor[index] = 'white';
        break; 
      }
    } 
  }

}
