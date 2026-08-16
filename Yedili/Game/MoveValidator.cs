using Yedili.Models;

namespace Yedili.Game;

public class MoveValidator
{
    private readonly GameRules _rules;

    public MoveValidator(GameRules rules)
    {
        _rules = rules;
    }

    public bool IsValidMove(Card card, Player player, Board board)
    {
        if (!player.Hand.Contains(card))
        {
            return false;
        }

        return _rules.IsValidMove(card, board);
    }
}