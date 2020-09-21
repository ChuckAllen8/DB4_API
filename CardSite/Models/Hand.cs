using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSite.Models
{
    public class Hand
    {
        public bool success { get; set; }
        public Card[] cards { get; set; }
        public string deck_id { get; set; }
        public int remaining { get; set; }
    }
}
