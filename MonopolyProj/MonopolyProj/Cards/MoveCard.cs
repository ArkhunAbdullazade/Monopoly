using MonopolyProj.Extensions;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public class MoveCard : Card
    {
        Game game;
        public int Steps { get; private set; }
        public override void UseCard(Player players) => game.Move(players, Steps);
        public MoveCard(string description, int steps, Game game) : base(description)
        {
            CardType = CARD_TYPES.move;
            this.game = game;
            Steps = steps;
        }
    }
}
