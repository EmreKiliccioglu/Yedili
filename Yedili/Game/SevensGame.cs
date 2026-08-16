using Yedili.Enums;
using Yedili.Models;

namespace Yedili.Game;

public class SevensGame
{
    public Deck Deck { get; }
    public GameState State { get; }

    public GameRules Rules { get; }
    public MoveValidator MoveValidator { get; }

    public AiPlayer AiPlayer { get; }

    public SevensGame()
    {
        Deck = new Deck();
        State = new GameState();

        Rules = new GameRules();
        MoveValidator = new MoveValidator(Rules);

        AiPlayer = new AiPlayer(this);
    }

    public void StartGame()
    {
        ResetGame();

        Deck.Reset();
        Deck.Shuffle();

        CreatePlayers();
        DealCards();

        SetStartingPlayer();

        State.IsGameStarted = true;
        State.IsGameOver = false;
    }

    private void ResetGame()
    {
        foreach (Player player in State.Players)
        {
            player.Hand.Clear();
        }

        State.Players.Clear();

        foreach (List<Card> cards in State.Board.Cards.Values)
        {
            cards.Clear();
        }

        State.CurrentPlayerIndex = 0;
        State.IsGameStarted = false;
        State.IsGameOver = false;
        State.Winner = null;
    }

    private void CreatePlayers()
    {
        State.Players.Add(
            new Player(1, "Sen", PlayerType.Human));

        State.Players.Add(
            new Player(2, "AI 1", PlayerType.AI));

        State.Players.Add(
            new Player(3, "AI 2", PlayerType.AI));

        State.Players.Add(
            new Player(4, "AI 3", PlayerType.AI));
    }

    private void DealCards()
    {
        int playerIndex = 0;

        foreach (Card card in Deck.Cards)
        {
            State.Players[playerIndex].Hand.Add(card);

            playerIndex++;

            if (playerIndex >= State.Players.Count)
            {
                playerIndex = 0;
            }
        }
    }

    private void SetStartingPlayer()
    {
        for (int i = 0; i < State.Players.Count; i++)
        {
            Player player = State.Players[i];

            bool hasSeven = player.Hand.Any(
                card => card.Rank == Rank.Seven);

            if (hasSeven)
            {
                State.CurrentPlayerIndex = i;
                return;
            }
        }
    }

    public bool PlayCard(Card card)
    {
        if (!State.IsGameStarted || State.IsGameOver)
        {
            return false;
        }

        Player currentPlayer = State.CurrentPlayer;

        if (!MoveValidator.IsValidMove(
                card,
                currentPlayer,
                State.Board))
        {
            return false;
        }

        currentPlayer.Hand.Remove(card);

        State.Board.AddCard(card);

        CheckGameOver();

        if (!State.IsGameOver)
        {
            NextPlayer();
        }

        return true;
    }

    private void NextPlayer()
    {
        State.CurrentPlayerIndex++;

        if (State.CurrentPlayerIndex >= State.Players.Count)
        {
            State.CurrentPlayerIndex = 0;
        }
    }

    private void CheckGameOver()
    {
        Player? winner = Rules.GetWinner(State.Players);

        if (winner == null)
        {
            return;
        }

        State.Winner = winner;
        State.IsGameOver = true;
    }

    public List<Card> GetValidMoves()
    {
        if (!State.IsGameStarted || State.IsGameOver)
        {
            return new List<Card>();
        }

        Player currentPlayer = State.CurrentPlayer;

        return currentPlayer.Hand
            .Where(card =>
                MoveValidator.IsValidMove(
                    card,
                    currentPlayer,
                    State.Board))
            .ToList();
    }

    public bool CanCurrentPlayerPass()
    {
        if (!State.IsGameStarted || State.IsGameOver)
        {
            return false;
        }

        return Rules.CanPass(
            State.CurrentPlayer,
            State.Board);
    }

    public bool PlayAiTurn()
    {
        if (!State.IsGameStarted || State.IsGameOver)
        {
            return false;
        }

        Player currentPlayer = State.CurrentPlayer;

        if (currentPlayer.Type != Enums.PlayerType.AI)
        {
            return false;
        }

        Card? selectedCard = AiPlayer.ChooseCard(currentPlayer);

        if (selectedCard == null)
        {
            NextPlayer();
            return false;
        }

        return PlayCard(selectedCard);
    }

    public bool PassTurn()
    {
        if (!State.IsGameStarted || State.IsGameOver)
        {
            return false;
        }

        if (!CanCurrentPlayerPass())
        {
            return false;
        }

        NextPlayer();

        return true;
    }
}