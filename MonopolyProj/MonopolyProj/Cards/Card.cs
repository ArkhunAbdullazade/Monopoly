using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public class Card : CardData, ICardUsable
    {
        public virtual void UseCard(Player players) { }

        public Card(string description)
        {
            Description = description;
        }
    }
}
