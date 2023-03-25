using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Documents;
using MonopolyProj.Cards;
using MonopolyProj.GameClass;

namespace MonopolyProj.Cards
{
    public class CardStorage
    {
        public List<Card> chanceCards { get; set; }
        public List<Card> comunityCards { get; set; }
        public List<List<int>> cardNumerals { get; set; }

        public CardStorage(Game game)
        {
            chanceCards = GetChanceCards(game);
            comunityCards = GetCommunityChestCards(game);
        }
        private List<List<int>> GetCardNumerals()
        {
            CardDataStorage tempCards = DeserialiseCards();
            return tempCards.cardNumerals;
        }
        private CardDataStorage DeserialiseCards()
        {
            var json = File.ReadAllText("Cards.json");
            CardDataStorage jsonCards = JsonSerializer.Deserialize<CardDataStorage>(json);
            return jsonCards;
        }

        private List<Card> CardDataToCard(List<CardData> cards)
        {
            List<Card> temp = new List<Card> { };
            foreach (var card in cards)
            {
                Card cardToAdd = new Card(card.Description);
                cardToAdd.CardType = card.CardType;
                temp.Add(cardToAdd);
            }
            return temp;
        }
        private List<Card> GetChanceCards(Game game)
        {
            List<Card> tempChanceCards = CardDataToCard(DeserialiseCards().chanceCards);
            List<int> tempCardNumerals = GetCardNumerals()[1];
            List<Card> finalChanceCards = GetCardsFromJSON(game, tempChanceCards, tempCardNumerals);

            return finalChanceCards;
        }
        private List<Card> GetCommunityChestCards(Game game)
        {
            List<Card> tempCommunityCards = CardDataToCard(DeserialiseCards().comunityCards);
            List<int> tempCardNumerals = GetCardNumerals()[0];
            List<Card> finalCommunityCards = GetCardsFromJSON(game, tempCommunityCards, tempCardNumerals);

            return finalCommunityCards;
        }
        private List<Card> GetCardsFromJSON(Game game, List<Card> Cards, List<int> CardNumerals)
        {
            List<Card> finalChanceCards = new List<Card> { };
            int currentNum = CardNumerals[0];
            int counter = 0;

            foreach (var card in Cards)
            {
                if (counter < CardNumerals.Count) currentNum = CardNumerals[counter];
                counter++;
                switch (card.CardType)
                {
                    case CARD_TYPES.money:
                        finalChanceCards.Add(new MoneyCard(card.Description, currentNum));
                        break;
                    case CARD_TYPES.move:
                        finalChanceCards.Add(new MoveCard(card.Description, currentNum, game));
                        break;
                    case CARD_TYPES.moveTo:
                        finalChanceCards.Add(new MoveToFieldCard(card.Description, currentNum, game));
                        break;
                    case CARD_TYPES.GoToPrison:
                        counter--;
                        finalChanceCards.Add(new GoToPrisonCard(card.Description, game));
                        break;
                    case CARD_TYPES.GetOutOfPrison:
                        counter--;
                        finalChanceCards.Add(new GetOutOfPrisonCard(card.Description, game));
                        break;
                }
            }

            return finalChanceCards;
        }

    }

}