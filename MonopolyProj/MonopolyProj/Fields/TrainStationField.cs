using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public class TrainStationField : BuyableField
    {
        public string Name { get; private set; }
        public override FIELD_TYPES FieldType { get; set; }
        public double RentToPay
        {
            get => (Owner.OwnerShip.Count(f => f.FieldType == FIELD_TYPES.station)) * Cost;
        }
        public TrainStationField(string name, double cost) : base(name, FIELD_TYPES.station, cost) { }
    }
}
