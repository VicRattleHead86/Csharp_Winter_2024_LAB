/*
 * Giezar Panaligan
 * Lab 1
 * 
 * Feb 5, 2024
 *  Lab 1 - Cell Phone Data Cost Calculator
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPE1300_LAB_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sTitle = "Lab 1 - Cell Phone Data Cost Calculator\n";                                                                                                  // variable for title
            long lNumberOfBytesUsed;                                                                                                                                      // variable for number of bytes
            bool bValid = false;                                                                                                                                          // boolean variable for input checking

            // Variable Labels for showing the table
            string lblAmt = "Amount";                                                                                                                                     // variable for showing the Amount label                                                                              
            string sCstUnit = "Cost/Unit";                                                                                                                                // variable for showing the Cost/Unit label
            string stUnit = "Unit";                                                                                                                                       // variable for showing the Unit
            string sblTotal = "Total";                                                                                                                                    // variable for showing the Total
            string SBarLine = "----------";                                                                                                                               // variable for showing the the dashes
            string sSubTotallbl = "SubTotal";                                                                                                                             // variable for showing the Subtotal label
            string s911AccessFeelbl = "911 Access Fee";                                                                                                                   // variable for showing the 911 Acess fee
            string sSystemAccessFeelbl = "System Access Fee";                                                                                                             // variable for showing the Sysetem Access fee
            string sTotalBeforelbl = "Total before GST";                                                                                                                  // variable for showing the Total before GST
            string sGSTlbl = "GST";                                                                                                                                       // variable for showing the GST
            string sTotalForDatalbl = "Total for Data:";                                                                                                                  // variable for showing the Total for Data
            string stemp = "";                                                                                                                                            // variable for showing the temporary variable as null holder
            string sblUnitGB = "GB";                                                                                                                                      // variable for showing the GB label
            string lblUnitMB = "MB";                                                                                                                                      // variable for showing the MB label
            string lblUnitKB = "KB";                                                                                                                                      // variable for showing the KB label
            string lbBytes = "Bytes";                                                                                                                                     // variable for showing the Bytes

            // Variables for calculation of GB, MB, KB and Bytes
            long lAmtGB = 0;                                                                                                                                              // variable for calculation of number of GB
            long lAmtMB = 0;                                                                                                                                              // variable for calculation of number of MB
            long lAmtKB = 0;                                                                                                                                              // variable for calculation of number of KB
            long iLeft = 0;                                                                                                                                               // variable for calculation of number of what's left
            long lBytes = 0;                                                                                                                                              // variable for calculation of number of bytes

            // Variable for calculation of costs and totals
            double dGBTotal = 0.0;                                                                                                                                        // varialbe for calculation of GB total
            double dCostUnitGB = 12.00;                                                                                                                                   // varialbe for calculation of CostUnit GB
            double dCostUnitMB = 0.25;                                                                                                                                    // varialbe for calculation of CostUnit MB
            double dCostUnitkB = 0.02;                                                                                                                                    // varialbe for calculation of CostUnit KB
            double dCostUniBytes = 0.01;                                                                                                                                  // varialbe for calculation of CostUnit Bytes
            double dMBTotal = 0.00;                                                                                                                                       // varialbe for calculation of MB total
            double dKBTotal = 0.00;                                                                                                                                       // varialbe for calculation of KB total
            double dBTotal = 0.00;                                                                                                                                        // varialbe for calculation of Bytes total
            double dSubTotal = 0.00;                                                                                                                                      // varialbe for calculation of sub total
            double d911AccessFee = 0.95;                                                                                                                                  // varialbe to hardcode 911 access fee val
            double dSystemAccessFee = 6.95;                                                                                                                               // varialbe to hardcode system access fee
            double dTotalBeforeGST;                                                                                                                                       // varialbe for calculation of Total before GST
            double dGST;                                                                                                                                                  // varialbe for calculation of GST
            double dTotalForData = 0.0;                                                                                                                                   // varialbe for calculation of Total for data

            // to display title in the middle
            Console.CursorLeft = (Console.WindowWidth - sTitle.Length) / 2;
            Console.WriteLine(sTitle);

            // Prompt user for data
            Console.Write("Enter the number of bytes used: ");
            bValid = long.TryParse(Console.ReadLine(), out lNumberOfBytesUsed);
            Console.WriteLine();

            if (bValid)
            {
                // Calculate how many GB MB bytes
                iLeft = lNumberOfBytesUsed;
                lAmtGB = iLeft / 1073741824;                                                                                                                                // how many GB
                iLeft %= 1073741824;                                                                                                                                        // how many left
                lAmtMB = iLeft / 1048576;                                                                                                                                   // how many MB
                iLeft %= 1048576;                                                                                                                                           // how many left
                lAmtKB = iLeft / 1024;                                                                                                                                      // how many KB
                iLeft %= 1024;                                                                                                                                              // how many left
                lBytes = iLeft;                                                                                                                                             // how many Bytes

                // Calculate how much GB, MB KB and Bytes
                dGBTotal = lAmtGB * dCostUnitGB;
                dMBTotal = lAmtMB * dCostUnitMB;
                dKBTotal = lAmtKB * dCostUnitkB;
                dBTotal = lBytes * dCostUniBytes;
                // Calculate for the subtotal
                dSubTotal = dGBTotal + dMBTotal + dKBTotal + dBTotal;
                // Calcualte for total before GST
                dTotalBeforeGST = dSubTotal + d911AccessFee + dSystemAccessFee;
                // Calculate for GST value
                dGST = dTotalBeforeGST * 0.05;
                // Calculate for total data
                dTotalForData = dTotalBeforeGST + dGST;

                // Display a color yellow label for table
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{lblAmt,-10}{stUnit,-10}{sCstUnit,-10}{sblTotal,-10}\n");
                Console.ResetColor();
                // Display all the data that will represents the table values by it's labeling
                Console.WriteLine($"{lAmtGB,-10}{sblUnitGB,-10}{dCostUnitGB,-10:C2}{dGBTotal:C2}");                                                                     // Display data for GB
                Console.WriteLine($"{lAmtMB,-10}{lblUnitMB,-10}{dCostUnitMB,-10:C2}{dMBTotal:C2}");                                                                     // Display data for MB
                Console.WriteLine($"{lAmtKB,-10}{lblUnitKB,-10}{dCostUnitkB,-10:C2}{dKBTotal:C2}");                                                                     // Display data for KB
                Console.WriteLine($"{lBytes,-10}{lbBytes,-10}{dCostUniBytes,-10:C2}{dBTotal:C2}");                                                                      // Display data for Bytes
                Console.WriteLine($"{stemp,-30}{SBarLine}");                                                                                                            // Display Bar line
                Console.WriteLine($"{sSubTotallbl,-30}{dSubTotal:C2}\n");                                                                                               // Display subtotal
                Console.WriteLine($"{s911AccessFeelbl,-30}{d911AccessFee:C2}\n");                                                                                       // Display 911 Access fee
                Console.WriteLine($"{sSystemAccessFeelbl,-30}{dSystemAccessFee:C2}\n");                                                                                 // Display system access fee
                Console.WriteLine($"{sTotalBeforelbl,-30}{dTotalBeforeGST:C2}\n");                                                                                      // Display total before GST
                Console.WriteLine($"{sGSTlbl,-30}{dGST:C2}");                                                                                                           // Display GST
                Console.WriteLine($"{stemp,-30}{SBarLine}");                                                                                                            // Display bar line
                Console.Write($"{sTotalForDatalbl,-30}{dTotalForData:C2}\n");                                                                                           // Display total for data
            }
            // Prompt user that invalid input has been entered.
            else
            {
                Console.Write("Please enter a valid value.\n");
            }
            Console.ReadKey();
        }
    }
}
