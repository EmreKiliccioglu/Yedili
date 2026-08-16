namespace Yedili.Models;

public class GameState
{
    public List<Player> Players { get; } = new();

    public Board Board { get; }

    public int CurrentPlayerIndex { get; set; }

    public bool IsGameStarted { get; set; }

    public bool IsGameOver { get; set; }

    public Player? Winner { get; set; }

    public GameState()
    {
        Board = new Board();
        CurrentPlayerIndex = 0;
        IsGameStarted = false;
        IsGameOver = false;
    }

    public Player CurrentPlayer => Players[CurrentPlayerIndex];
}