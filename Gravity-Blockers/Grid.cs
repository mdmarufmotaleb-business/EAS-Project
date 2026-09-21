namespace EAS_Project
{
    public class Grid
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public string[,] Cells { get; private set;} //2D array of strings

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

        public void MakeMove(int inputColumn, Player currentPlayer, Player playerA, Player playerB, bool isBlock)
        {
            string piece;

            if (isBlock)
            {
                piece = "[X]";
            }
            else if (currentPlayer == playerA)
            {
                piece = "[A]";
            }
            else
            {
                piece = "[B]";
            }

            int col = inputColumn - 1;

            // Keeps dropping the piece as far as it goes in selected column
            for (int r = 0; r < Rows; r++)
            {
                if (Cells[r, col] != "[ ]")
                {
                    Cells[r - 1, col] = piece;
                    return;
                }
            }

            Cells[Rows - 1, col] = piece;
        }


        public bool IsValidMove(int inputColumn)
        {
            int col = inputColumn - 1;
            return Cells[0, col] == "[ ]"; // Checks if the top cell of the column is empty
        }

        public bool IsValidColumn(int inputColumn)
        {
            return inputColumn >= 1 && inputColumn <= Columns;
        }

        public void RotateRight()
        {
            string[,] newCells = new string[Columns, Rows];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    newCells[c, Rows - 1 - r] = Cells[r, c];
                }
            }

            Cells = newCells;
            int temp = Rows;
            Rows = Columns;
            Columns = temp;
        }

        public void RotateLeft()
        {
            string[,] newCells = new string[Columns, Rows];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    newCells[Columns - 1 - c, r] = Cells[r, c];
                }
            }

            Cells = newCells;
            int temp = Rows;
            Rows = Columns;
            Columns = temp;
        }
    }

}
