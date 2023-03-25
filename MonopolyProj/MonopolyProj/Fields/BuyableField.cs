using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class BuyableField : Field, IBuyable
    {
        public double Cost { get; private set; }
        public Player? Owner { get; set; } = null;
        public double RentToPay
        {
            get => Cost;
        }

        public BuyableField(string? name, FIELD_TYPES fieldType, double cost) : base(name, fieldType)
        {
            Cost = cost;
        }

        public void Buy(Player player)
        {
            if (Owner != null)
                throw new InvalidOperationException($"This place is already owned by Player {Owner.NickName}");

            player.PayMoney(Cost);
            player.AddToOwnerShip(this);
            Owner = player;
        }

        public virtual void ExchangeField(Player owner, Player buyer, double negotiationPrice)
        {
            if (owner.NickName == buyer.NickName)
                throw new ArgumentException("You can't Exchange with yourself");
            if (owner.NickName != Owner.NickName)
                throw new ArgumentException($"The Player {owner.NickName} does not own this field");
            buyer.PayMoney(negotiationPrice);
            buyer.AddToOwnerShip(this);
            Owner.EarnMoney(negotiationPrice);
            Owner.RemoveFromOwnerShip(this);
            Owner = buyer;
        }
        private void PayRent(Player player)
        {
            if (Owner != null && Owner.NickName != player.NickName)
            {
                player.PayMoney(RentToPay);
                Owner.EarnMoney(RentToPay);
            }
        }

        public override void OnEnter(Player player)
        {
            PayRent(player);
        }
    }
}
