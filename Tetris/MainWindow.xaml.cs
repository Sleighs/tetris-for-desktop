using System.Security.AccessControl;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            LoadMainMenu();
        }

        public void LoadMainMenu()
        {
            // Load the main menu
            MainContent.Content = new MainMenu();
        }

 
        public void StartGame()
        {
            MainContent.Content = new GameScreen(); // Switch to the game screen
        }

       private void Window_KeyDown(object sender, KeyEventArgs e)
       {
             /*if (e.Key == Key.Escape)
            {
                if (MainContent.Content is GameScreen)
                {
                    // Handle specific logic for GameScreen
                }
                else if (MainContent.Content is MainMenu)
                {
                    // Possibly do nothing or handle a specific logic for MainMenu
                }
                else
                {
                    Application.Current.Shutdown();
                }
                e.Handled = true;
             
            }*/
        }
    }
}