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
using System.Windows.Threading;
using Tetris;
using Tetris.Models;


namespace Tetris.Views
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : UserControl
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("/Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/TileRed.png", UriKind.Relative)),
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("/Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("/Assets/Block-Z.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;

        public GameState gameState = new GameState();

        private Dictionary<Key, bool> keyStates = new Dictionary<Key, bool>();
        private DispatcherTimer gameLoopTimer;
        //private Game game;

        public GameScreen()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
            InitializeGame();
        }

        private void InitializeGame()
        {
            /*
            this.Loaded += GameScreen_Loaded;
            this.Unloaded += GameScreen_Unloaded;

            // Set up key event handlers
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.Focusable = true;
            this.Focus();

            // Initialize game and game loop
            game = new Game(keyStates);
*/
            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            gameLoopTimer.Tick += GameLoop;
            
        }



        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }

            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private void DrawHeldBlock(Block heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach (Position p in block.TilePositions())
            {

                imageControls[p.Row + dropDistance, p.Column].Opacity = .25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        public void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            DrawGhostBlock(gameState.CurrentBlock);
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                int fallDelay = 1000;
                //CalculateFallDelay();
                
                await Task.Delay(fallDelay);

                if (!gameState.IsPaused)
                { 
                    gameState.MoveBlockDown();
                    
                    Draw(gameState);
                }
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }

        private int CalculateFallDelay()
        {
            int level = 1; //gameState.Level; // Assuming gameState has a Level property that increments
            int baseDelay = 500;
            int delayDecreasePerLevel = 25;
            int minimumDelay = 100;
            return Math.Max(
                //baseDelay - (level - 1) * delayDecreasePerLevel, minimumDelay
                baseDelay - (level * delayDecreasePerLevel), minimumDelay
                );
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            //game.Update();
            // Optionally, you can also call InvalidateVisual or other methods to update the UI
        }
    }
}
