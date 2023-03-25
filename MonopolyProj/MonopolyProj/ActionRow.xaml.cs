using MonopolyProj.Exceptions;
using MonopolyProj.Fields;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Xml;
using System.Xml.Linq;

namespace MonopolyProj
{
    public partial class ActionRow : UserControl
    {
        private Game game;
        private MonopolyField monopolyField;
        private Player selectedPlayer;
        private BuyableField exchangeField;

        private bool notEnoughMoney;
        private int neededAmount;
        private int cubeThrows;

        public ActionRow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetGame(Game game)
        {
            this.game = game;
        }
        public void SetMonopolyField(MonopolyField monopolyField)
        {
            this.monopolyField = monopolyField;
        }

        public void SetPlayersInActionRow()
        {

        }

        private void ThrowCubesButton(object sender, RoutedEventArgs e)
        {
             if (cubeThrows >= 1)
            {
                MessageBox.Show("You can not throw again");
            }
            else
            {
                try
                {
                    this.game.GoForward(this.game.CurrentPlayer);
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(BankruptException))
                    {
                        MessageBox.Show("YOU ARE BANKRUPT");
                        MessageBox.Show($"The Winner is {this.game.Players[0].NickName}");
                        monopolyField.GetMainWindow.Close();
                        return;
                    }
                }
                monopolyField.Update();
                PayMessageBox();
                CardMessageBox();
                cubeThrows++;
            }
        }

        private void FinishTurnButton(object sender, RoutedEventArgs e)
        {
            if (cubeThrows < 1)
            {
                MessageBox.Show("You have to Throw the Cubes");
            }
            else if (notEnoughMoney && neededAmount > this.game.CurrentPlayer.Money)
            {
                MessageBox.Show($"You have to sell something to continue. You need {neededAmount}$");
            }
            else
            {
                if (notEnoughMoney && neededAmount <= this.game.CurrentPlayer.Money)
                    this.game.CurrentPlayer.PayMoney(neededAmount);
                this.game.NextPlayer();
                monopolyField.Update();
                ResetVariables();
            }
        }

        private void ResetVariables()
        {
            cubeThrows = 0;
            selectedPlayer = null;
            neededAmount = 0;
            notEnoughMoney = false;
        }

        private void PlaceholdersListBoxPlayer_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject)) as ListBoxItem;
            if (item != null)
            {
                foreach (Player currentPlayer in this.game.Players)
                {
                    if (currentPlayer.NickName == item.Content.ToString())
                        selectedPlayer = currentPlayer;
                }
            }
        }

        private void PayMessageBox()
        {
            if (this.game.LastPayMent.Count() != 0)
            {
                MessageBox.Show($"You have to pay {this.game.LastPayMent[this.game.CurrentPlayer]}$");
                this.game.ClearLastPayment();
            }
        }

        private void CardMessageBox()
        {
            if (this.game.LastCardText.Count() != 0)
            {
                MessageBox.Show($"You Entered a Card Field: {this.game.LastCardText[this.game.CurrentPlayer]}");
            }
        }

        private BuyableField FindFieldByName(string name)
        {
            foreach (var item in game.BuyableFields)
            {
                if (item.Name == name)
                {
                    return (BuyableField)item;
                }
            }
            return null;
        }
        private void ExchangeFieldButton(object sender, RoutedEventArgs e)
        {
            var exchangeData = inputField.Text.Split('-');
            exchangeField = FindFieldByName(exchangeData[0]);
            
            try
            {
                (exchangeField).ExchangeField(this.game.CurrentPlayer, selectedPlayer, int.Parse(exchangeData[1]));
                MessageBox.Show($"The Owner ( {this.game.CurrentPlayer.NickName} ) exchanged the Field '{exchangeField.Name}' with Player {selectedPlayer.NickName} for {exchangeData[1]}$");
                monopolyField.Update();
            }
            catch
            {
                MessageBox.Show("Field Exchange Error");
            }
        }

        private void LeavePrisonButton(object sender, RoutedEventArgs e)
        {
            try
            {
                this.game.PayFineImmediately(this.game.CurrentPlayer);
                monopolyField.Update();
                MessageBox.Show("You left the prison");
            }
            catch
            {
                MessageBox.Show("You cant go out of jail");
            }
        }

        private void BuyButton(object sender, RoutedEventArgs e)
        {
            IField field = this.game.Fields[this.game.PlayerPosition[this.game.CurrentPlayer]];

            try
            {
                ((BuyableField)field).Buy(this.game.CurrentPlayer);
                monopolyField.Update();
            }
            catch
            {
                MessageBox.Show("You cant buy this Field");
            }
            PayMessageBox();
        }

        private void BuyHouseButton(object sender, RoutedEventArgs e)
        {
            IField field = this.game.Fields[this.game.PlayerPosition[this.game.CurrentPlayer]];

            try
            {
                ((StreetField)field).BuyHouse(this.game.CurrentPlayer);
                monopolyField.Update();
                MessageBox.Show($"You bought a house for '{field.Name}'");
            }
            catch
            {
                MessageBox.Show("You cant buy a house for this field");
            }
            PayMessageBox();
        }

        private void MusicButton(object sender, RoutedEventArgs e)
        {
            if (game.IsPlayingMusic) game.bgMusic.Stop();
            else game.bgMusic.PlayLooping();
            game.IsPlayingMusic = !game.IsPlayingMusic;
        }
    }
}
