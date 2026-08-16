using Yedili.Enums;

namespace Yedili.Models;

public class Board
{
    private readonly Dictionary<Suit, List<Card>> _cards = new();

    public IReadOnlyDictionary<Suit, List<Card>> Cards => _cards;

    public Board()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            _cards[suit] = new List<Card>();
        }
    }

    public void AddCard(Card card)
    {
        _cards[card.Suit].Add(card);
    }
}