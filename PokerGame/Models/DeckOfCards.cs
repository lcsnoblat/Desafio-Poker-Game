using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Models
{
    public class DeckOfCards : Card
    {
        const int NumberOfCards = 52;
        private Card[] Deck;

        public DeckOfCards()
        {
            Deck = new Card[NumberOfCards];
        }

        public Card[] getDeck { get { return Deck; } }

        public void StartNewDeck()
        {
            int i = 0;
            foreach(Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Face v in Enum.GetValues(typeof(Face)))
                {
                    Deck[i] = new Card { MySuit = s, MyFace = v };
                    i++;
                }
            }
            ShuffleCards();
        }

        private void ShuffleCards()
        {
            Random randomizer = new Random();
            Card tempCard;

            for (int shuffleTimes = 0; shuffleTimes < 500; shuffleTimes++)
            {
                for (int i = 0; i < NumberOfCards; i++)
                {
                    int secondCardIndex = randomizer.Next(13);
                    tempCard = Deck[i];
                    Deck[i] = Deck[secondCardIndex];
                    Deck[secondCardIndex] = tempCard;
                }
            }
        }
    }
}