using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TamasBodor_Probafeladat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProcessDataFromTXT(false);
            Console.ReadKey();
        }

        /// <summary>
        /// Read the data from the file called "input.txt" an process it as it was requested.
        /// </summary>
        /// <param name="showRiskLevels">If this parameter is "false", than the console will only show the numbers which we will need to calculate with. In this case the program will highlight these numbers with green. If this parameter is "true", than the program will also show the calculated risk level (number + 1) with red.</param>
        static void ProcessDataFromTXT(bool showRiskLevels)
        {
            StreamReader reader = new StreamReader("input.txt");
            string[] lines = reader.ReadToEnd().Split('\n');
            char[,] characterArray = new char[lines.Length, lines[0].Length];

            List<Tuple<int, int, char>> valuesWithCoordinates = new List<Tuple<int, int, char>>();

            for (int col = 0; col < lines.Length; col++)
            {
                char[] rowElements = lines[col].ToCharArray();
                for (int row = 0; row < rowElements.Length; row++)
                {
                    valuesWithCoordinates.Add(new Tuple<int, int, char>(col, row, rowElements[row]));
                    characterArray[col, row] = rowElements[row];
                }
            }

            int up, down, left, right = 0;
            List<char> values = new List<char>();
            List<int> riskLevels = new List<int>();
            for (int col = 0; col < lines.Length; col++)
            {
                char[] rowElements = lines[col].ToCharArray();
                for (int row = 0; row < rowElements.Length; row++)
                {
                    up = col - 1;
                    down = col + 1;
                    left = row - 1;
                    right = row + 1;
                    char myNumberAsChar = rowElements[row];
                    bool justAWhiteNumber = true;

                    //All the numbers in the middle section:
                    if (up != -1 && left != -1 && down != lines.Length && right != rowElements.Length)
                    {
                        if (myNumberAsChar < characterArray[up, row] && myNumberAsChar < characterArray[down, row] && myNumberAsChar < characterArray[col, left] && myNumberAsChar < characterArray[col, right])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //First row, except the corners
                    if (up.Equals(-1) && left != -1 && right != rowElements.Length)
                    {
                        if (myNumberAsChar < characterArray[col, left] && myNumberAsChar < characterArray[col, right] && myNumberAsChar < characterArray[down, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //Last row, except the corners
                    if (down.Equals(lines.Length) && left != -1 && right != rowElements.Length)
                    {
                        if (myNumberAsChar < characterArray[col, left] && myNumberAsChar < characterArray[col, right] && myNumberAsChar < characterArray[up, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //Right section without corners
                    if (right.Equals(rowElements.Length) && up != -1 && down != lines.Length)
                    {
                        if (myNumberAsChar < characterArray[up, row] && myNumberAsChar < characterArray[down, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //Left section wihtout corners
                    if (left.Equals(0) && up != -1 && down != lines.Length)
                    {
                        if (myNumberAsChar < characterArray[up, row] && myNumberAsChar < characterArray[down, row] && myNumberAsChar < characterArray[col, right])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //Corners (Top, left)
                    if (row.Equals(0) && col.Equals(0))
                    {
                        if (myNumberAsChar < characterArray[col, right] && myNumberAsChar < characterArray[down, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    } 
                    //Corners (Top, right)
                    if (row.Equals(rowElements.Length) && col.Equals(0))
                    {
                        if (myNumberAsChar < characterArray[col, left] && myNumberAsChar < characterArray[down, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //Corners (Bottom, left)
                    if (row.Equals(0) && col.Equals(lines.Length))
                    {
                        if (myNumberAsChar < characterArray[col, right] && myNumberAsChar < characterArray[up, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    //Corners (Bottom, right)
                    if (row.Equals(rowElements.Length) && col.Equals(lines.Length))
                    {
                        if (myNumberAsChar < characterArray[col, left] && myNumberAsChar < characterArray[up, row])
                        {
                            values.Add(myNumberAsChar);
                            riskLevels.Add(Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(myNumberAsChar);
                            if (showRiskLevels)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("(" + (Convert.ToInt32(char.GetNumericValue(myNumberAsChar)) + 1).ToString() + ")");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                            justAWhiteNumber = false;
                        }
                    }
                    if (justAWhiteNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(myNumberAsChar);
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nFinal result: " + riskLevels.Sum());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n=== Press any key to close the program! ===");
        }
    }
}
