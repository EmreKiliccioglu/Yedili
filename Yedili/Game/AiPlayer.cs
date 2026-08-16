using Yedili.Models;

namespace Yedili.Game;

public class AiPlayer
{
    private readonly SevensGame _game;

    public AiPlayer(SevensGame game)
    {
        _game = game;
    }

    public Card? ChooseCard(Player player)
    {
        List<Card> validMoves = player.Hand
            .Where(card =>
                _game.MoveValidator.IsValidMove(
                    card,
                    player,
                    _game.State.Board))
            .ToList();

        if (validMoves.Count == 0)
        {
            return null;
        }

        // İlk aşamada basit AI:
        // Geçerli kartlardan rastgele birini seç.
        int index = Random.Shared.Next(validMoves.Count);

        return validMoves[index];
    }
}