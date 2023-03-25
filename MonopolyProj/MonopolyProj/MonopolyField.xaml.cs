using MonopolyProj.Fields;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonopolyProj
{
    public partial class MonopolyField : UserControl
    {
        private Game game;
        private List<Field> fields;
        private MainWindow mainWindow;

        public MonopolyField(Game game, MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.fields = GetAllFields();
            this.game = game;
            actionRow.SetGame(game);
            actionRow.SetMonopolyField(this);
            Update();
        }

        public MainWindow GetMainWindow
        {
            get { return mainWindow; }
        }

        private List<Field> GetAllFields()
        {
            var fields = new List<List<Field>>()
            {
                GetFields(row0),
                GetFields(row1),
                GetFields(row2),
                GetFields(row3),
            }.SelectMany(i => i);
            return fields.ToList();
        }

        private List<Field> GetFields(FieldRow row)
        {
            return new List<Field>()
            {
                row.field1,
                row.field2,
                row.field3,
                row.field4,
                row.field5,
                row.field6,
                row.field7,
                row.field8,
                row.field9
            };
        }

        public void Update()
        {
            for (int i = 0; i < this.game.Fields.Count(); i++)
            {

                if (i == 0)
                {
                    gofield.fieldNameLabel.Content = this.game.Fields[0].Name;
                    gofield.arrow.Visibility = Visibility.Visible;
                    SetPlayersOnBigField(gofield, i);
                }
                else if (i == 10)
                {
                    prison.fieldNameLabel.Content = this.game.Fields[10].Name;
                    prison.prison.Visibility = Visibility.Visible;
                    SetPlayersOnBigField(prison, i);
                }
                else if (i == 20)
                {
                    freeParking.fieldNameLabel.Content = this.game.Fields[20].Name;
                    freeParking.car.Visibility = Visibility.Visible;
                    SetPlayersOnBigField(freeParking, i);
                }
                else if (i == 30)
                {
                    goToJail.fieldNameLabel.Content = this.game.Fields[30].Name;
                    goToJail.policeMan.Visibility = Visibility.Visible;
                    SetPlayersOnBigField(goToJail, i);
                }
                else if (i < 10)
                {
                    SetFieldData(i, -1);
                }
                else if (i > 10 && i < 20)
                {
                    SetFieldData(i, -2);
                }
                else if (i > 20 && i < 30)
                {
                    SetFieldData(i, -3);
                }
                else if (i > 30 && i < 40)
                {
                    SetFieldData(i, -4);
                }
            }

            SetPlayerData();
            SetActionRowData();
        }

        private void SetPlayerData()
        {
            PlayerData[] playerDatas = GetPlayerDatas();
            for (int i = 0; i < playerDatas.Length; i++)
            {
                playerDatas[i].Visibility = Visibility.Visible;
                playerDatas[i].playerName.Content = this.game.Players[i].NickName;
                playerDatas[i].playerHeads[i].Visibility = Visibility.Visible;
                playerDatas[i].playerMoney.Content = $"{this.game.Players[i].Money}$";
                SetPlayerOwnerShip(playerDatas);

                playerDatas[i].playerOwnerShip.ItemsSource = playerDatas[i].PlayerOwnerShip;

                if (this.game.Players[i].NickName == this.game.CurrentPlayer.NickName)
                    playerDatas[i].playerName.Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
                else
                    playerDatas[i].playerName.Background = new SolidColorBrush(Colors.PaleGoldenrod);
            }
            HidePlayerData();
        }

        private void HidePlayerData()
        {
            PlayerData[] allData = new PlayerData[] { playerDataRow.Player1Data, playerDataRow.Player2Data };
            for (int i = 0; i < allData.Length; i++)
            {
                if (i > this.game.Players.Count() - 1)
                    allData[i].Visibility = Visibility.Hidden;
            }
        }

        private void SetActionRowData()
        {
            List<string> playerNames = this.game.Players.Select(p => p.NickName).ToList();
            actionRow.PlayerSelection.ItemsSource = playerNames;
        }

        private PlayerData[] GetPlayerDatas()
        {
            PlayerData[] datas = new PlayerData[] { playerDataRow.Player1Data, playerDataRow.Player2Data };
            Array.Resize(ref datas, this.game.Players.Count());
            return datas;
        }



        private void SetPlayerOwnerShip(PlayerData[] playerDatas)
        {
            for (int data = 0; data < playerDatas.Length; data++)
            {
                playerDatas[data].PlayerOwnerShip.Clear();
                for (int ownership = 0; ownership < this.game.Players[data].OwnerShip.Count(); ownership++)
                {
                    playerDatas[data].PlayerOwnerShip.Add(this.game.Players[data].OwnerShip[ownership].Name);
                }
            }
        }

        private void SetFieldData(int position, int extra)
        {
            fields[position + extra].fieldNameLabel.Content = this.game.Fields[position].Name;
            if (this.game.Fields[position].GetType() != typeof(StreetField))
            {
                fields[position + extra].fieldColorRectangle.Fill = null;
            }
            if (this.game.Fields[position].GetType() == typeof(TrainStationField))
            {
                fields[position + extra].Train.Visibility = Visibility.Visible;
            }
            if (this.game.Fields[position].GetType() == typeof(ChanceField))
            {
                fields[position + extra].QuestionMark.Visibility = Visibility.Visible;
            }
            if (this.game.Fields[position].GetType() == typeof(CommunityChestField))
            {
                fields[position + extra].Chest.Visibility = Visibility.Visible;
            }
            if (this.game.Fields[position].GetType() == typeof(SupplierField))
            {
                if (this.game.Fields[position].Name == "Water Works")
                    fields[position + extra].Water.Visibility = Visibility.Visible;
                else if (this.game.Fields[position].Name == "Energy Company")
                    fields[position + extra].Bulb.Visibility = Visibility.Visible;
            }

            if (this.game.Fields[position].GetType() == typeof(StreetField) || this.game.Fields[position].GetType() == typeof(TrainStationField)
            || this.game.Fields[position].GetType() == typeof(SupplierField))
                fields[position + extra].fieldPriceLabel.Content = (((BuyableField)this.game.Fields[position]).Cost).ToString() + " $";
            else
                fields[position + extra].fieldPriceLabel.Content = null;
            SetPlayers(position, extra);

        }
        private void SetPlayers(int position, int extra)
        {
            if (this.game.PlayerPosition.Count() >= 1 && this.game.PlayerPosition[this.game.Players[0]] == position)
            {
                fields[position + extra].Player1.Visibility = Visibility.Visible;
            }
            else
            {
                fields[position + extra].Player1.Visibility = Visibility.Hidden;
            }

            if (this.game.PlayerPosition.Count() >= 2 && this.game.PlayerPosition[this.game.Players[1]] == position)
            {
                fields[position + extra].Player2.Visibility = Visibility.Visible;
            }
            else
            {
                fields[position + extra].Player2.Visibility = Visibility.Hidden;
            }
        }

        private void SetPlayersOnBigField(BigField bigField, int position)
        {
            if (this.game.PlayerPosition.Count() >= 1 && this.game.PlayerPosition[this.game.Players[0]] == position)
            {
                bigField.Player1.Visibility = Visibility.Visible;
            }
            else
            {
                bigField.Player1.Visibility = Visibility.Hidden;
            }

            if (this.game.PlayerPosition.Count() >= 2 && this.game.PlayerPosition[this.game.Players[1]] == position)
            {
                bigField.Player2.Visibility = Visibility.Visible;
            }
            else
            {
                bigField.Player2.Visibility = Visibility.Hidden;
            }
        }

    }
}
