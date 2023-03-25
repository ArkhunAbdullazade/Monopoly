using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Cards
{
    public sealed class CardStorageSingleton
    {
        public readonly CardStorage? CardStorage;

        private static CardStorageSingleton? instance = null;

        public static CardStorageSingleton GetInstance(Game game)
        {
            if (instance == null)
            {
                var cardStorageTemp = new CardStorage(game);

                instance = new CardStorageSingleton(cardStorageTemp);
            }

            return instance;
        }

        private CardStorageSingleton(CardStorage? cardStorage)
        {
            CardStorage = cardStorage;
        }
    }
}
