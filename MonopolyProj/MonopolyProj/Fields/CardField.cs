using MonopolyProj.Cards;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class CardField : Field
    {

        public CardField(string? name, FIELD_TYPES fieldType) : base(name, fieldType) { }

        public static Card GetCard(Card[] cards)
        {
            Random random = new Random(DateTime.Now.Millisecond.GetHashCode());
            return cards[random.Next(0, cards.Length - 1)];
        }
    }
}
