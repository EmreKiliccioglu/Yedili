namespace Yedili.Core.Model;

public class GameState
{
    public List<Player> Players { get; } = new();

    public Board Board { get; }

    public int CurrentPlayerIndex { get; set; }

    public bool IsGameOver { get; set; }

    public GameState()
    {
        Board = new Board();
        CurrentPlayerIndex = 0;
        IsGameOver = false;
    }

    public Player CurrentPlayer => Players[CurrentPlayerIndex];
}