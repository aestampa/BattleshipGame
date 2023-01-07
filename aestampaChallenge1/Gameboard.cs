using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aestampaBattleship
{
    public class Gameboard
    {
        // A 10 x 10 grid
        private char[,] grid = new char[10, 10];
        
        // Grid object that sets up gameboard
        public char[,] Grid 
        { 
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }

        // Acquires target location and places a character in that location
        public void SetChar(int row, int col, char aChar)
        {
            if (row < grid.GetLength(0) && col < grid.GetLength(1))
            {
                grid[row, col] = aChar;
            } 
        }

        // Fill board with spaces
        public void FillGrid()
        {
            // Retiterate through grid and fill them with spaces
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = ' '; 
                }
            }
        }

        // If player activates hacks, draw the ships on the gameboard
        public void DrawShips(char[,] ships)
        {
            char shipChar = 'S'; // char representing ship

            // Retiterate through shipGrid to find a ship
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (ships[row, col] == 'H') // If ship grid contains a ship char
                    {
                        grid[row, col] = shipChar; // Draw ship char on locations of ship
                    } 
                }
            }
        }

        // If player hits a ship, draw O at their target location
        public void DrawHit(int x, int y)
        {
            grid[x, y] = 'O';
        }

        // If player misses ship, draw X at their target location
        public void DrawMiss(int x, int y)
        {
            grid[x, y] = 'X';
        }

        // Displays the board
        public void Display()
        {
            char rowChar = 'A'; // First row leter index
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                DrawLine(); // Draw a line after each row
                Console.Write(rowChar + " "); // Write a letter for each row
                rowChar++; // Increment row letter
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                 
                    Console.Write($"| {grid[row, col]} "); // Draw each "space" of the board
                }

                Console.WriteLine("|"); // Draw a line separating each space
            }
            DrawLine(); // Draw final line
            DrawColumnNumbers(); // Draw column numbers 
        }

        // Draws each line of board for formatting
        private void DrawLine()
        {
            Console.Write("  ");
            for (int i = 0; i < grid.GetLength(1) * 4 + 1; i++)
            {
                Console.Write($"-");
            }
            Console.WriteLine();
        }

        // Draws column numbers of board
        private void DrawColumnNumbers()
        {
            Console.Write("   ");
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                Console.Write($" {i+1}  ");
            }
            Console.WriteLine();
        }
    }



}