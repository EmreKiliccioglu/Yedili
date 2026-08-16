using Yedili.Core.Enums;

namespace Yedili.Core.Model;

public class Board
{
    private readonly Dictionary<Suit, List<Card>> _cards = new();

    public IReadOnlyDictionary<Suit, List<Card>> Cards => _cards;

    public Board()
    {
        foreach (var suit in Enum.GetValues<Suit>())
        {
            _cards[suit] = new List<Card>();
        }
    }

    public void AddCard(Card card)
    {
        _cards[card.Suit].Add(card);
    }
}