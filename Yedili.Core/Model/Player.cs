using System.Collections.Generic;

namespace Yedili.Core.Model;

public class Player
{
    public int Id { get; }
    public string Name { get; }
    public bool IsHuman { get; }

    public List<Card> Hand { get; } = new();

    public Player(int id, string name, bool isHuman)
    {
        Id = id;
        Name = name;
        IsHuman = isHuman;
    }
}