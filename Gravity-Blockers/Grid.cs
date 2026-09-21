namespace EAS_Project
{
    public class Grid
    {
        public int Rows { get; }
        public int Columns { get; }
        public string[,] Cells { get; }

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Cells = new string[rows, columns];

            // Initialise all cells as empty
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Cells[r, c] = "[ ]";
                }
            }
        }

        public void Display()
        {
            Console.WriteLine();

            // Column numbers
            Console.Write("   ");
            for (int c = 1; c <= Columns; c++)
            {
                Console.Write($"{c}   ");
            }
            Console.WriteLine();

            // Grid cells
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Console.Write($"{Cells[r, c]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
