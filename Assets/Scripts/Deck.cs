using System;
using System.Collections.Generic;

public sealed class Deck
{
    private static readonly Lazy<Deck> Lazy =
        new Lazy<Deck>(() => new Deck());

    public static Deck Instance => Lazy.Value;

    public List<int> deck = new List<int>();
    public int deckIndex;
    
    private void GenerateDeck()
    {
        for (var cardValue = 1; cardValue <= 13; cardValue++)
        {
            for (var cardCount = 0; cardCount < 4; cardCount++)
            {
                deck.Add(cardValue);
            }
        }
    }
    
    private void ShuttleDeck()
    {
        for (var i = 0; i < 52; i++)
        {
            var swapIndex = new Random().Next(52);
            (deck[i], deck[swapIndex]) = (deck[swapIndex], deck[i]);
        }
    }
}
