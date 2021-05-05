using System;
using System.Linq;

namespace TicTacToeAPI.Infrastructure.Models
{
  public class Game
  {
    public Game()
    {
      Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public string Player1Name { get; set; }
    public string Player2Name { get; set; }

    public string Status { get; set; } // 'Incomplete', 'Win', 'Draw'
    public string Winner { get; set; } // '[Player1Name]'
    public DateTime GameStartDate { get; set; }

    public string Cell1 { get; set; }
    public string Cell2 { get; set; }
    public string Cell3 { get; set; }
    public string Cell4 { get; set; }
    public string Cell5 { get; set; }
    public string Cell6 { get; set; }
    public string Cell7 { get; set; }
    public string Cell8 { get; set; }
    public string Cell9 { get; set; }

    public bool AnyEmptyCell()
    {
      foreach (var p in GetType().GetProperties().Where(p => p.Name.Contains("Cell")))
      {
        if (p.GetValue(this) == null)
        {
          return true;
        }
      }

      return false;
    }

    public bool CellHasValue(int cell)
    {
      foreach (var p in GetType().GetProperties().Where(p => p.Name.Contains("Cell")))
      {
        if (p.Name.Contains(cell.ToString()) && p.GetValue(this) != null)
        {
          return true;
        }
      }

      return false;
    }

    public void SetCell(int cell, string value)
    {
      foreach (var p in GetType().GetProperties().Where(p => p.Name.Contains("Cell")))
      {
        if (p.Name.Contains(cell.ToString()))
        {
          p.SetValue(this, value);

          break;
        }
      }
    }

    public void GetBoardValues(out string[] gameBoard)
    {
      gameBoard = new string[9];
      foreach (var p in GetType().GetProperties().Where(p => p.Name.Contains("Cell")))
      {
        int cellNo = 0;
        for (int col = 0; col < 9; col++)
        {
          cellNo++;
          if ((p.Name.Contains(cellNo.ToString())))
          {
            gameBoard[col] = p.GetValue(this) as string;
          }
        }
      }
    }

  }
}
