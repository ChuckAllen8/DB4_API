using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CardSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardSite.Controllers
{
    public class CardController : Controller
    {
        private const string API_BASE = "https://deckofcardsapi.com";

        public async Task<IActionResult> Index(string deck_id = "")
        {
            if(deck_id == "")
            {
                deck_id = await MakeDeck();
            }

            Hand model = JsonSerializer.Deserialize<Hand>((string)await Draw(deck_id));

            if(model.remaining < 5)
            {
                ViewData["Message"] = "Cards will be shuffled prior to next draw.";
                await Shuffle(model.deck_id);
            }
            return View(model);
        }

        public async Task<string> Draw(string deck_id)
        {
            HttpClient client = SetAPI();
            var response = await client.GetAsync($"api/deck/{deck_id}/draw/?count=5");
            var hand = await response.Content.ReadAsStringAsync();;
            return hand;
        }

        public async Task Shuffle(string deck_id)
        {
            HttpClient client = SetAPI();
            var response = await client.GetAsync($"api/deck/{deck_id}/shuffle/");
        }

        public async Task<string> MakeDeck()
        {
            HttpClient client = SetAPI();
            var response = await client.GetAsync("api/deck/new/shuffle/?deck_count=1");
            var deckRaw = await response.Content.ReadAsStringAsync();
            Deck deck = JsonSerializer.Deserialize<Deck>(deckRaw);
            return deck.deck_id;
        }

        public HttpClient SetAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API_BASE);
            return client;
        }
    }
}
