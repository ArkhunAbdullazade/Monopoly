using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class GoToJailField : IField
    {
        public string Name { get; private set; }
        public FIELD_TYPES FieldType { get; set; } = FIELD_TYPES.goToJail;
        Game game;

        public GoToJailField(string name, Game game)
        {
            Name = name;
            this.game = game;
        }
        public void OnEnter(Player player)
        {
            game.SetPlayerInPrison(player);
        }
    }
}
