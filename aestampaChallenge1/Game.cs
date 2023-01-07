using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aestampaBattleship
{ 
    public class Game
    {
        private char[,] shipGrid = new char[10, 10]; // Separate grid containing ships

        Gameboard board = new Gameboard(); // Gameboard

        // Ships that the game will have
        Ship destroyer1 = new Ship(); // Destroyer
        Ship destroyer2 = new Ship(); // Destroyer
        Ship submarine1 = new Ship(); // Submarine
        Ship submarine2 = new Ship(); // Submarine
        Ship battleShip = new Ship(); // Battleship
        Ship carrier = new Ship(); // Carrier

        // Runs the concept of the game
        public void Run()
        {
            int finish = 0;
            int playAgain = 0; // Indicates if user wants to play again (1 if they do not want to)
            int valid; // Indicates if user input is valid
            string answer = " "; // Y / N answer if user wants to play again
            char hacksAnswer; // Y / N answer if user wants hacks

            // Display title of game
            Console.WriteLine("Battleship");
            Console.WriteLine("---------------");

            hacksAnswer = hacks(); // Prompt user if they wants hacks

            BuildShips(); // Set up all ships' locations
            board.FillGrid(); // Set up gameboard

            // If hacks are activated
            if (hacksAnswer == 'Y' || hacksAnswer == 'y')
            {
                board.DrawShips(shipGrid); // Draw ships on gameboard
            }
            
            // Display gameboard
            board.Display();

            // Begin game by repeatedly asking player for target location until all ships are destroyed
            while (playAgain == 0)
            {
            
                PlayerTurn(); // Prompt player for target location
                
                finish = CheckShips(); // Check if all ships are destroyed
                Console.WriteLine(); // Wrtie an extra line for formatting

                if (finish == 1) // If player finished the game
                {
                    Console.WriteLine("Congratulations! All ships have been taken down."); // Display a message saying that the player won the game
                    Console.WriteLine("Would you like to play again? (Y/N)"); // Ask user if they want to play again
                    answer = Console.ReadLine(); // Read answer from user
                    valid = YesOrNo(answer); // Check if input is valid
                    while (valid == 0) // If input is invalid
                    {
                        Console.WriteLine("Invalid Input. Would you like to play again? (Y/N)"); // Handle invalid input
                        answer = Console.ReadLine(); // Read answer from user
                        valid = YesOrNo(answer); // Check if input is valid
                    }

                    if (answer[0] == 'N' || answer[0] == 'n') // If answer is no, end the game
                    {
                        Console.WriteLine("Thanks for playing!"); // Display a message that the game is ending
                        playAgain = 1; // Set playAgain integer to 1, indicating that player does not want to play again
                    } else // If answer is yes, play the game
                    {
                        Console.WriteLine("Here we go again!"); // Display a message that the player is playing the game again
                        hacksAnswer = hacks(); // Prompt user if they wants hacks

                        shipGrid = new char [10, 10]; // Reinitialize grid containing ships
                        BuildShips(); // Set up all ships' locations
                        board.FillGrid(); // Set up gameboard

                        // If hacks are activated
                        if (hacksAnswer == 'Y' || hacksAnswer == 'y')
                        {
                            board.DrawShips(shipGrid); // Draw ships on gameboard
                        }

                        // Display gameboard
                        board.Display();
                    }
                }
            }
        }

        // Returns answer if user wants hack or not
        private char hacks()
        {
            string answer; // answer
            int valid; // checks if input is valid
            Console.WriteLine("Activate Hacks? (Y / N)"); // Prompt user if they want hacks
            answer = Console.ReadLine(); // Read answer from user
            valid = YesOrNo(answer); // Check if input is valid

            // If input is invalid
            while (valid == 0)
            {
                Console.WriteLine("Invalid Input. Activate Hacks? (Y/N)"); // Prompt user again if they want hacks
                answer = Console.ReadLine(); // Read answer from user
                valid = YesOrNo(answer); // Check is input is valid
            }

            return answer[0]; // return answer
        }

        // Check if Y / N answer is valid
        public int YesOrNo(string answer)
        {
            if (answer[0] == 'Y' || answer[0] == 'y'
                || answer[0] == 'N' || answer[0] == 'n') // If answer has an invalid character
            {
                return 1; // Return 1 for valid input
            } else if (answer.Length > 1) // If length of answer is grater than 1
            {
                return 0; // Return 0 for invalid input
            }
            return 0; // Return 0 for invalid input
        }

        // Check answer if it is valid
        public int checkAnswer(string target)
        {
            if (target[0] - 65 < 0 || target[0] - 65 > 9) // If row letter is greater than 'J' or less than 'A'
            {
                return 0; // Return 0 for invalid input
            } else if (int.Parse(target.Substring(1)) - 1 < 0 || int.Parse(target.Substring(1)) - 1 > 9) // If column number is greater than 10 or negative
            {
                return 0; // Return 0 for invalid input
            } else if (target.Length != 2 && target.Length != 3) // If length of answer is less than 2 or greater than 3
            {
                return 0; // Return 0 for invalid input
            }

            return 1; // Return 1 for valid input
        }

        // Build ship's location, bow, stern, and direction
        public void BuildShips()
        {
            // Set up destroyers' length
            destroyer1.SetLength(2);
            destroyer2.SetLength(2);

            // Set up submarines' length
            submarine1.SetLength(3);
            submarine2.SetLength(3);

            // Set up battleship's length
            battleShip.SetLength(4);

            // Set up carrier's length
            carrier.SetLength(5);

            // Set destroyers' locations
            SetShips(destroyer1);
            SetShips(destroyer2);

            // Set submarines' locations
            SetShips(submarine1);
            SetShips(submarine2);

            // Set battleship's location
            SetShips(battleShip);

            // Set carrier's location
            SetShips(carrier);
        }

        // Set ships to their locations
        private void SetShips(Ship ship)
        {
            int valid = StoreShips(ship); // ensures that ships do not overlap
            while (valid == 1) // if ship overlaps
            { 
                ship.SetBow(); // change location
                valid = StoreShips(ship); // store ships again
            }
        }

        // Prompts user for target location
        public void PlayerTurn()
        {
            int valid = 0; // Indicates with input is valid
            string target = " "; // Initialize target location
       
                // prompt for location
                Console.WriteLine("What's our target?");
                target = Console.ReadLine(); // read target location input
                valid = checkAnswer(target); // check if valid

                while (valid == 0) // if input is invalid
                {
                    Console.WriteLine("Invalid Answer. Please try again."); // display error message
                    Console.WriteLine("What's our target?"); // prompt for location
                    target = Console.ReadLine(); // read target location input
                    valid = checkAnswer(target); // check if valid
            }

            // place shot
            board.SetChar(target[0] - 65, int.Parse(target.Substring(1)) - 1, 'O');

            // determine is shot is a hit
            Hit(target); 
        }

        // Determine if the target location has a ship
        public void Hit(string target)
        {
            int hit = 0; // Indicates if ship if hit
            bool alreadyHit = false; // Indicates if the ship is already hit
            int x = target[0] - 65; // x location of target
            int y = int.Parse(target.Substring(1)) - 1; // y location of target

            if (shipGrid[x, y] == 'H') // If target location contains a ship
            {
                hit = 1; // Set hit indicator to 1
            } else if (shipGrid[x, y] == 'O' || shipGrid[x, y] == 'X') // If target location is already hit
            {
                Console.WriteLine("You have hit this spot already. Please try again."); // Display error message
                alreadyHit = true; // Set boolean to true
            }

            if (hit == 1 && alreadyHit == false) // If ship is hit
            {
                Console.WriteLine("Hit!"); // Display message saying the player hit a ship
                shipGrid[x, y] = 'O'; // Store hit to ship grid array
                board.DrawHit(x, y); // Draw hit to board
                board.Display(); // Display board
            } else if (hit == 0 & alreadyHit == false)
            {
                Console.WriteLine("Miss!"); // Display message saying the player missed a ship
                board.DrawMiss(x, y); // Draw miss to board
                board.Display(); // Display board
            }

             // Display chart of ships completely destroyed
             Console.WriteLine();
             Console.WriteLine("Ships Destroyed");
             Console.WriteLine("---------------");
            
        }
        
        // Check ships to see if they are completely destroyed
        private int CheckShips()
        {
            int destroyerHit1 = HitAll(destroyer1); // checks destroyer1
            int destroyerHit2 = HitAll(destroyer2); // checks destroyer2

            int submarineHit1 = HitAll(submarine1); // checks submarine1
            int submarineHit2 = HitAll(submarine2); // checks submarine2

            int battleshipHit = HitAll(battleShip); // checks battleship
            int carrierHit = HitAll(carrier); // checks carrier

            if (destroyerHit1 == 1) // If destroyer 1 is completely hit
            {
                Console.WriteLine("Destroyer 1"); // Display Destroyer 1
            } 

            if (destroyerHit2 == 1) // If destroyer 2 is completely hit
            {
                Console.WriteLine("Destroyer 2"); // Display Destroyer 2
            }

            if (submarineHit1 == 1) // If submarine 1 is completely hit
            {
                Console.WriteLine("Submarine 1"); // Display Submarine 1
            }

            if (submarineHit2 == 1) // If submarine 2 is completely hit
            {
                Console.WriteLine("Submarine 2"); // Display Submarine 2
            }

            if (battleshipHit == 1) // If battleship is completely hit
            {
                Console.WriteLine("Battleship"); // Display Battleship
            }

            if (carrierHit == 1) // If carrier is comletely hit
            {
                Console.WriteLine("Carrier"); // Display carrier
            }

            // If all ships are hit
            if (destroyerHit1 == 1 && destroyerHit2 == 1 
                && submarineHit1 == 1 && submarineHit2 == 1 
                && battleshipHit == 1 && carrierHit == 1)
            {
                return 1; // Return 1 to indicate ships are hit
            } else { 
                return 0; // Return 0 to indicate if not all ships are hit
            }
        }

        // Determines if a ship is completely destroyed
        private int HitAll(Ship ship)
        {
            int hitCount = 0; // how many times a ship was hit
            int[] xCoordinates = ship.GetXCoordinates(); // get x coordinates of ship's location
            int[] yCoordinates = ship.GetYCoordinates(); // get y coordinates of ship's location

            for (int i = 0; i < 10; i++) // Reiterate through all arrays of x and y coordinates
            {
                if (shipGrid[xCoordinates[i], yCoordinates[i]] == 'O') // If any coordinates has a hit
                {
                    hitCount++; // Increment hit count
                }
            }

            if (hitCount == ship.GetLength()) // if the hit count is equal to length of ship
            {
                return 1; // Return 1 to indicate that all of ship is destroyed
            } else
            {
                return 0; // Return 0 to indicate that not all of ship is destroyed
            }
        }

        // Store ships in a separate grid
        private int StoreShips(Ship ship)
        {
            char shipChar = 'H'; // char representing a ship
            int bowXLocation = ship.GetBowX(); // x location of ship's bow
            int bowYLocation = ship.GetBowY(); // y locaiton of ship's bow
            int direction = ship.GetDirection(); // direction of ship's bow (horziontal or vertical)

            // Reiterate through ship's future location to check if ship does not overlap another ship
            for (int i = 0; i < ship.GetLength(); i++)
            {
                // If ship's future location already a ship
                if (shipGrid[bowXLocation, bowYLocation + i] == shipChar || shipGrid[bowXLocation - i, bowYLocation] == shipChar)
                {
                    return 1; // Return 1; the ship is overlapping anotehr ship
                }
            }

            if (direction == 0) // If ship is horizontal
            {
                for (int i = 0; i < ship.GetLength(); i++) // Reiterate through shipGrid to store ship
                {
                    shipGrid[bowXLocation - i, bowYLocation] = shipChar; // Store ship to grid
                    ship.FillXCoordinates(bowXLocation - i); // Fill x coordinates array of ship
                    ship.FillYCoordinates(bowYLocation); // Fill y coordinates array of ship
                }
            }
            else // If ship is vertical
            {
                for (int i = 0; i < ship.GetLength(); i++) // Reiterate through shipGrid to store ship
                {
                    shipGrid[bowXLocation, bowYLocation + i] = shipChar; // Store ship to grid
                    ship.FillXCoordinates(bowXLocation); // Fill x coordinates array of ship
                    ship.FillYCoordinates(bowYLocation + i); // Fill y coordinates array of ship
                }
            }

            return 0; // Return 0 if ship is successfully stored
        }
    }
}