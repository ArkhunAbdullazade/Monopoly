using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public enum FIELD_TYPES
    {
        start,
        jail,
        goToJail,
        street,
        station,
        supplier,
        chance,
        communityChest,
        tax,
        freeParking,
    }
    public interface IField
    {
        public string? Name { get; }
        public FIELD_TYPES FieldType { get; }
        public void OnEnter(Player player);
    }
}
