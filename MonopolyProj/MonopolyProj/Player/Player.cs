using MonopolyProj.Cards;
using MonopolyProj.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonopolyProj.GameClass;
using System.Runtime.CompilerServices;
using MonopolyProj.Exceptions;

namespace MonopolyProj
{
    public class Player
    {
        public double Money { get; private set; }
        public string NickName { get; private set; } = "Unknown";
        public bool InPrison { get; set; } = false;
        public bool HasGetOutOfPrisonCard { get; set; }

        private List<BuyableField> ownerShip = new List<BuyableField>();
        public List<BuyableField> OwnerShip { get { return ownerShip; } }

        private List<Card> cards = new List<Card>();
        public List<Card> Cards { get { return cards; } }

        public Player(string nickName)
        {
            NickName = nickName;
            Money = 1500;
        }
        public void AddToOwnerShip(BuyableField field) => ownerShip.Add(field);
        public void RemoveFromOwnerShip(BuyableField fieldToRemove)
        {
            int indexToRemove = 0;
            for (int i = 0; i < OwnerShip.Count(); i++)
            {
                if (fieldToRemove.Name == OwnerShip[i].Name)
                    indexToRemove = i;
            }
            ownerShip.RemoveAt(indexToRemove);
        }

        public void PayMoney(double amount)
        {
            if (Money - amount <= 0)
            {
                throw new BankruptException();
            }
            Money -= amount;
        }

        public void EarnMoney(double amount) => Money += amount;

        public void AddCard(Card cardToAdd) => cards.Add(cardToAdd);

        public void RemoveCard(Card card) => cards.Remove(card);

    }
}
