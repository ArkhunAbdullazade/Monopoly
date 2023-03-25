
using Microsoft.VisualBasic.FileIO;
using MonopolyProj.Cards;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{

    public class FieldStorage
    {
        public List<IField> fields { get; set; } = new List<IField>();
        public List<int> fieldNumerals { get; set; }

        public FieldStorage(Game game)
        {
            fields = GetFields(game);
        }
        private List<int> GetFieldNumerals()
        {
            FieldDataStorage tempFields = DeserialiseFields();
            return tempFields.fieldNumerals;
        }
        private FieldDataStorage DeserialiseFields()
        {
            var json = File.ReadAllText("Fields.json");
            FieldDataStorage jsonCards = JsonSerializer.Deserialize<FieldDataStorage>(json);
            return jsonCards;
        }

        private List<IField> FieldDataToField(List<FieldData> fields)
        {
            List<IField> temp = new List<IField> { };
            foreach (var field in fields)
            {
                temp.Add(new Field(field.Name, field.FieldType));
            }
            return temp;
        }
        private List<IField> GetFields(Game game)
        {
            List<IField> tempFields = FieldDataToField(DeserialiseFields().fields);
            List<int> tempCardNumerals = GetFieldNumerals();
            List<IField> finalChanceCards = GetFieldsFromJSON(game, tempFields, tempCardNumerals);

            return finalChanceCards;
        }
        private List<IField> GetFieldsFromJSON(Game game, List<IField> Fields, List<int> FieldNumerals)
        {
            List<IField> finalFields = new List<IField> { };
            int currentNum = FieldNumerals[0];
            int counter = 0;

            foreach (var field in Fields)
            {
                switch (field.FieldType)
                {
                    case FIELD_TYPES.start:
                        counter--;
                        finalFields.Add(new StartField(field.Name));
                        break;
                    case FIELD_TYPES.jail:
                        counter--;
                        finalFields.Add(new JailField(field.Name));
                        break;
                    case FIELD_TYPES.goToJail:
                        counter--;
                        finalFields.Add(new GoToJailField(field.Name, game));
                        break;
                    case FIELD_TYPES.street:
                        finalFields.Add(new StreetField(field.Name, currentNum));
                        break;
                    case FIELD_TYPES.station:
                        finalFields.Add(new TrainStationField(field.Name, currentNum));
                        break;
                    case FIELD_TYPES.supplier:
                        finalFields.Add(new SupplierField(field.Name, currentNum));
                        break;
                    case FIELD_TYPES.chance:
                        counter--;
                        finalFields.Add(new ChanceField(field.Name, game));                        
                        break;
                    case FIELD_TYPES.communityChest:
                        counter--;
                        finalFields.Add(new CommunityChestField(field.Name, game));
                        break;
                    case FIELD_TYPES.tax:
                        finalFields.Add(new TaxField(field.Name, game, currentNum));
                        break;
                    case FIELD_TYPES.freeParking:
                        finalFields.Add(new FreeParkingField(field.Name));
                        break;
                }
                if (counter == 27)
                {
                    Console.WriteLine();
                }
                if (counter > 0 && counter < 29)
                {
                currentNum = FieldNumerals[counter];
                }
                counter++;
            }
            return finalFields;
        }


    }
}
