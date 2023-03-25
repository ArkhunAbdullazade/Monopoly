using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class FreeParkingField : IField
    {
        public string Name { get; private set; }
        public FIELD_TYPES FieldType { get; set; } = FIELD_TYPES.freeParking;
        public FreeParkingField(string name)
        {
            Name = name;
        }

        public void OnEnter(Player player) { }
    } 
}
