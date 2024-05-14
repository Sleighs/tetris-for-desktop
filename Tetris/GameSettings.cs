using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameSettings
    {
        
        
        public GameSettings() {
            

        }

        // das and arr
        /*
        public int ArrDelay { get; private set; }
        public int DasDelay { get; private set; }

        public bool isLeftPressed = false;
        public bool isRightPressed = false;
        private DateTime leftPressTime;
        private DateTime rightPressTime;
        public bool isDASActive = false;
        private DateTime lastAutoShiftTime;


        void Update() {
            DateTime currentTime = DateTime.Now;

            // Check for left or right key press initiation
            if (isLeftPressed && (currentTime - leftPressTime).TotalMilliseconds >= dasDelay) {
                isDASActive = true;
                lastAutoShiftTime = currentTime;
                MovePieceLeft();  // Initial DAS movement
            }
            if (isRightPressed && (currentTime - rightPressTime).TotalMilliseconds >= dasDelay) {
                isDASActive = true;
                lastAutoShiftTime = currentTime;
                MovePieceRight(); // Initial DAS movement
            }

            // If DAS is active, check for ARR
            if (isDASActive) {
                if (isLeftPressed && (currentTime - lastAutoShiftTime).TotalMilliseconds >= arrDelay) {
                    lastAutoShiftTime = currentTime;
                    MovePieceLeft();
                }
                if (isRightPressed && (currentTime - lastAutoShiftTime).TotalMilliseconds >= arrDelay) {
                    lastAutoShiftTime = currentTime;
                    MovePieceRight();
                }
            }
        }

        void OnKeyDown(KeyEventArgs e) {
            if (e.KeyCode == Keys.Left) {
                isLeftPressed = true;
                leftPressTime = DateTime.Now;
            } else if (e.KeyCode == Keys.Right) {
                isRightPressed = true;
                rightPressTime = DateTime.Now;
            }
        }

        void OnKeyUp(KeyEventArgs e) {
            if (e.KeyCode == Keys.Left) {
                isLeftPressed = false;
                isDASActive = false;
            } else if (e.KeyCode == Keys.Right) {
                isRightPressed = false;
                isDASActive = false;
            }
        }

        */

    }
}
