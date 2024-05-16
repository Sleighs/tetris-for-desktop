using System.Windows;
using System.Windows.Automation.Peers;

namespace Tetris
{
    public class GameState
    {
        private Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
                
                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }

        public bool GameStart { get; private set; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public Block HeldBlock { get; private set; }    
        public bool CanHold { get; private set; }

        public bool IsLeftPressed { get; internal set; }
        public bool IsRightPressed { get; internal set; }

        public DateTime? LeftPressTime { get; internal set; } = null;
        public DateTime? RightPressTime { get; internal set; } = null;

        public DateTime LastAutoShiftTime { get; internal set; }
        public TimeSpan dasDelay = TimeSpan.FromMilliseconds(500);  // Delay before auto-repeat starts
        public TimeSpan arrDelay = TimeSpan.FromMilliseconds(50);  // Auto-repeat rate

        public int ArrDelay { get; internal set; }
        public int DasDelay { get; internal set; }
        public bool IsPlacingPieceAfterDelay { get; private set; } = false;

        public bool CanDropNewBlock { get; internal set; } = true;

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
            GameStart = false;
            IsRightPressed = false;
            IsLeftPressed = false;
        }

        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void HoldBlock()
        {
            if (!CanHold)
            { return; }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }

        public void RotateBlockCW() 
        {
            CurrentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock()
        {
            // Set id for each cell in the game grid 
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += GameGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;       
            }
        }

        public async void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                IsPlacingPieceAfterDelay = true;

                await Task.Delay(2000);
                IsPlacingPieceAfterDelay = false;
                if (!IsPlacingPieceAfterDelay)
                {
                    PlaceBlock();
                }
            }
        }

        private int TileDropDistance(Position p)
        {
            int drop = 0;

            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;

            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        public async void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);

            // Wait before placing the block   
            IsPlacingPieceAfterDelay = true;
            await Task.Delay(2000);
            IsPlacingPieceAfterDelay = false; // Reset the flag after the delay

            if (!IsPlacingPieceAfterDelay) // Only place the block if the flag is false
            {
                PlaceBlock(); // Place the block after the delay
            }
        }
    }
}
