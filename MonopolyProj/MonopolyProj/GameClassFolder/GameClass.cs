using MonopolyProj.Cards;
using MonopolyProj.Extensions;
using MonopolyProj.Fields;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace MonopolyProj.GameClass
{
	public class Game
	{
        private IField[] fields;
        private Card[] chanceCard;
        private Card[] communityChestCards;
        private List<Player> players;
        private Dictionary<Player, int> playerPositions = new Dictionary<Player, int>();
        private Dictionary<Player, List<int[]>> diceThrows = new Dictionary<Player, List<int[]>>();
        private Dictionary<Player, int> lastPayMent = new Dictionary<Player, int>();
        private Dictionary<Player, int> triesToEscapeFromPrison = new Dictionary<Player, int>();
        private Dictionary<Player, string> lastCardText = new Dictionary<Player, string>();
        private Queue<Player> playerQueue = new Queue<Player>();
        private IEnumerable<IField> buyableFields;
        public SoundPlayer bgMusic { get; set; } = new SoundPlayer("bgmusic.wav");
        public bool IsPlayingMusic { get; set; } = false;

        public bool IsGameOver { get; set; }

        public Dictionary<Player, int> TriesToEscapeFromPrison
        {
            get => triesToEscapeFromPrison; 
            set => triesToEscapeFromPrison = value;
        }
        public IReadOnlyDictionary<Player, List<int[]>> DiceThrows
        {
            get { return diceThrows; }
        }
        public IReadOnlyList<IField> BuyableFields
        {
            get { return buyableFields.ToList(); }
        }
        public Player CurrentPlayer { get; private set; }

        public IReadOnlyDictionary<Player, int> PlayerPosition
        {
            get { return playerPositions; }
        }

        public IReadOnlyList<Player> Players
        {
            get { return players; }
        }

        public IReadOnlyList<IField> Fields
        {
            get { return fields; }
        }

        public IReadOnlyList<int> GetLastThrow(Player player)
        {
            return Array.AsReadOnly(diceThrows[player][diceThrows[player].Count() - 1]);
        }

        public IReadOnlyDictionary<Player, int> LastPayMent
        {
            get { return lastPayMent; }
        }

        public IReadOnlyList<Card> CommunityChest
        {
            get { return communityChestCards; }
        }

        public IReadOnlyList<Card> Chance
        {
            get { return chanceCard; }
        }

        public IReadOnlyDictionary<Player, string> LastCardText
        {
            get { return lastCardText; }
        }


        public Game(Player[] players)
        {
            CardStorageSingleton cardsStorage = CardStorageSingleton.GetInstance(this);
            chanceCard = cardsStorage.CardStorage.chanceCards.ToArray();
            communityChestCards = cardsStorage.CardStorage.comunityCards.ToArray();
            FieldStorageSingleton fieldsStorage = FieldStorageSingleton.GetInstance(this);
            fields = fieldsStorage.FieldStorage.fields.ToArray();
            this.players = players.ToList();
            buyableFields = fields.Where(i => i is BuyableField);
            foreach (Player player in players)
                playerQueue.Enqueue(player);
            foreach (Player player in players)
                playerPositions.Add(player, 0);
            foreach (Player player in players)
                diceThrows.Add(player, new List<int[]>());
            foreach (Player player in players)
                triesToEscapeFromPrison.Add(player, 0);
            NextPlayer();
        }

        public void NextPlayer()
        {
            Player player = playerQueue.Dequeue();

            CurrentPlayer = player;

            playerQueue.Enqueue(player);
            lastPayMent.Clear();
            lastCardText.Clear();
        }

        public void GoForward(Player player)
        {
            SetPlayerPosition(player, this.ThrowDice(player));
        }

        public void SetLastCardText(Player player, string cardText)
        {
            lastCardText.Add(player, cardText);
        }

        public void SetPlayerPosition(Player player, int[] dices)
        {
            if (Double(player, dices) && this.CheckForDoubletsInARow(player) >= 2)
            {
                SetPlayerInPrison(player);
                return;
            }

            if (this.CheckForPrison(player, dices) == false)
                return;

            playerPositions[player] = SetInRange(dices, PlayerPosition[player]);
            fields[playerPositions[player]].OnEnter(player);
        }

        public void SetPlayerPosition(Player player, int position)
        {
            playerPositions[player] = position;
            fields[playerPositions[player]].OnEnter(player);
        }

        

        public void PayFineImmediately(Player player)
        {
            RemovePlayerFromPrison(player);
            player.PayMoney(50);
            GoForward(player);
        }

        public void RemovePlayerFromPrison(Player player)
        {
            if (player.InPrison == false)
                throw new InvalidOperationException("The player is not in Jail and has not to be removed");
            player.InPrison = false;
            triesToEscapeFromPrison[player] = 0;
        }

        public void SetPlayerInPrison(Player player)
        {
            playerPositions[player] = 10;
            player.InPrison = true;
        }

        public void CallOnEnter(Player player)
        {
            fields[playerPositions[player]].OnEnter(player);
        }

        public void SaveDiceThrow(Player player, int[] dices)
        {
            diceThrows[player].Add(dices);
            if (diceThrows[player].Count() > 3)
                diceThrows[player].RemoveAt(0);
        }

        private int SetInRange(int[] diceThrow, int playerPos)
        {
            int diceSum = diceThrow[0] + diceThrow[1];
            if (playerPos + diceSum > fields.Count() - 1)
            {
                CrossedStartField();
                int nextPos = playerPos + diceSum;
                while (nextPos > fields.Count() - 1)
                {
                    nextPos -= fields.Count();
                }
                return nextPos;
            }
            else
            {
                return playerPos + diceSum;
            }
        }

        private void CrossedStartField()
        {
            CurrentPlayer.EarnMoney(200);
        }

        public bool Double(Player player, int[] diceThrow)
        {
            return diceThrow[0] == diceThrow[1];
        }

        public void SetPlayerPos(Player player, int pos)
        {
            playerPositions[player] = pos;
            fields[playerPositions[player]].OnEnter(player);
        }

        public void SetLastThrow(Player player, List<int[]> throwed)
        {
            diceThrows[player] = throwed;
        }

        public void SetLastPayMent(Player player, int amount)
        {
            if (LastPayMent.ContainsKey(player) == false)
                lastPayMent.Add(player, amount);
            else
                lastPayMent[player] = amount;
        }

        public void ClearLastPayment()
        {
            lastPayMent.Clear();
        }
    }
}
