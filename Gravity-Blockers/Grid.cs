namespace EAS_Project
{
    public class Grid
    {
        public int Rows { get; }
        public int Columns { get; }

        public string[,] Cells { get; } //2D array of strings

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            Cells = new string[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Cells[r, c] = "[ ]";
                }
            }
        }

        public void MakeMove(int inputColumn)
        // drops the piece down
        {
            int col = inputColumn - 1;

            for (int r = 0; r < Rows; r++)
            {
                if (Cells[r, col] != "[ ]")
                {
                    Cells[r - 1, col] = "[X]";
                    return;
                }
            }

            Cells[Rows - 1, col] = "[X]";
        }


        public bool IsValidMove(int inputColumn)
        {
            int col = inputColumn - 1;
            return Cells[0, col] == "[ ]"; // Check if the top cell of the column is empty
        }
    }

}
