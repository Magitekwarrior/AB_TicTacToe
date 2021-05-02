import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { HttpParams } from "@angular/common/http";

import {  throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable()
export class GameService {

  baseURL: string = "http://localhost:5001/";

  playerOne: string = "USER";
  playerTwo: string = "CPU"

  constructor(private httpClient: HttpClient) {

  }

  // get new game board from rest api
  getNewGame(): Observable<Game> {
    const params = new HttpParams()
    .set('PlayerName', this.playerOne);

    return this.httpClient.get<Game>(this.baseURL + '/api/Game/start', {params});
  }

  // get games played previously from rest api
  getGameHistory(): Observable<Game[]> {
    const params = new HttpParams()
    .set('PlayerName', this.playerOne);

    console.log('getGameHistory '+ this.baseURL + '/api/Game/history' + '; params: {' + params + '}')
    return this.httpClient.get<Game[]>(this.baseURL + '/api/Game/history', {params});
  }

  // post to rest api to save player's move in current game and get next cpu move
  postPlayNextMove(move: Move): Observable<Move> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(move);
    console.log(body)

    return this.httpClient.post<Move>(this.baseURL + '/api​/Game​/{id}​/play-move', body, {'headers':headers} );
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }

}

interface Game {
  id: string;
  player1Name: string;
  player2Name: string;
  status: string; // 'Incomplete', 'Win', 'Draw'
  winner: string; // '[Player1Name]'
  gameStartDate: Date;
  cell1: string;
  cell2: string;
  cell3: string;
  cell4: string;
  cell5: string;
  cell6: string;
  cell7: string;
  cell8: string;
  cell9: string;
}

interface Move {
  cell: string;
  value: string;
}

