using Yedili.Core.Enums;
using Yedili.Core.Model;

namespace Yedili.Core.Model;

public class Deck
{
    private readonly List<Card> _cards = new();

    public IReadOnlyList<Card> Cards => _cards;

    public Deck()
    {
        CreateDeck();
    }

    private void CreateDeck()
    {
        _cards.Clear();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        var random = Random.Shared;

        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }
}