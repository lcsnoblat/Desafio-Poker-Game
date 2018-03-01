using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Models
{
    public enum Hand
    {
        Nothing,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush
    }

    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }
    public class HandRanker : Card
    {
        public int heartsSum;
        public int diamondSum;
        public int clubSum;
        public int spadesSum;
        public Card[] cards;
        public HandValue handValue;

        public HandRanker(Card[] sortedHand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = new Card[5];
            Cards = sortedHand;
            handValue = new HandValue();
        }

        public HandValue HandValues
        {
            get { return handValue; }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
            }
        }

        public Hand EvaluateHand()
        {
            
            getNumberOfSuit();
            if (FourOfKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeOfKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;

            handValue.HighCard = (int)cards[4].MySuit;
            return Hand.Nothing;
        }

        private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                if (element.MySuit == Card.Suit.Heart)
                    heartsSum++;
                else if (element.MySuit == Card.Suit.Diamond)
                    diamondSum++;
                else if (element.MySuit == Card.Suit.Club)
                    clubSum++;
                else if (element.MySuit == Card.Suit.Spade)
                    spadesSum++;
            }
        }

        private bool RoyalFlush()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if ((int)cards[i].MySuit < 10)
                {
                    return false;
                }
            }
            if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
            {
                if (cards[0].MySuit + 1 == cards[1].MySuit &&
                    cards[1].MySuit + 1 == cards[2].MySuit &&
                    cards[2].MySuit + 1 == cards[3].MySuit &&
                    cards[3].MySuit + 1 == cards[4].MySuit)
                {
                    handValue.Total = (int)cards[4].MySuit;
                    return true;
                }
            }
            return false;
        }

        private bool StraightFlush()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if ((int)cards[i].MySuit > 10)
                {
                    return false;
                }
            }

            if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
            {
                if (cards[0].MySuit + 1 == cards[1].MySuit &&
                    cards[1].MySuit + 1 == cards[2].MySuit &&
                    cards[2].MySuit + 1 == cards[3].MySuit &&
                    cards[3].MySuit + 1 == cards[4].MySuit)
                {
                    handValue.Total = (int)cards[4].MySuit;
                    return true;
                }
            }
            return false;
        }

        private bool FourOfKind()
        {
            if (cards[0].MySuit == cards[1].MySuit && cards[0].MySuit == cards[2].MySuit && cards[0].MySuit == cards[3].MySuit)
            {
                handValue.Total = (int)cards[1].MySuit * 4;
                handValue.HighCard = (int)cards[4].MySuit;
                return true;
            }
            else if (cards[1].MySuit == cards[2].MySuit && cards[1].MySuit == cards[3].MySuit && cards[1].MySuit == cards[4].MySuit)
            {
                handValue.Total = (int)cards[1].MySuit * 4;
                handValue.HighCard = (int)cards[0].MySuit;
                return true;
            }

            return false;
        }

        private bool FullHouse()
        {
            if ((cards[0].MySuit == cards[1].MySuit && cards[0].MySuit == cards[2].MySuit && cards[3].MySuit == cards[4].MySuit) ||
                (cards[0].MySuit == cards[1].MySuit && cards[2].MySuit == cards[3].MySuit && cards[2].MySuit == cards[4].MySuit))
            {
                handValue.Total = (int)(cards[0].MySuit) + (int)(cards[1].MySuit) + (int)(cards[2].MySuit) +
                    (int)(cards[3].MySuit) + (int)(cards[4].MySuit);
                return true;
            }

            return false;
        }

        private bool Flush()
        {
            if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
            {
                handValue.Total = (int)cards[4].MySuit;
                return true;
            }

            return false;
        }

        private bool Straight()
        {
            if (cards[0].MySuit + 1 == cards[1].MySuit &&
                cards[1].MySuit + 1 == cards[2].MySuit &&
                cards[2].MySuit + 1 == cards[3].MySuit &&
                cards[3].MySuit + 1 == cards[4].MySuit)
            {
                handValue.Total = (int)cards[4].MySuit;
                return true;
            }

            return false;
        }

        private bool ThreeOfKind()
        {
            if ((cards[0].MySuit == cards[1].MySuit && cards[0].MySuit == cards[2].MySuit) ||
            (cards[1].MySuit == cards[2].MySuit && cards[1].MySuit == cards[3].MySuit))
            {
                handValue.Total = (int)cards[2].MySuit * 3;
                handValue.HighCard = (int)cards[4].MySuit;
                return true;
            }
            else if (cards[2].MySuit == cards[3].MySuit && cards[2].MySuit == cards[4].MySuit)
            {
                handValue.Total = (int)cards[2].MySuit * 3;
                handValue.HighCard = (int)cards[1].MySuit;
                return true;
            }
            return false;
        }

        private bool TwoPairs()
        {
            if (cards[0].MySuit == cards[1].MySuit && cards[2].MySuit == cards[3].MySuit)
            {
                handValue.Total = ((int)cards[1].MySuit * 2) + ((int)cards[3].MySuit * 2);
                handValue.HighCard = (int)cards[4].MySuit;
                return true;
            }
            else if (cards[0].MySuit == cards[1].MySuit && cards[3].MySuit == cards[4].MySuit)
            {
                handValue.Total = ((int)cards[1].MySuit * 2) + ((int)cards[3].MySuit * 2);
                handValue.HighCard = (int)cards[2].MySuit;
                return true;
            }
            else if (cards[1].MySuit == cards[2].MySuit && cards[3].MySuit == cards[4].MySuit)
            {
                handValue.Total = ((int)cards[1].MySuit * 2) + ((int)cards[3].MySuit * 2);
                handValue.HighCard = (int)cards[0].MySuit;
                return true;
            }
            return false;
        }

        private bool OnePair()
        {
            if (cards[0].MySuit == cards[1].MySuit)
            {
                handValue.Total = (int)cards[0].MySuit * 2;
                handValue.HighCard = (int)cards[4].MySuit;
                return true;
            }
            else if (cards[1].MySuit == cards[2].MySuit)
            {
                handValue.Total = (int)cards[1].MySuit * 2;
                handValue.HighCard = (int)cards[4].MySuit;
                return true;
            }
            else if (cards[2].MySuit == cards[3].MySuit)
            {
                handValue.Total = (int)cards[2].MySuit * 2;
                handValue.HighCard = (int)cards[4].MySuit;
                return true;
            }
            else if (cards[3].MySuit == cards[4].MySuit)
            {
                handValue.Total = (int)cards[3].MySuit * 2;
                handValue.HighCard = (int)cards[2].MySuit;
                return true;
            }

            return false;
        }
    }
}