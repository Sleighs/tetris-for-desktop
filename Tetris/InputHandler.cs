using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Tetris.Models;
using System.Windows.Media;
using Tetris.Views;

namespace Tetris
{
    public class InputHandler
    {
        private GameState gameState;
        private GameScreen gameScreen;

        public InputHandler(GameState gameState, GameScreen gameScreen)
        {
            this.gameState = gameState;
            this.gameScreen = gameScreen;
        }

        /*public bool IsLeftPressed { get; internal set; }
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
        */

        private void OnKeyUp(string screenType, string keyPressed)    
        {
            if (gameState.IsLeftPressed && gameState.IsRightPressed)
            {
                //gameState.IsLeftPressed = false;
                //gameState.IsRightPressed = false;
            }
        }

        public void OnKeyDown(string screenType, string keyPressed)
        {

            if (screenType == "game")
            {
                switch (keyPressed)
                {
                    case "left":
                        if (gameState.LeftPressTime == null)
                        {
                            gameState.LeftPressTime = DateTime.Now;
                        }

                        gameState.IsLeftPressed = true;
                        HandleHorizontalMovement("left");
                        break;
                    case "right":
                        if (gameState.RightPressTime == null)
                        {
                            gameState.RightPressTime = DateTime.Now;
                        }

                        gameState.IsRightPressed = true;
                        HandleHorizontalMovement("right");
                        break;
                }
            }

        }

        private void HandleHorizontalMovement(string button)
        {
            DateTime now = DateTime.Now;

            Console.WriteLine(button);
            
            Console.WriteLine("Left Pressed: " + gameState.IsLeftPressed);
            Console.WriteLine("Right Pressed: " + gameState.IsRightPressed);
            Console.WriteLine("Left Press Time: " + gameState.LeftPressTime);
            Console.WriteLine("Right Press Time: " + gameState.RightPressTime);
            Console.WriteLine(now);

            if (gameState.IsLeftPressed && gameState.IsRightPressed)
            {
                //gameState.IsLeftPressed = false;
                //gameState.IsRightPressed = false;
            }

            if (gameState.IsLeftPressed && gameState.LeftPressTime.HasValue)// && (now - gameState.LeftPressTime) >= gameState.dasDelay)
            {
                gameState.MoveBlockLeft();
                //gameScreen.Draw(gameState);
            }
            if (gameState.IsRightPressed && gameState.RightPressTime.HasValue)// && (now - gameState.RightPressTime) >= gameState.dasDelay)
            {
                gameState.MoveBlockRight();
                //gameScreen.Draw(gameState);
            }

        }
    }
}
