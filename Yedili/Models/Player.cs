using Yedili.Enums;

namespace Yedili.Models;

public class Player
{
    public int Id { get; }
    public string Name { get; }
    public PlayerType Type { get; }

    public List<Card> Hand { get; } = new();

    public Player(int id, string name, PlayerType type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
}