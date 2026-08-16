using Yedili.Enums;
using Yedili.Models;

namespace Yedili.Game;

public class GameRules
{
    public bool IsValidMove(Card card, Board board)
    {
        var suitCards = board.Cards[card.Suit];

        // 7, kendi türünün başlangıç kartıdır.
        if (card.Rank == Rank.Seven)
        {
            return suitCards.Count == 0;
        }

        // Önce o türün 7'si açılmış olmalı.
        if (suitCards.Count == 0)
        {
            return false;
        }

        int cardRank = GetRankValue(card.Rank);

        int lowestRank = suitCards
            .Min(c => GetRankValue(c.Rank));

        int highestRank = suitCards
            .Max(c => GetRankValue(c.Rank));

        // 7'nin alt tarafına açılan kart
        if (cardRank == lowestRank - 1)
        {
            return true;
        }

        // 7'nin üst tarafına açılan kart
        if (cardRank == highestRank + 1)
        {
            return true;
        }

        return false;
    }

    public bool CanPass(Player player, Board board)
    {
        return !player.Hand.Any(
            card => IsValidMove(card, board));
    }

    public bool IsGameOver(IEnumerable<Player> players)
    {
        return players.Any(
            player => player.Hand.Count == 0);
    }

    public Player? GetWinner(IEnumerable<Player> players)
    {
        return players.FirstOrDefault(
            player => player.Hand.Count == 0);
    }

    private int GetRankValue(Rank rank)
    {
        return rank switch
        {
            Rank.Ace => 1,
            Rank.Two => 2,
            Rank.Three => 3,
            Rank.Four => 4,
            Rank.Five => 5,
            Rank.Six => 6,
            Rank.Seven => 7,
            Rank.Eight => 8,
            Rank.Nine => 9,
            Rank.Ten => 10,
            Rank.Jack => 11,
            Rank.Queen => 12,
            Rank.King => 13,
            _ => 0
        };
    }
}