using MonopolyProj.Cards;
using MonopolyProj.GameClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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

    public partial class MainWindow : Window
    {
        public Menu menu { get; private set; }
        public MonopolyField monopolyField { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            menu = new Menu(this);
            ShowMenu();
        }

        public void ShowMenu()
        {
            contentControl.Content = menu;
        }

        public void ShowMonopolyField(Game game)
        {
            monopolyField = new MonopolyField(game, this);
            contentControl.Content = monopolyField;
        }
    }
}

