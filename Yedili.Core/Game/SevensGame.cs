using Yedili.Core.Model;

namespace Yedili.Core.Game;

public class SevensGame
{
    public GameState State { get; }

    public Deck Deck { get; }

    public SevensGame()
    {
        State = new GameState();
        Deck = new Deck();
    }

    public void Start()
    {
        CreatePlayers();

        Deck.Shuffle();

        DealCards();
    }

    private void CreatePlayers()
    {
        State.Players.Clear();

        State.Players.Add(
            new Player(1, "Player", true));

        State.Players.Add(
            new Player(2, "AI 1", false));

        State.Players.Add(
            new Player(3, "AI 2", false));

        State.Players.Add(
            new Player(4, "AI 3", false));
    }

    private void DealCards()
    {
        int playerIndex = 0;

        foreach (var card in Deck.Cards)
        {
            State.Players[playerIndex].Hand.Add(card);

            playerIndex++;

            if (playerIndex >= State.Players.Count)
                playerIndex = 0;
        }
    }
}