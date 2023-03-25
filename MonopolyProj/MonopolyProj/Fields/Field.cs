using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class Field : IField
    {
        public string? Name { get; set; } = "Unknown";

        public virtual FIELD_TYPES FieldType { get; set; }

        public virtual void OnEnter(Player player) { }
        public Field(string? name, FIELD_TYPES fieldType)
        {
            Name = name;
            FieldType = fieldType;
        }
    }
}
