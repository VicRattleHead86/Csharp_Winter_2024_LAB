/*
 * Giezar Panaligan
 * Lab 2
 * 
 *  Feb 5, 2024
 *  Lab 2 - Vintage Pong
 * 
 * Pseudo Code
 *      Create CDrawer Canvas with the desired of 800 by 600 screen size
 *      Set all the variables for the game to be used when the game run
 *      create a main loop of the game under it will be a sub loop and other branches
 *      Load the game by displaying the paddle, ball, and the incrementing imaginary velocity of the ball when hit walls or paddle
 *          Calculate the ball velocity and initialize the velocity based on users choice
 *          While in game play a score will be displayed in the middle on the canvas
 *          Increment the score for each of the successful bounce
 *      Prompt Play Again or Quit and show the final score
 *      If player chooses to play again all of the variables required to make the loop works for the game
 *      including the calculations will be reset or initialized.
 *      if the player chooses to quit the window will close.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using GDIDrawer;

namespace CMPE1300_LAB_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int iScreenXSize = 800;                                                     // Set Width size of GDIDrawer          
            int iScreenYSize = 600;                                                     // Set Height size of GDIDrawer
            int iScale = 5;                                                             // Set Scale size
            int iScaledWidth = iScreenXSize / iScale;                                   // the pixels of window width / Scale size
            int iScaledHeight = iScreenYSize / iScale;                                  // the pixels of window height / Scale size
            int iBallX = 0;                                                             // Set the initial position of the ball in X
            int iBallY = 0;                                                             // Set the initial position of the ball in Y
            int iBallVelocityX = 0;                                                     // Set Velocity of the ball in X
            int iBallVelocityY = 0;                                                     // Set Velocity of the ball in Y
            int iBallSize = 2;                                                          // Set the size of ball
            int iScore = 0;                                                             // initialize the score to 0
            int iPaddleSize = 10;                                                       // Set the size of paddle
            bool bValidClick = false;                                                   // Set the game click value
            bool bQuit = false;                                                         // Set the game quit value
            bool bPlayGame = false;                                                     // Set the game play
            bool bNotContinous = true;                                                  // Set var for Canvas to load the window                                      
            bool bNotLogging = true;                                                    // Set var for Canvas for logging
            bool bNotDone = true;                                                       // Set the not done to continue the sub loop
            Point point = new Point();                                                  // to get the position of mouse click


            // Body

            // Create an new instance of GDIDrawer
            CDrawer Canvas = new CDrawer(iScreenXSize, iScreenYSize, bNotContinous, bNotLogging);       // Create a GIDDrawer window
            Canvas.Scale = iScale;                                                                      // assign vlaue for the scale

            // Borders
            for (int i = 0; i < 160; i++)
            {
                Canvas.SetBBScaledPixel(i, 0, Color.Cyan);                                               // top border
                Canvas.SetBBScaledPixel(i, 119, Color.Cyan);                                             // bottom border

            }

            for (int i = 0; i < 120; i++)
            {
                Canvas.SetBBScaledPixel(159, i, Color.Cyan);                                             // right side border
            }

            // Main loop of the game
            do
            {

                iBallX = 2;                                                             // reset or set the ball position in X
                iBallY = 1;                                                             // reset or set the ball position in Y
                iBallVelocityX = 2;                                                     // reset or set the Velocity of Ball in X
                iBallVelocityY = 2;                                                     // reset or set the Velocity of Ball in Y
                iScore = 0;                                                             // reset or set the total score
                iPaddleSize = 10;                                                       // set the size of paddle
                bValidClick = false;                                                    // the game click to false
                bQuit = false;                                                          // reset or set the game to false
                bNotDone = true;                                                        // game continue flag set
                bPlayGame = false;                                                      // game play set

                while (bNotDone)
                {
                    
                    // Update the ball speed
                    iBallX += iBallVelocityX;                                           // Adjust according to your desired speed
                    iBallY += iBallVelocityY;                                           // Adjust according to your desired speed


                    // Use for Displaying paddle / Paddle condition position
                    Canvas.GetLastMousePositionScaled(out point);
                    if (point.Y < iPaddleSize / 2)
                    {
                        point.Y = 2 + iPaddleSize / 2;
                    }
                    else if (point.Y + iPaddleSize / 2 > iScaledHeight)
                    {
                        point.Y = iScaledHeight - 2 - iPaddleSize / 2;
                    }

                    // Display the actual paddle of the game
                    Canvas.AddLine(1, point.Y - iPaddleSize / 2, 1, point.Y + iPaddleSize / 2, Color.Red, iPaddleSize);

                    // Condition for the ball hit in X
                    if (iBallY > iScaledHeight - iBallSize - 2 || iBallY < 2)
                    {
                        iBallVelocityY *= -1;
                    }

                    // Condition for the ball hit in Y
                    if (iBallX > iScaledWidth - iBallSize - 2)
                    {
                        iBallVelocityX *= -1;
                    }

                    // Make the ball bounce once hit by the paddle
                    if (iBallX < 2)
                    {
                        if (Math.Abs(point.Y - iBallY) <= iPaddleSize / 2)
                        {
                            iBallVelocityX *= -1;
                            iScore += 1;
                        }
                        else { bNotDone = false; }
                    }

                    // Diplay the ball
                    Canvas.AddRectangle(iBallX, iBallY, iBallSize, iBallSize, Color.Green);
                    
                    // Display the score
                    Canvas.AddText($"{iScore}", 30, iScaledWidth / 2, iScaledHeight / 2, 0, 0, Color.Gray);

                    // Call canvas render instance
                    Canvas.Render();

                    // Sleep for a short duration to control the speed of the animation
                    Thread.Sleep(20);

                    // Clear the canvas for next frame
                    Canvas.Clear();
                }

                // Display Final Score
                Canvas.AddText($"Final Score: {iScore}", 30, iScaledWidth / 2, iScaledHeight / 2, 0, 0, Color.Gray);


                // Display the button of Play Again
                Canvas.AddRectangle(90, 100, 20, 10, Color.Green);
                Canvas.AddRectangle(91, 101, 18, 8, Color.Black);
                Canvas.AddText($"Play Again", 14, 90, 100, 20, 10, Color.Green);


                // Display the buttom of Quit
                Canvas.AddRectangle(130, 100, 20, 10, Color.Gray);
                Canvas.AddRectangle(131, 101, 18, 8, Color.Black);
                Canvas.AddText($"Quit", 14, 130, 100, 20, 10, Color.Gray);

                // Set point in X and Y for the mouse click
                do
                {
                    if (Canvas.GetLastMouseLeftClickScaled(out point))
                    {
                        if ((point.X >= 90) && (point.X <= 110) && (point.Y >= 100) && (point.Y <= 110))
                        {
                            bValidClick = true;
                            bPlayGame = true;

                        }

                        if ((point.X >= 130) && (point.X <= 150) && (point.Y >= 100) && (point.Y <= 110))
                        {
                            bValidClick = true;
                            bQuit = true;
                        }
                    }
                }
                while (!bValidClick);
                Canvas.Clear();
            
            } // Main loop
            while (bPlayGame); 
        }
    }
}
