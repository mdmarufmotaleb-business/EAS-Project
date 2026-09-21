namespace EAS_Project
{
    public static class Display
    {
        public static void DisplayGrid(string nameA, string nameB)
        {
            Grid grid = new Grid(7, 7);
            DisplayInitialGrid(grid);
        }

        public static void DisplayInitialGrid(Grid grid)
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
    }
}
