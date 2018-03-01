using PokerGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokerGame.Controllers
{
    public class DealCardsController : Controller
    {
        // GET: DealCards
        public ActionResult PokerGame()
        {
            var dealer = new DealCards();
            dealer.Deal();
            ViewBag.playerCards = dealer.sortedPlayerCards;
            ViewBag.computerCards = dealer.sortedComputerCards;
            ViewBag.Winner = dealer.whoWins;

            var playerRanker = new HandRanker(ViewBag.playerCards);
            var computerRanker = new HandRanker(ViewBag.computerCards);
            Hand playerHand = playerRanker.EvaluateHand();
            Hand computerHand = computerRanker.EvaluateHand();

            ViewBag.playerHand = playerHand;
            ViewBag.computerHand = computerHand;

            return View();
        }
    }
} 