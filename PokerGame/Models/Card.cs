using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Models
{
    public class Card
    {
        public enum Face
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
        public enum Suit
        {
            Club,
            Diamond,
            Heart,
            Spade
        }

        public Suit MySuit { get; set; }
        public Face MyFace { get; set; }
    }
}