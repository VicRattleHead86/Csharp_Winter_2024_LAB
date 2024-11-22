/*
 * Giezar Panaligan Lab 3 - Graphing Calculator
 * 
 * Mar 25, 2024
 * CMPE1300 - Lab 3
 * 
 * Pseudo Code
 *      Display titles and Prompt the user to enter the following values coefficient a b c lower limit and upper limit values
 *          Validate the values entered with the following criteria
 *              a should not be 0 or any letters
 *              b should not be any letters
 *              c should not be any letters
 *              lower limit should not be any letters
 *              upper limit should not be any letters should be higher than lower limit
 *          validate the above input to make sure it complies with requirements as valid inputs
 *          
 *          calculate the quadratic expression and use this value as Y 
 *          calculate the lower and upper limit 
 *          the program will draw the X and Y coordinates that fits the screen
 *                    
 *          make a loop based on the calculated values by incrementing by each +=0.02
 *          for each of every instances of the loop
 *          create a rectacngle using this parameters AddRectangle((int)(i*50+400), (int)(-dFX*50+300),1,1,Color.Yellow)
 *          the program will draw the graph based on the values that has been supplied by the user
 *          
 *          Prompt the user to run the program again yes or no
 *      
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;

namespace CMPE1300_LAB3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int iScreenXSize = 800;                                                                     // Set Width size of GDIDrawer   160       
            int iScreenYSize = 600;                                                                     // Set Height size of GDIDrawer  120
            int iScale = 5;                                                                             // Set Scale size
            int iScaledWidth = iScreenXSize / iScale;                                                   // the pixels of window width / Scale size
            int iScaledHeight = iScreenYSize / iScale;                                                  // the pixels of window height / Scale size
            bool bNotContinous = true;                                                                  // Set var for Canvas to load the window                                      
            bool bNotLogging = true;                                                                    // Set var for Canvas for logging
            string Prompt1 = "Enter a value for ";                                                      // string to Prompt the user coefficent
            string Prompt2 = "Enter the ";                                                              // string to prompt the user to enter lower and upper limit
            bool bValid = false;

            // Create an new instance of GDIDrawer
            CDrawer Canvas = new CDrawer(iScreenXSize, iScreenYSize, bNotContinous, bNotLogging);        // Create a GIDDrawer window
                                                                                                         //Canvas.Scale = iScale;
            do
            {
                // variables for reset
                bool bExit = false;                                                                     // flag for exiting program
                Console.Clear();                                                                        // clear the console
                Canvas.Clear();                                                                         // clear the canvas
                string sTitle = "Quadratic Equation Graph\n";                                           // string for Displays the title
                DiplayTitle(sTitle);                                                                    // call methods that display title
                double dValueA = 0;                                                                     // var for coefficient a
                double dValueB = 0;                                                                     // var for coefficient b
                double dValueC = 0;                                                                     // var for coefficient c
                double dLowerLimit = 0;                                                                 // var for lower limit
                double dUpperLimit = 0;                                                                 // var for upper limit

                // Method that capture the values from user
                GetValue(out dValueA, out dValueB, out dValueC, out dLowerLimit, out dUpperLimit, out bValid, Prompt1, Prompt2);
                
                // Method draw the graph accross the X and Y coordinate 
                DrawGraph(dValueA, dValueB, dValueC, dLowerLimit, dUpperLimit, ref Canvas);
                
                // Method that draw X and Y coordinate
                XYcoordinates(50, 50, ref Canvas);
            }
            while ((YesNo("Run again? yes or no") == "yes"));
        }

        // Yes and No prompt for user to continue adding tracks
        static string YesNo(string sRepeat)
        {
            do
            {
                Console.Write("\nRun again? ");
                sRepeat = Console.ReadLine().ToLower();

                if (sRepeat != "yes" && sRepeat != "no")
                {
                    Console.WriteLine("\nYou must respond with yes or no.");
                }
                else if (sRepeat == "no")
                {
                    Console.WriteLine("\nBye!");
                }
            }
            while (sRepeat != "yes" && sRepeat != "no");

            return sRepeat;
        } // YesNo

        //Get the hardcoded string and display as title
        static void DiplayTitle(string sTitle)
        {
            Console.CursorLeft = (Console.WindowWidth - sTitle.Length) / 2;
            Console.WriteLine(sTitle);
        } // DiplayTitle

        // Method that draw X and Y coordinate
        static void XYcoordinates(int X50_ctr, int Y50_ctr, ref CDrawer canvas)
        {
            // x Coordinates
            for (int i = 0; i < 800; i++)
            {
                canvas.SetBBScaledPixel(i, 300, Color.Green);                                               // X axis

                if (X50_ctr == 50)
                {
                    canvas.AddLine(i, 297, i, 305, Color.Red);                                              // X small lines
                }

                X50_ctr--;

                if (X50_ctr == 0)
                {
                    X50_ctr = 50;
                }
            }

            // y Coordinates
            for (int i = 0; i < 600; i++)
            {
                canvas.SetBBScaledPixel(400, i, Color.Green);                                               // Y axis

                if (Y50_ctr == 50)
                {
                    canvas.AddLine(397, i, 405, i, Color.Red);                                               // X small lines
                }

                Y50_ctr--;

                if (Y50_ctr == 0)
                {
                    Y50_ctr = 50;
                }
            }
            return;
        }

        // Method that capture the values from user
        static public void GetValue(out double dValueA, out double dValueB, out double dValueC, out double dLowerLimit, out double dUpperLimit, out bool bValid, string Prompt1, string Prompt2)
        {
                // Get value for A
                do
                {
                    Console.Write($"{Prompt1}a: ");
                    bValid = double.TryParse(Console.ReadLine(), out dValueA);

                    if (!bValid)
                    {
                        Console.WriteLine("\nYou have entered an invalid double value, Please try again!");
                    }
                    else if (dValueA == 0)
                    {
                        Console.WriteLine("\nValue cannot be 0, Please try again!");
                        bValid = false;
                    }
                }
                while (!bValid);

                // Get value for B
                do
                {
                    Console.Write($"{Prompt1}b: ");
                    bValid = double.TryParse(Console.ReadLine(), out dValueB);

                    if (!bValid)
                    {
                        Console.WriteLine("\nYou have entered an invalid double value, Please try again!");
                    }
                }
                while (!bValid);

                // Get value for C
                do
                {
                    Console.Write($"{Prompt1}c: ");
                    bValid = double.TryParse(Console.ReadLine(), out dValueC);

                    if (!bValid)
                    {
                        Console.WriteLine("\nYou have entered an invalid double value, Please try again!");
                    }
                }
                while (!bValid);

                do
                {
                // Get value for lower limit
                Console.Write($"\n{Prompt2}lower limit : ");
                    bValid = double.TryParse(Console.ReadLine(), out dLowerLimit);

                    if (!bValid)
                    {
                        Console.WriteLine("\nYou have entered an invalid double value, Please try again!");
                    }
                }
                while (!bValid);

                do
                {
                // Get value for upper limit
                Console.Write($"{Prompt2}upper limit : ");
                    bValid = double.TryParse(Console.ReadLine(), out dUpperLimit);

                    if (!bValid)
                    {
                        Console.WriteLine("\nYou have entered an invalid double value, Please try again!");
                    }
                    else if (dUpperLimit <= dLowerLimit)
                    {
                        Console.WriteLine("The Upper limit must be greater than Lower limit value");
                        bValid = false;
                    }
                }
                while (!bValid);
        }
        // Methods that calculate the quadratic expression
        static public double Quadratic(double dCoEffA, double dCoEffB, double dCoEffC, double dXValue)
        {
            double dFx = dCoEffA * Math.Pow(dXValue, 2) + dCoEffB * dXValue + dCoEffC;
            return dFx;
        }

        // Method that draw the graph
        static public void DrawGraph(double dCoeffiA, double dCoeffiB, double dCoeffiC, double dLowValueX, double dHighValueX, ref CDrawer canvas)
        {

            for (double i = dLowValueX; i < dHighValueX; i+=0.02)
            {
                double dFX = Quadratic(dCoeffiA, dCoeffiB, dCoeffiC, i);
                canvas.AddRectangle((int)(i*50+400), (int)(-dFX*50+300),1,1,Color.Yellow);
            }
        }
    }
}
