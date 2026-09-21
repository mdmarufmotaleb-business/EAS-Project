namespace EAS_Project
{
    public static class Display
    {
        public static Grid CreateGrid()
        { 
            Grid grid = new Grid(7, 7);
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
    }
}
