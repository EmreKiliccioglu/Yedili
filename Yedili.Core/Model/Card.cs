using System;
using System.Collections.Generic;
using System.Text;

using Yedili.Core.Enums;

namespace Yedili.Core.Model;

public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
}
