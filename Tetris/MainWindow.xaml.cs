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

namespace Tetris
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

                switch (e.Key)
                {
                    case Key.Left:
                        gameState.LeftPressTime = null;
                        gameState.IsLeftPressed = false;
                        break;
                    case Key.Right:
                        gameState.RightPressTime = null;
                        gameState.IsRightPressed = false;
                        break;
                    default:
                        return;
                }
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

                //GameScreen.HandleHorizontalMovement(e, gameState);
                //gameScreen.HandleHorizontalMovement(e, gameState);

                if (gameState.GameOver)
                {
                    return;
                }

                switch (e.Key)
                {
                    case Key.Left:
                        /*if (gameState.LeftPressTime == null)
                        {
                            gameState.LeftPressTime = DateTime.Now;
                        }
                        
                        gameState.IsLeftPressed = true;
                        gameScreen.HandleHorizontalMovement("left");
                        */
                        inputHandler.OnKeyDown("game", "left");
                        break;
                    case Key.Right:
                        /*if (gameState.RightPressTime == null)
                        {
                            gameState.RightPressTime = DateTime.Now;
                        }
                        
                        gameState.IsRightPressed = true;
                        gameScreen.HandleHorizontalMovement("right");
                        */
                        inputHandler.OnKeyDown("game", "right");
                        break;
                    case Key.Down:
                        gameState.MoveBlockDown();
                        break;
                    case Key.Up:
                        gameState.RotateBlockCW();
                        break;
                    case Key.Z:
                        gameState.RotateBlockCCW();
                        break;
                    case Key.C:
                        gameState.HoldBlock();
                        break;
                    case Key.Space:
                        gameState.DropBlock();
                        break;
                    case Key.Escape:
                        // Open pause menu
                        if (PauseMenu.Visibility == Visibility.Collapsed)
                        {
                            PauseGame();
                            return;
                        }
                        else
                        {
                            ResumeGame_Click(this, new RoutedEventArgs());
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
        }

        private void ResumeGame_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;
            // Resume game logic here
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

        public void QuitToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            PauseMenu.Visibility = Visibility.Collapsed;

            ((MainWindow)Window.GetWindow(this)).LoadMainMenu();
        }


    }
}