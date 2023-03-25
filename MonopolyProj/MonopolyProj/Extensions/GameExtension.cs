using MonopolyProj.Fields;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Extensions
{
    public static class GameExtension
    {
        public static void Move(this Game game, Player player, int stepsToGo)
        {
            if (stepsToGo < 0)
            {
                if (game.PlayerPosition[player] + stepsToGo < 0)
                {
                    game.SetPlayerPosition(player, (game.PlayerPosition[player] + stepsToGo) + game.Fields.Count());
                }
                else
                {
                    game.SetPlayerPosition(player, (game.PlayerPosition[player] + stepsToGo));
                }
            }
            else
            {
                if (game.PlayerPosition[player] + stepsToGo > game.Fields.Count() - 1)
                {
                    game.SetPlayerPos(player, game.PlayerPosition[player] + stepsToGo - game.Fields.Count());
                    player.EarnMoney(200);
                }
                else
                {
                    game.SetPlayerPosition(player, (game.PlayerPosition[player] + stepsToGo));
                }
            }
        }

        public static void MoveTo(this Game game, Player player, int destination)
        {
            if (destination > game.PlayerPosition[player])
            {
                game.SetPlayerPosition(player, destination);
            }
            else if (destination < game.PlayerPosition[player])
            {
                game.SetPlayerPosition(player, destination);
                player.EarnMoney(200);
            }
        }

        public static int CheckForDoubletsInARow(this Game game, Player player)
        {
            int doublets = 0;
            bool lastCheck = false;
            for (int i = game.DiceThrows[player].Count() - 1; i >= 0; i--)
            {
                if (game.DiceThrows[player][i][0] == game.DiceThrows[player][i][1])
                {
                    if (lastCheck)
                        doublets++;
                    lastCheck = true;
                }
                else
                {
                    lastCheck = false;
                }
            }
            return doublets;
        }

        public static bool CheckForPrison(this Game game, Player player, int[] dices)
        {
            if (player.InPrison == false)
                return true;
            else
                game.TriesToEscapeFromPrison[player]++;

            if (game.Double(player, dices) == false && game.TriesToEscapeFromPrison[player] >= 3)
            {
                game.PayFineImmediately(player);
                return true;
            }
            else if (player.InPrison && game.Double(player, dices))
            {
                game.RemovePlayerFromPrison(player);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int[] ThrowDice(this Game game, Player player)
        {
            Random random = new Random(System.DateTime.Now.Millisecond.GetHashCode());
            int value1 = random.Next(1, 6);
            int value2 = random.Next(1, 6);
            int[] dices = new int[] { value1, value2 };
            game.SaveDiceThrow(player, dices);

            return dices;
        }
    }
}
