using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{

    public class CardDataStorage
    {
        public List<CardData> chanceCards { get; set; }
        public List<CardData> comunityCards { get; set; }
        public List<List<int>> cardNumerals { get; set; }
    }
}
