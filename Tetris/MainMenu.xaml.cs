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

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Code to start the game
            // Typically changing the UserControl displayed in the MainWindow
            ((MainWindow)Window.GetWindow(this)).StartGame();
            Console.WriteLine("Start Game");
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            // Code to show high scores
            // Maybe load a new UserControl that shows high scores
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Code to open settings
            // Maybe load a new UserControl for settings
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Closes the application
        }
    }
}
