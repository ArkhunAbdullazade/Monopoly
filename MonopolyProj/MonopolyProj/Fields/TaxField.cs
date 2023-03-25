using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class TaxField : IField
    {
        public string Name { get; private set; }
        public FIELD_TYPES FieldType { get; set; } = FIELD_TYPES.tax;

        public double Tax { get; private set; }

        private Game game;

        public TaxField(string name, Game game, double tax)
        {
            Tax = tax;
            this.game = game;
            Name = name;
        }

        public void OnEnter(Player player)
        {
            player.PayMoney(Tax);
        }
    }
}
