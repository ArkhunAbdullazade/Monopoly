using MonopolyProj.Cards;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Fields
{
    public sealed class FieldStorageSingleton
    {
        public readonly FieldStorage? FieldStorage;

        private static FieldStorageSingleton? instance = null;

        public static FieldStorageSingleton GetInstance(Game game)
        {
            if (instance == null)
            {
                var fieldStorageTemp = new FieldStorage(game);

                instance = new FieldStorageSingleton(fieldStorageTemp);
            }

            return instance;
        }

        private FieldStorageSingleton(FieldStorage? fieldStorage)
        {
            FieldStorage = fieldStorage;
        }
    }
}
