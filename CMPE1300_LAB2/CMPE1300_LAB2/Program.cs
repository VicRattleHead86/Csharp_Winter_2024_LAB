/*
 * Giezar Panaligan
 * Lab 2
 * 
 * Feb 24, 2024
 *  Lab 2 - Vintage Pong
 * 
 * Pseudo Code
 *      Display titles
 *      prompt for number of bytes
 *      read the input given
 *      
 *      calculate how many of each bytes equivalent to GB, MB, KB, and bytes using integer math
 *      calculate cost for the each bytes equivalent to GB, MB, KB, and bytes
 *      calculate the gst sub total, total before GST, GST and Total for Data. 
 *      
 *      Display the the table showing Amount, Unit, Cost/Unit, Total
 *      Display SubTotal, 911 Access Fee, System Access Fee, Total before GST, GST, Total for GST. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using GDIDrawer;

namespace BouncyBall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int iWindowWidth = 500;
            int iWindowHeight = 600;
            int iScale = 5;
            int iScaledWidth = iWindowWidth / iScale;
            int iScaledHeight = iWindowHeight / iScale;
            int iBallVelocityX = 1;                         // make zero at first
            int iBallVelocityY = 0;
            int iBallPositionX = 50;
            int iBallPositionY = 5;
            int iBallSizeWidth = 20;
            int iBallSizeHeight = 20;
            int iheightCounter = 0;
            int iTop = 0;
            int iLeft = 0;

            bool bValidClick = false;
            bool bAgain = false;
            bool bQuit = false;
            Point point = new Point();

            bool bNotContinous = false;
            bool bNotDone = true;




            CDrawer Canvas = new CDrawer(iWindowWidth, iWindowHeight, bNotContinous);
            Canvas.Scale = iScale;
            for (int i = 0; i < 120; i++)
            {
                Canvas.SetBBScaledPixel(i, 119, Color.Cyan);
                Canvas.SetBBScaledPixel(0, i, Color.Cyan);
                Canvas.SetBBScaledPixel(119, i, Color.Cyan);
            }

            do
            {
                bAgain = false;
                bQuit = false;
                bValidClick = false;
                iBallPositionX = 50;
                iBallPositionY = 5;
                do
                {
                    // check positions
                    iBallPositionY = (iBallPositionY < iTop) ? iTop : iBallPositionY;
                    iBallPositionY = (iBallPositionY > iScaledHeight - iBallSizeHeight - 1) ? iScaledHeight - iBallSizeHeight - 1 : iBallPositionY;
                    iBallPositionX = (iBallPositionX < iTop) ? iLeft : iBallPositionX;
                    iBallPositionX = (iBallPositionX > iScaledWidth - iBallSizeWidth - 1) ? iScaledWidth - iBallSizeWidth - 1 : iBallPositionX;

                    // draw Ball
                    Canvas.Clear();
                    Canvas.AddEllipse(iBallPositionX, iBallPositionY, iBallSizeWidth, iBallSizeHeight, Color.LimeGreen);
                    Canvas.Render();
                    Thread.Sleep(40);
                    bNotDone = true;

                    // move ball 
                    iBallPositionX += iBallVelocityX; // no change for at first
                    iBallPositionY += iBallVelocityY;

                    // Did it hit boarder?
                    if (iBallPositionY > iScaledHeight - iBallSizeHeight)  // bottom
                    {
                        iBallVelocityY *= -1; // change direction
                    }
                    if ((iBallPositionX > iScaledWidth - iBallSizeWidth - 1) || (iBallPositionX < iLeft + 1))  // the walls
                    {
                        iBallVelocityX *= -1; // change direction
                    }

                    // Gravity 

                    iBallVelocityY++;

                    iheightCounter = iBallPositionY;
                    if ((iheightCounter >= 102) && (iBallVelocityY == -2)) // came up with exact numbers in debug
                    {
                        bNotDone = false;
                    }

                    if (iBallVelocityY < 0)
                    {
                        iBallPositionY += 1;  // as ball goes up it is being pulled by gravity
                    }

                }
                while (bNotDone);

                // Using a mouse
                Canvas.AddRectangle(10, 10, 30, 10, Color.White);
                Canvas.AddText($"Again?", 14, 10, 10, 30, 10, Color.LightBlue);
                Canvas.AddRectangle(10, 25, 30, 10, Color.White);
                Canvas.AddText($"Quit?", 14, 10, 25, 30, 10, Color.LightBlue);
                Canvas.Render();
                do
                {
                    if (Canvas.GetLastMouseLeftClickScaled(out point))
                    {
                        if ((point.X >= 10) && (point.X <= 40) && (point.Y >= 10) && (point.Y <= 20))
                        {
                            bValidClick = true;
                            bAgain = true;

                        }
                        if ((point.X >= 10) && (point.X <= 40) && (point.Y >= 25) && (point.Y <= 35))
                        {
                            bValidClick = true;
                            bQuit = true;
                        }
                    }
                }
                while (!bValidClick);
                Canvas.Clear();
            }
            while (bAgain);

            //Console.WriteLine("Press any Key to Quit");
            //Console.ReadKey(false);
        }
    }
}
