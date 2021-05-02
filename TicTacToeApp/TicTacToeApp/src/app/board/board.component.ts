import { Component, OnInit } from '@angular/core';
import { GameService } from '../game.service';

import { GameModel } from '../models/game.model';
import { MoveModel } from '../models/playermove.model';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss'],
})
export class BoardComponent implements OnInit {
  squares: string[] = [];
  xIsNext: boolean = true;
  outcome: string = '';
  isGameOver: boolean = false;
  startGame: boolean = false; // hidden by default
  activePlayer: string = '';

  currentGameBoard: GameModel = new GameModel();
  nextMove: MoveModel = new MoveModel();
  currentMove: MoveModel = new MoveModel();

  constructor(public gameService: GameService) {}

  ngOnInit() {
    this.newGame();
  }

  newGame() {
    this.squares = Array(9).fill('');
    this.outcome = '';
    this.xIsNext = true;
    this.isGameOver = false;
    this.startGame = true;
    this.activePlayer = 'USER';

    this.gameService.postNewGame().subscribe((data: GameModel) => {
      console.log('game: ', data);
      this.currentGameBoard = data;
    });
  }

  get player() {
    return this.xIsNext ? 'X' : 'O';
  }

  makeMove(idx: number) {
    console.log('gameover?', this.isGameOver)
    if (this.isGameOver) return;

    console.log('idx: ' + idx);
    console.log('squares 1st:', this.squares);

    this.currentMove.gameId = this.currentGameBoard.id;
    this.currentMove.cell = idx + 1;
    this.currentMove.value = 'X';

    // Get Cpu's next move
    this.gameService
      .postPlayNextMove(this.currentMove)
      .subscribe((data: MoveModel) => {
        console.log('postPlayNextMove: ', data);
        this.nextMove = data;

        if (!this.squares[idx]) {
          this.squares.splice(idx, 1, this.player);
          //this.squares[idx] = this.player;
          //this.xIsNext = !this.xIsNext;
        }

        this.calculateWinner();
        console.log('outcome:', this.outcome);
        console.log('GameOver after player?:', this.isGameOver);
        if (this.isGameOver)
          return
        else {
          // CPU makes their move.
          var newIdx = this.nextMove.cell - 1;
          console.log('newIdx', newIdx)

          this.squares[newIdx] = this.nextMove.value;

          this.calculateWinner();
          console.log('GameOver after CPU?:', this.isGameOver);
          console.log('outcome:', this.outcome);
        }

        console.log('squares:', this.squares);

      });
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
      [2, 4, 6],
    ];

    this.outcome = '';

    for (let i = 0; i < lines.length; i++) {
      const [a, b, c] = lines[i];
      if (
        this.squares[a] &&
        this.squares[a] === this.squares[b] &&
        this.squares[a] === this.squares[c]
      ) {
        this.isGameOver = true;
        this.outcome = 'Player ' + this.squares[a] + ' won the game!';
        return
      }
    }

    if (!this.squares.includes('')) {
      this.isGameOver = true;
      this.outcome = 'DRAWN GAME';
      return
    }
  }
}
