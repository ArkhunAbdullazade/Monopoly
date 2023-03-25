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
    public class CommunityChestField : CardField
    {
        public string Name { get; private set; }
        public FIELD_TYPES FieldType { get; set; } = FIELD_TYPES.communityChest;

        private Game game;

        public CommunityChestField(string name, Game game) : base(name, FIELD_TYPES.communityChest)
        {
            this.game = game;
            Name = name;
        }

        public override void OnEnter(Player player)
        {
            Card card = GetCard(game.CommunityChest.ToArray());
            card.UseCard(player);
            game.SetLastCardText(player, card.Description);
        }
    }
}
