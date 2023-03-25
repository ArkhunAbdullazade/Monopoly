using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public interface IBuyable
    {
        double Cost { get; }
        double RentToPay { get; }
        public Player? Owner { get; }
        void Buy(Player player);
        void ExchangeField(Player owner, Player buyer, double negotiatetprice);

    }
}
