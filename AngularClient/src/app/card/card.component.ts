import { asElementData } from '@angular/core/src/view/types';
import { Component, Input, OnInit } from '@angular/core';
import { Card } from './Card';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {

  _card: Card;
  showEnglish: boolean = true;

  @Input() 
  set card(newCard: Card) { this._card = newCard; this.showEnglish = true;}
  get name(): Card { return this._card; }

  constructor() { 
  }

  ngOnInit() {
  }

}
