using MonopolyProj.Extensions;
using MonopolyProj.Fields;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public class MoveToFieldCard : Card
    {
        Game game;
        public int FieldPosition { get; private set; }
        public override void UseCard(Player players) => game.MoveTo(players, FieldPosition);
        public MoveToFieldCard(string description, int fieldPosition, Game game) : base(description)
        {
            CardType = CARD_TYPES.moveTo;
            FieldPosition = fieldPosition;
            this.game = game;
        }
    }
}
