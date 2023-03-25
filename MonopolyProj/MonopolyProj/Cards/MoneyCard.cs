using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public class MoneyCard : Card
    {
        public double Amount { get; private set; }
        public override void UseCard(Player players) => players.EarnMoney(Amount);
        public MoneyCard(string description, double amount) : base(description)
        {
            CardType = CARD_TYPES.money;
            Amount = amount;  
        }
    }
}
