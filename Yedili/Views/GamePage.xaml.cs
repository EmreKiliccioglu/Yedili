using Yedili.Enums;
using Yedili.Game;
using Yedili.Models;

namespace Yedili.Views;

public partial class GamePage : ContentPage
{
    private readonly SevensGame _game;

    public GamePage()
    {
        InitializeComponent();

        _game = new SevensGame();

        StartNewGame();
    }

    private async void StartNewGame()
    {
        _game.StartGame();

        UpdateScreen();

        await PlayAiTurns();
    }

    private void UpdateScreen()
    {
        CurrentPlayerLabel.Text =
            $"Sıra: {_game.State.CurrentPlayer.Name}";

        UpdatePlayers();

        UpdateBoard();

        UpdatePlayerHand();
    }

    private void UpdatePlayers()
    {
        var players = _game.State.Players;

        TopPlayerLabel.Text =
            $"{players[2].Name} - {players[2].Hand.Count} kart";

        LeftPlayerLabel.Text =
            $"{players[1].Name}\n{players[1].Hand.Count} kart";

        RightPlayerLabel.Text =
            $"{players[3].Name}\n{players[3].Hand.Count} kart";
    }

    private void UpdateBoard()
    {
        BoardCardsLayout.Children.Clear();

        bool hasCards = _game.State.Board.Cards
            .SelectMany(pair => pair.Value)
            .Any();

        EmptyBoardLabel.IsVisible = !hasCards;

        if (!hasCards)
        {
            return;
        }

        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            var suitCards = _game.State.Board.Cards[suit]
                .OrderBy(card => GetRankOrder(card.Rank))
                .ToList();

            if (suitCards.Count == 0)
            {
                continue;
            }

            var row = new HorizontalStackLayout
            {
                Spacing = -20,
                HorizontalOptions = LayoutOptions.Center
            };

            foreach (Card card in suitCards)
            {
                var image = new Image
                {
                    Source = GetCardImage(card),
                    WidthRequest = 55,
                    HeightRequest = 77,
                    Aspect = Aspect.AspectFit
                };

                row.Children.Add(image);
            }

            BoardCardsLayout.Children.Add(row);
        }
    }

    private void UpdatePlayerHand()
    {
        PlayerHandLayout.Children.Clear();

        Player player = _game.State.Players[0];

        List<Card> validMoves = _game.GetValidMoves();

        var sortedHand = player.Hand
            .OrderBy(card => GetSuitOrder(card.Suit))
            .ThenBy(card => GetRankOrder(card.Rank))
            .ToList();

        foreach (Card card in sortedHand)
        {
            ImageButton button = CreateCardButton(
            card,
            validMoves.Contains(card));

            PlayerHandLayout.Children.Add(button);
        }

        PassButton.IsVisible =
            _game.State.CurrentPlayer.Type == PlayerType.Human &&
            _game.CanCurrentPlayerPass();
    }

    private ImageButton CreateCardButton(
    Card card,
    bool isValid)
    {
        var button = new ImageButton
        {
            Source = GetCardImage(card),

            WidthRequest = 90,
            HeightRequest = 126,

            Padding = 0,

            Aspect = Aspect.AspectFit,

            BackgroundColor = Colors.Transparent,

            // Bütün kartlar normal görünür.
            Opacity = 1.0,

            // Oynanabilir kart biraz yukarı çıkar.
            TranslationY = isValid ? -15 : 0
        };

        button.IsEnabled = isValid;

        button.Clicked += (_, _) =>
        {
            PlayCard(card);
        };

        return button;
    }

    private async void PlayCard(Card card)
    {
        bool success = _game.PlayCard(card);

        if (!success)
        {
            return;
        }

        UpdateScreen();

        await PlayAiTurns();
    }

    private async Task PlayAiTurns()
    {
        while (
            _game.State.IsGameStarted &&
            !_game.State.IsGameOver &&
            _game.State.CurrentPlayer.Type == PlayerType.AI)
        {
            await Task.Delay(700);

            _game.PlayAiTurn();

            UpdateScreen();
        }
    }

    private string GetCardText(Card card)
    {
        string suit = card.Suit switch
        {
            Suit.Clubs => "♣",
            Suit.Diamonds => "♦",
            Suit.Hearts => "♥",
            Suit.Spades => "♠",
            _ => ""
        };

        string rank = card.Rank switch
        {
            Rank.Ace => "A",
            Rank.Two => "2",
            Rank.Three => "3",
            Rank.Four => "4",
            Rank.Five => "5",
            Rank.Six => "6",
            Rank.Seven => "7",
            Rank.Eight => "8",
            Rank.Nine => "9",
            Rank.Ten => "10",
            Rank.Jack => "J",
            Rank.Queen => "Q",
            Rank.King => "K",
            _ => ""
        };

        return $"{rank}{suit}";
    }

    private int GetSuitOrder(Suit suit)
    {
        return suit switch
        {
            Suit.Clubs => 0,
            Suit.Diamonds => 1,
            Suit.Hearts => 2,
            Suit.Spades => 3,
            _ => 99
        };
    }

    private int GetRankOrder(Rank rank)
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
            _ => 99
        };
    }

    private async void OnPassClicked(
    object? sender,
    EventArgs e)
    {
        if (!_game.CanCurrentPlayerPass())
        {
            return;
        }

        _game.PassTurn();

        UpdateScreen();

        await PlayAiTurns();
    }

    private string GetCardImage(Card card)
    {
        string suit = card.Suit switch
        {
            Suit.Clubs => "clubs",
            Suit.Diamonds => "diamonds",
            Suit.Hearts => "hearts",
            Suit.Spades => "spades",
            _ => throw new ArgumentOutOfRangeException()
        };

        string rank = card.Rank switch
        {
            Rank.Ace => "a",
            Rank.Two => "2",
            Rank.Three => "3",
            Rank.Four => "4",
            Rank.Five => "5",
            Rank.Six => "6",
            Rank.Seven => "7",
            Rank.Eight => "8",
            Rank.Nine => "9",
            Rank.Ten => "10",
            Rank.Jack => "j",
            Rank.Queen => "q",
            Rank.King => "k",
            _ => throw new ArgumentOutOfRangeException()
        };

        return $"card{suit}{rank}.png";
    }
}