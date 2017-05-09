using System;
using System.Collections.Generic;
using System.Linq;

namespace minesweeper
{
    internal class Program
    {
        private static readonly Random Random = new Random();

        private static void Main(string[] args)
        {
            //var a = CreateMineLocations(10, 400);
            var a = CreateMineField(30, 30, 10);
            Console.WriteLine("");
            WriteGrid(a);
        }

        private static void WriteGrid(int[,] a)
        {
            var maxRows = a.GetUpperBound(0);
            var maxColumns = a.GetUpperBound(1);

            for (var row = 0; row <= maxRows; row++)
            {
                for (var column = 0; column <= maxColumns; column++)
                {
                    var value = a[row, column];

                    Console.Write($"{(value == -1 ? "*" : value.ToString()).PadLeft(2, ' ')}  ");
                }
                Console.WriteLine();
            }
        }

        private static int[,] CreateMineField(int rows, int columns, int numberOfMines)
        {
            var totalMines = numberOfMines;
            var mineLocations = CreateMineLocations(totalMines, rows * columns).OrderBy(o => o).ToList();

            var mineField = new int[rows, columns];

            //Populate mines into cells

            mineLocations.ForEach(mine => { mineField[mine / rows, mine % columns] = -1; });

            var adjacentCells = new[,]
            {
                {0, 1}, {0, -1}, // Side to Side
                {1, 0}, {-1, 0}, // Top and bottom
                {-1, -1}, {-1, 1}, // top corners
                {1, -1}, {1, 1} // Bottom Corners
            };


            //Populate total mine count based on adjacent cells
            for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
            {
                if (mineField[r, c] == -1) // skip mines
                    continue;

                var numMines = 0;

                for (var i = 0; i <= adjacentCells.GetUpperBound(0); i++)
                {
                    var rOffset = adjacentCells[i, 1];
                    var cOffset = adjacentCells[i, 0];

                    //Don't count off the board
                    if (r + rOffset > rows - 1 || r + rOffset < 0)
                        continue;

                    if (c + cOffset > columns - 1 || c + cOffset < 0)
                        continue;

                    //Don't count non-mine spaces
                    if (mineField[r + rOffset, c + cOffset] != -1)
                        continue;

                    numMines++;
                }
                mineField[r, c] = numMines;
            }

            return mineField;
        }

        private static List<int> CreateMineLocations(int totalMines, int totalCells)
        {
            var locations = new List<int>();


            if (totalMines > totalCells)
                return locations;

            while (locations.Count() < totalMines)
            {
                var location = Random.Next(0, totalCells - 1);

                if (!locations.Contains(location))
                    locations.Add(location);
            }

            return locations;
        }
    }
}