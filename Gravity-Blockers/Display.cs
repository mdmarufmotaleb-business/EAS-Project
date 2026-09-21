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
        }

        public static void DisplayTurnMessage()
        {
            Console.WriteLine("\nPlease select a valid column number to drop your piece");
            Console.WriteLine("Or type 'RIGHT'/'LEFT' to rotate the board\n");
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

        public static void DisplayDropSuccessMessage(string name, int column)
        {
            Console.WriteLine($"\n{name} has dropped a piece in column {column}");
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
    }
}
