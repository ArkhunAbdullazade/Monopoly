﻿using System;
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
    public partial class PlayerData : UserControl
    {
        public Ellipse[] playerHeads;
        public ObservableCollection<string> PlayerOwnerShip { get; set; } = new ObservableCollection<string>();

        public PlayerData()
        {
            InitializeComponent();
            DataContext = this;
            playerHeads = new Ellipse[] { Player1Head, Player2Head };
        }

    }
}
