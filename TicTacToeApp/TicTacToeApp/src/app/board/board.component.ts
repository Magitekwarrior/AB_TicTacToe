import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {
  squares: string[] = [];
  xIsNext: boolean = true;
  outcome: string = '';
  isGameOver: boolean = false;

  constructor() {}

  ngOnInit() {
    this.newGame();
  }

  newGame() {
    this.squares = Array(9).fill('');
    this.outcome = '';
    this.xIsNext = true;
    this.isGameOver =  false;
  }

  get player() {
    return this.xIsNext ? 'X' : 'O';
  }

  makeMove(idx: number) {
    if (!this.isGameOver){
      if (!this.squares[idx]) {
        this.squares.splice(idx, 1, this.player);
        this.xIsNext = !this.xIsNext;
      }

      var playerWon = this.calculateWinner();
      if (playerWon == "DRAW"){
        this.outcome = "Game is a DRAW" ;
      }
      else{
        this.outcome = "Player " + playerWon + " won the game!" ;
      }

    }
  }

  calculateWinner() {
    const lines = [
      [0, 1, 2],
      [3, 4, 5],
      [6, 7, 8],
      [0, 3, 6],
      [1, 4, 7],
      [2, 5, 8],
      [0, 4, 8],
      [2, 4, 6]
    ];

    for (let i = 0; i < lines.length; i++) {
      const [a, b, c] = lines[i];
      if (
        this.squares[a] &&
        this.squares[a] === this.squares[b] &&
        this.squares[a] === this.squares[c]
      ) {
        this.isGameOver = true;
        return this.squares[a];
      }
    }

    if (!this.squares.includes('')){
      this.isGameOver = true;
      return 'DRAW';
    }

    return '';
  }
}
