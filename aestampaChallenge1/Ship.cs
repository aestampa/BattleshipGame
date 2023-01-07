using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aestampaBattleship
{
    public class Ship
    {
        private int length; // length of ship
        private int direction; // direction of ship (horziontal or vertical)
        private int bowX; // x location of bow of ship
        private int bowY; // y location of bow of ship
        private int sternX; // x location of stern of ship
        private int sternY; // y locaiton of stern of ship
        private int countX = 0; // size of x coordinates array
        private int countY = 0; // size of y coordinates array
        private int[] xCoordinates = new int[0]; // x coordinates of ship
        private int[] yCoordinates = new int[0]; // y coordinates of ship

        // Set length of ship
        public void SetLength(int shipLength)
        {
            this.length = shipLength;
            SetBow(); // set bow of ship
            setXCoordinates(length); // set x coordinates of ship
            setYCoordinates(length); // set y coordinates of ship
        }

        // Set location of bow of ship
        public void SetBow()
        {
            int space = length - 1; // number of spaces to be filled after the bow of ship
            Random random = new Random(); // randomize location of bow
            bowX = random.Next(space, 9); // x location of bow
            bowY = random.Next(0, 9 - space); // y location of bow
            SetStern(); // set stern of ship
            SetDirection(); // set direction of ship
        }

        // Set x coordinates of ship
        public void setXCoordinates(int length)
        {
            this.xCoordinates = new int[10];
        }

        // Set y coordinates of ship
        public void setYCoordinates(int length)
        {
            this.yCoordinates = new int[10];
        }

        // Fill array of x coordinates of ship
        public void FillXCoordinates(int x)
        {
            xCoordinates[countX] = x;
            countX++;
        }


        // Fill array of y coordinates of ship
        public void FillYCoordinates(int y)
        {
            yCoordinates[countY] = y;
            countY++;
        }

        // Sets x and y coordinates of stern
        private void SetStern()
        {

            int space = length - 1; // how many spaces of the board will be filled after bow
            if (direction == 0) // if ship is horizontal
            {
                this.sternX = bowX - space;
                this.sternY = bowY; // y coordinate of stern is static
            } else // if ship is vertical
            {
                this.sternX = bowX; // x coordinate of stern is static 
                this.sternY = bowY + space;
            }
        }

        // Sets direction of ship
        private void SetDirection()
        {
            this.direction = Direction();
        }

        // Returns array of x coordinate of ship location
        public int[] GetXCoordinates()
        {
            return xCoordinates;
        }

        // Returns array of y coordinates of ship location
        public int[] GetYCoordinates()
        {
            return yCoordinates;
        }

        // Returns x coordinate of bow
        public int GetBowX()
        {
            return bowX;
        }

        // Returns y coordinate of bow
        public int GetBowY()
        {
            return bowY;
        }

        // Returns x coordinate of stern
        public int GetSternX()
        {
            return sternX;
        }

        // Returns y coordinate of stern
        public int GetSternY()
        {
            return sternY;
        }

        // Returns direction of ship
        public int GetDirection()
        {
            return direction;
        }

        // Returns length of ship
        public int GetLength()
        {
            return length;
        }

        // Sets direction of ship
        public int Direction()
        {
            // Randomize direcion
            Random rnd = new Random();
            int num = rnd.Next();

            if (num % 2 == 0)
            {
                return 0; // return horizontal direction
            } else
            {
                return 1; // return vertical direction
            }
        }

    }
}