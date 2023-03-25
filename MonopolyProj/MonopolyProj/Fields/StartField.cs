using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class StartField : IField
    {
        public FIELD_TYPES FieldType { get; set; } = FIELD_TYPES.start;
        public string Name { get; private set; }

        public StartField(string name)
        {
            Name = name;
        }

        public void OnEnter(Player player)
        {
            return;
        }
    }
}
