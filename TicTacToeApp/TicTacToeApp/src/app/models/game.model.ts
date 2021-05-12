export class GameModel {

  id: string = '';
  player1Name: string = '';
  player2Name: string = '';
  status: string = ''; // 'Incomplete', 'Win', 'Draw'
  winner: string = ''; // '[Player1Name]'
  gameStartDate: Date = new Date();
  cell1: string = '';
  cell2: string = '';
  cell3: string = '';
  cell4: string = '';
  cell5: string = '';
  cell6: string = '';
  cell7: string = '';
  cell8: string = '';
  cell9: string = '';
}
