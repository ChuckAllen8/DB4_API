using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSite.Models
{
    public class Card
    {
        public string image { get; set; }
        public string suit { get; set; }
        public string value { get; set; }
        public string code { get; set; }
    }
}
