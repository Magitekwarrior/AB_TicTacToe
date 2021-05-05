namespace TicTacToeAPI.Infrastructure.Models
{
  public class NextMove
  {
    public int Cell { get; set; }
    public string Value { get; set; }
    public bool GameCompleted { get; set; }
    public string Winner { get; set; }
  }
}
