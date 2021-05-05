using System.Runtime.Serialization;

namespace TicTacToeAPI.Infrastructure.Models.enums
{
  public enum GamePlayer
  {
    [EnumMember(Value = "Human")]
    Human,
    [EnumMember(Value = "Cpu")]
    Cpu
  }
}
