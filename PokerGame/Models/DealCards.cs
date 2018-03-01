using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Models
{
    public class DealCards : DeckOfCards
    {
        public Card[] playerCards;
        public Card[] computerCards;
        public Card[] sortedPlayerCards;
        public Card[] sortedComputerCards;
        public String whoWins = "";

        public DealCards()
        {
            playerCards = new Card[5];
            sortedPlayerCards = new Card[5];
            computerCards = new Card[5];
            sortedComputerCards = new Card[5];
        }

        public void Deal()
        {
            StartNewDeck(); 
            getHand();
            sortCards();
            evaluateHands();
        }

        public void getHand()
        {
            for (int i = 0; i < 5; i++)
                playerCards[i] = getDeck[i];

            for (int i = 5; i < 10; i++)
                computerCards[i - 5] = getDeck[i];
        }

        public void sortCards()
        {
            var queryPlayer = from hand in playerCards
                              orderby hand.MyFace
                              select hand;

            var queryComputer = from hand in computerCards
                                orderby hand.MyFace
                                select hand;

            var index = 0;
            foreach (var element in queryPlayer.ToList())
            {
                sortedPlayerCards[index] = element;
                index++;
            }

            index = 0;
            foreach (var element in queryComputer.ToList())
            {
                sortedComputerCards[index] = element;
                index++;
            }
        }

        public void evaluateHands()
        {
            HandRanker playerHandEvaluator = new HandRanker(sortedPlayerCards);
            HandRanker computerHandEvaluator = new HandRanker(sortedComputerCards);

            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            Console.WriteLine("\n\n\n\n\nPlayer's Hand: " + playerHand);
            Console.WriteLine("\nComputer's Hand: " + computerHand);

            //evaluate hands
            if (playerHand > computerHand)
            {
                whoWins = "Player Wins!";
            }
            else if (playerHand < computerHand)
            {
                whoWins = "Computer Wins!";
            }
            else 
            {
                if (playerHandEvaluator.HandValues.Total > computerHandEvaluator.HandValues.Total)
                    whoWins = "Player Wins!";
                else if (playerHandEvaluator.HandValues.Total < computerHandEvaluator.HandValues.Total)
                    whoWins = "Computer Wins!";
                else if (playerHandEvaluator.HandValues.HighCard > computerHandEvaluator.HandValues.HighCard)
                    whoWins = "Player Wins!";
                else if (playerHandEvaluator.HandValues.HighCard < computerHandEvaluator.HandValues.HighCard)
                    whoWins = "Computer Wins!";
                else
                    whoWins = "No Winners!";
            }
        }
    }
}