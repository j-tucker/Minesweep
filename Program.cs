using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{

    class Program
    {

        static void Main(string[] args)
        {
            //var a = CreateMineLocations(10, 400);
            var a = CreateMineField(30, 30, 100);
            Console.WriteLine("");
            WriteGrid(a);

        }

        static void WriteGrid(int[,] a)
        {

            int bound0 = a.GetUpperBound(0);
            int bound1 = a.GetUpperBound(1);

            for (int variable1 = 0; variable1 <= bound0; variable1++)
            {
                for (int variable2 = 0; variable2 <= bound1; variable2++)
                {
                    var value = a[variable1, variable2];
                    // Console.Write($"{value.ToString().PadLeft(2, ' ')}  ");

                    Console.Write($"{(value == -1 ? "*" : value.ToString()).PadLeft(2, ' ')}  ");

                }
                Console.WriteLine();
            }
        }

        static int[,] CreateMineField(int rows, int columns, int numberOfMines)
        {
            int totalCells = rows * columns;
            int totalMines = numberOfMines; // Convert.ToInt32(Math.Ceiling(totalCells * (difficulty * .01)));

            //Assign mines to cells at random

            var mineField = new int[rows, columns];



            var mineLocations = CreateMineLocations(totalMines, totalCells);
            int currentCell = 0;

            //Populate mines into cells
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    currentCell++;
                    if (mineLocations.Contains(currentCell))
                        mineField[r, c] = -1; // -1 = You hit a mine!                    
                    else
                        mineField[r, c] = 0; // 0 = No Mine;
                }
            }


            var adjacentCells = new int[,] {
                                                {0,1},{0,-1}, // Side to Side
                                                {1,0},{-1,0}, // Top and bottom
                                                {-1,-1},{-1,1}, // top corners
                                                {1,-1},{1,1},   // Bottom Corners
                                            };


            //Populate total mine count based on adjacent cells
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    int numMines = 0;
                    if (mineField[r, c] == -1) // skip mines
                        continue;

                    for (int i = 0; i <= adjacentCells.GetUpperBound(0); i++)
                    {
                        int cOffset = adjacentCells[i, 0];
                        int rOffset = adjacentCells[i, 1];

                        if (r + rOffset > rows - 1 || r + rOffset < 0)
                            continue;

                        if (c + cOffset > columns - 1 || c + cOffset < 0)
                            continue;

                        if (mineField[r + rOffset, c + cOffset] == -1)
                        {
                            numMines++;
                            mineField[r, c] = numMines;
                        }

                    }
                }
            }

            return mineField;
        }


        static List<int> CreateMineLocations(int totalMines, int totalCells)
        {

            var locations = new List<int>();


            if (totalMines > totalCells)
                return locations;

            while (locations.Count() < totalMines)
            {
                var random = new Random();
                var location = random.Next(1, totalCells);

                if (!locations.Contains(location))
                    locations.Add(location);
            }

            return locations;
        }
    }
}
