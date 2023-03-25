using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class FieldData
    {
        public string Name { get; set; }
        public FIELD_TYPES FieldType { get; set; }

        public FieldData(string name, FIELD_TYPES fieldType)
        {
            Name = name;
            FieldType = fieldType;
        }
    }
}
