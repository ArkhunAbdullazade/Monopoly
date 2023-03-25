using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public enum CARD_TYPES
    {
        money,
        move,
        moveTo,
        GoToPrison,
        GetOutOfPrison,
    }
    public interface ICardUsable
    {
        void UseCard(Player players);
    }
}
