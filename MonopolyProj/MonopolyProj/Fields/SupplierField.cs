using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class SupplierField : BuyableField
    {
        public string Name { get; private set; }
        public override FIELD_TYPES FieldType { get; set; }
        public double RentToPay
        {
            get => Cost;
        }
        public SupplierField(string name, double cost) : base(name, FIELD_TYPES.supplier, cost) { }
    }
}
