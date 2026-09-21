namespace EAS_Project
{
    public static class Display
    {
        public static void DisplayGrid(string nameA, string nameB)
        {
            DisplayInitialGrid(7, 7);
        }

        public static void DisplayInitialGrid(int rows, int columns)
        {
            DisplayColumnNumbers(columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
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
