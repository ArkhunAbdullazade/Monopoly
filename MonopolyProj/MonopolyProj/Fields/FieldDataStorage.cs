using MonopolyProj.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class FieldDataStorage
    {
        public List<FieldData> fields { get; set; } = new List<FieldData>();
        public List<int> fieldNumerals { get; set; }
    }
}
