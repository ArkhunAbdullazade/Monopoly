using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonopolyProj.Fields
{
    public class StreetField : BuyableField
    {
        public string Name { get; private set; }
        public override FIELD_TYPES FieldType { get; set; }
        public bool HasHouse { get; private set; } = false;
        public double HouseCost { get; private set; } = 100;
        public double RentToPay
        {
            get => HasHouse ? Cost + (HouseCost) : Cost;
        }

        public StreetField(string name, double cost) : base(name, FIELD_TYPES.street, cost) {}

        public void BuyHouse(Player player)
        {
            if (Owner == null) throw new InvalidOperationException($"The Street is not bought to build a house");

            if (Owner.NickName == player.NickName)
            {
                player.PayMoney(HouseCost);
                HasHouse = true;
            }
            else throw new InvalidOperationException($"The Street is already owned by Player {Owner.NickName}");
        }
        public override void ExchangeField(Player owner, Player buyer, double negotiationPrice)
        {
            if (HasHouse)
                throw new InvalidOperationException("You have to sell all your houses before its possible to exchange with other players");
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
    }
}