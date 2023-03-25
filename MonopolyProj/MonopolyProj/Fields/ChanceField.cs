using MonopolyProj.Cards;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class ChanceField : CardField
    {
        Game game;
        public string Name { get; private set; }
        public FIELD_TYPES FieldType { get; set; } = FIELD_TYPES.chance;


        public ChanceField(string name, Game game) : base(name, FIELD_TYPES.chance)
        {
            this.game = game;
            Name = name;
        }

        public override void OnEnter(Player player)
        {
            Card card = GetCard(game.Chance.ToArray());
            card.UseCard(player);
            game.SetLastCardText(player, card.Description);
        }
    }
}
