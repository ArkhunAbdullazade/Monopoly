using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public class GetOutOfPrisonCard : Card
    {
        Game game;
        public override void UseCard(Player players) => game.RemovePlayerFromPrison(players);
        public GetOutOfPrisonCard(string description, Game game) : base(description)
        {
            CardType = CARD_TYPES.GetOutOfPrison;
            this.game = game;
        }
    }
}
