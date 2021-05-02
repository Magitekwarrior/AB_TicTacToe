import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { HttpParams } from "@angular/common/http";

import {  throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

import { GameModel } from './models/game.model';
import { MoveModel } from './models/playermove.model';

@Injectable()
export class GameService {

  baseURL: string = "http://localhost:5001/api/Game/";

  playerOne: string = 'USER';
  playerTwo: string = 'CPU';
  gameId: string = '';

  constructor(private httpClient: HttpClient) {

  }

  // post new game board from rest api
  postNewGame(): Observable<GameModel> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify({ playerName: this.playerOne});

    console.log('getNewGame '+ this.baseURL + 'start' + '| params: {' + body + '}')

    return this.httpClient.post<GameModel>(this.baseURL + 'start', body, {'headers':headers} );
  }

  // get games played previously from rest api
  getGameHistory(): Observable<GameModel[]> {
    console.log('getGameHistory '+ this.baseURL + 'history' + '| params: {' + this.playerOne + '}')
    return this.httpClient.get<GameModel[]>(this.baseURL + 'history/' + this.playerOne );
  }

  // post to rest api to save player's move in current game and get next cpu move
  postPlayNextMove(move: MoveModel): Observable<MoveModel> {
    const headers = { 'content-type': 'application/json'}
    const body=JSON.stringify(move);

    var postUrl = `${this.baseURL}${move.gameId}​/play-move`;

    console.log('postPlayNextMove: ' + postUrl)
    console.log('body:' + body)

    return this.httpClient.post<MoveModel>(postUrl, body, {'headers':headers} );
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
