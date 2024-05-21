using System.Runtime.InteropServices;
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
using Tetris;
using Tetris.Models;


namespace Tetris.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


        //public GameState GameState { get; private set; }
        //public GameScreen GameScreen { get; private set; }


        public MainWindow()
        {
            InitializeComponent();
            LoadMainMenu();
            AllocConsole();
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (MainContent.Content is GameScreen gameScreen)
            {
                GameState gameState = gameScreen.gameState;
                //InputHandler inputHandler = new InputHandler(gameState);

                switch (e.Key)
                {
                    case Key.Left:
                        gameState.IsLeftPressed = false;
                        gameState.LeftPressTime = null;
                        break;
                    case Key.Right:
                        gameState.IsRightPressed = false;
                        gameState.RightPressTime = null;
                        break;
                    case Key.Down:
                        gameState.IsDownPressed = false;
                        break;
                    case Key.Space:
                        gameState.IsSpacePressed = false;
                        break;
                    default:
                        return;
                }

                //gameScreen.Draw(gameState);
            }
        }

       private void Window_KeyDown(object sender, KeyEventArgs e)
       {
            // Log key press
            //System.Diagnostics.Debug.WriteLine("Key pressed: " + e.Key);
            //Console.WriteLine("Key pressed: " + e.Key);

            if (MainContent.Content is GameScreen gameScreen)
            {
                GameState gameState = gameScreen.gameState;
                InputHandler inputHandler = new InputHandler(gameState);      

                if (gameState.GameOver)
                {
                    return;
                }

                switch (e.Key)
                {
                    case Key.Left:
                        inputHandler.OnKeyDown("game", "left");
                        break;
                    case Key.Right:
                        inputHandler.OnKeyDown("game", "right");
                        break;
                    case Key.Down:
                        gameState.MoveBlockDown();
                        break;
                    case Key.Up:
                        gameState.RotateBlockCW();
                        break;
                    case Key.Z:
                    case Key.RightCtrl:
                        gameState.RotateBlockCCW();
                        break;
                    case Key.C:
                        gameState.HoldBlock();
                        break;
                    case Key.Space:
                        gameState.DropBlock();
                        break;
                    case Key.Escape:
                        // Open and close pause menu
                        if (PauseMenu.Visibility == Visibility.Collapsed)
                        {
                            PauseGame();
                            return;
                        }
                        else
                        {
                            ResumeGame_Click(this, new RoutedEventArgs());
                            return;
                        }
                        break;
                    default:
                        return;
                }

                gameScreen.Draw(gameState);
            } 
            
            else if (MainContent.Content is MainMenu)
            {
                if (e.Key == Key.Escape)
                {
                    Application.Current.Shutdown();
                }
            }
            
        }


        // Pause Menu Logic
        private void PauseGame()
        {
            PauseMenu.Visibility = Visibility.Visible;
            // Pause game logic here
            GameState gameState = ((GameScreen)MainContent.Content).gameState;
            gameState.IsPaused = true;
        }

        private void ResumeGame_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            // Resume game logic here
            GameState gameState = ((GameScreen)MainContent.Content).gameState;
            gameState.IsPaused = false;
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            // Restart game logic here
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            // Open settings window or panel here
        }

        private void QuitToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;

            ((MainWindow)Window.GetWindow(this)).LoadMainMenu();
        }


    }
}