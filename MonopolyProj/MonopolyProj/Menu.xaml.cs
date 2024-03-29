﻿using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MonopolyProj
{
    public partial class Menu : UserControl
    {
        public ObservableCollection<string> PlayerNames { get; private set; } = new ObservableCollection<string>();
        private Game game;
        private MainWindow mainWindow;

        public Menu(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            playerList.ItemsSource = PlayerNames;
        }

        private void AddPlayerButtonClick(object sender, RoutedEventArgs e)
        {
            if (PlayerNames.Contains(playerNameTextBox.Text))
            {
                MessageBox.Show("You cant use a Player-Name twice");
            }
            else if ((playerNameTextBox.Text.Length != 0))
            {
                PlayerNames.Add(playerNameTextBox.Text);
                playerNameTextBox.Text = null;
            }
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            if (PlayerNames.Count() != 2)
            {
                MessageBox.Show("You have to add 2 Players to start the game");
            }
            else
            {
                Player[] players = new Player[PlayerNames.Count()];
                for (int i = 0; i < PlayerNames.Count(); i++)
                {
                    players[i] = new Player(PlayerNames[i]);
                }
                game = new Game(players);
                this.mainWindow.ShowMonopolyField(game);
            }
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            PlayerNames.Clear();
        }
    }
}