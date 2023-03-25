using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public class GoToPrisonCard : Card
    {
        Game game;
        public override void UseCard(Player players) => game.SetPlayerInPrison(players);
        public GoToPrisonCard(string description, Game game): base(description)
        {
            CardType = CARD_TYPES.GoToPrison;
            this.game = game;
        }
    }
}
