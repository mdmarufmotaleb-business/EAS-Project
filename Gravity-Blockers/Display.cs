namespace EAS_Project
{
    public static class Display
    {
        public static Grid CreateGrid()
        { 
            Grid grid = new Grid(5, 7);
            return grid;
        }

        public static Grid DisplayInitialGrid(Grid grid)
        {

            DisplayColumnNumbers(grid.Columns);

            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    Console.Write("[ ] ");
                }
                Console.WriteLine();
            }

            return grid;
        }

        public static void DisplayColumnNumbers(int columns)
        {
            Console.WriteLine();
            for (int j = 0; j < columns; j++)
            {
                Console.Write($" {j + 1}  ");
            }
            Console.WriteLine();
        }

        public static void DisplayGrid(Grid grid)
        {
            DisplayColumnNumbers(grid.Columns);

            for (int i = 0; i < grid.Rows; i++)
            {
                for (int j = 0; j < grid.Columns; j++)
                {
                    Console.Write($"{grid.Cells[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        public static void DisplayFullColumnMessage(int column)
        {
            Console.WriteLine($"\nColumn {column} is full. Please choose another column");
            Thread.Sleep(1000);
        }

        public static void DisplayTurnMessage()
        {
            Console.WriteLine("\nPlease select a valid column number");
            Console.WriteLine("\nOr type 'RIGHT'/'LEFT' to rotate the board");
        }

        public static void DisplayTurnNameMessage(string name)
        {
            Console.WriteLine($"\n{name}'s turn...");
            Thread.Sleep(1000); 
        }

        public static void DisplayInvalidInputMessage(string name)
        {
            Console.WriteLine("\nInvalid input, please try again");
            Display.DisplayTurnNameMessage(name);
        }

        public static void DisplayDropSuccessMessage(string name, int column, bool isBlock)
        {
            if (isBlock)
            {
                Console.WriteLine($"\n{name} has dropped a block in column {column}");
            }
            else
            {
                Console.WriteLine($"\n{name} has dropped a piece in column {column}");
            }
            Thread.Sleep(1000);
        }

        public static void DisplayInvalidColumnMessage(int column, int maxColumns)
        {
            Console.WriteLine($"\nColumn {column} is invalid. Please choose a column between 1 and {maxColumns}");
            Thread.Sleep(1000);
        }

        public static void DisplayRotationMessage(string direction)
        {
            Console.WriteLine($"\nThe grid has rotated {direction}");
            Thread.Sleep(1000);
        }

        public static void DisplayIntroMessage()
        {
            Console.WriteLine("\n\nHello, Welcome to Connect 4 - Gravity Blockers!");
            Console.WriteLine("Please start by entering the names of both players\n");
        }

        public static void DisplayWelcomeMessage(string nameA, string nameB)
        {
            Console.WriteLine($"\nWelcome {nameA} and {nameB}! We will begin shortly...\n");
            Thread.Sleep(3000);
        }

        public static void DisplayTitleMessage()
        {
            Console.WriteLine("\n\n\nCONNECT 4 - GRAVITY BLOCKERS");
        }

        public static void DisplayEmptyNameMessage(string player)
        {
            Console.WriteLine($"\nPlayer {player}'s name cannot be empty. Please enter a valid name.");
        }

        public static void DisplayMovesRemainingMessage(Player player)
        {
            Console.WriteLine($"\n{player.name} has {player.movesRemaining} moves remaining");
        }

        public static void DisplayMovePieceMessage()
        {
            Console.WriteLine("\nYou are dropping a PIECE");
        }

        public static void DisplayMoveBlockMessage()
        {
            Console.WriteLine("\nYou are dropping a BLOCK");
        }

        public static void DisplayWinMessage(string name)
        {
            Console.WriteLine($"\n{name} has won the game!");
        }

        public static void DisplayDrawMessage()
        {
            Console.WriteLine("\nThe game is a draw!");
        }

        public static void DisplayInvalidBlockMessage()
        {
            Console.WriteLine("\nInvalid block placement. A block must be placed adjacent to at least one piece.");
            Thread.Sleep(1000);
        }
    }
}
