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

        public bool CheckWin(Grid grid, string piece_symbol)
        {
            // Loop through every cell if it matches the piece_symbol
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    string currentCell = grid.Cells[r, c];

                    if (currentCell == "[ ]" || currentCell == "[X]")
                        continue;

                    if (currentCell != piece_symbol)
                        continue;

                    // Check horizontal
                    if (c <= grid.Columns - 4 &&
                        currentCell == grid.Cells[r, c + 1] &&
                        currentCell == grid.Cells[r, c + 2] &&
                        currentCell == grid.Cells[r, c + 3])
                    {
                        return true;
                    }

                    // Check vertical
                    if (r <= grid.Rows - 4 &&
                        currentCell == grid.Cells[r + 1, c] &&
                        currentCell == grid.Cells[r + 2, c] &&
                        currentCell == grid.Cells[r + 3, c])
                    {
                        return true;
                    }

                    // Check diagonal down-right
                    if (r <= grid.Rows - 4 && c <= grid.Columns - 4 &&
                        currentCell == grid.Cells[r + 1, c + 1] &&
                        currentCell == grid.Cells[r + 2, c + 2] &&
                        currentCell == grid.Cells[r + 3, c + 3])
                    {
                        return true;
                    }

                    // Check diagonal up-right
                    if (r >= 3 && c <= grid.Columns - 4 &&
                        currentCell == grid.Cells[r - 1, c + 1] &&
                        currentCell == grid.Cells[r - 2, c + 2] &&
                        currentCell == grid.Cells[r - 3, c + 3])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Since the grid can rotate, we must check every edge
        public bool IsFull(Grid grid)
        {
            // top row
            for (int col = 0; col < grid.Columns; col++)
            {
                if (grid.Cells[0, col] == "[ ]")
                    return false;
            }

            // bottom row
            for (int col = 0; col < grid.Columns; col++)
            {
                if (grid.Cells[grid.Rows - 1, col] == "[ ]")
                    return false;
            }

            // left column
            for (int row = 0; row < grid.Rows; row++)
            {
                if (grid.Cells[row, 0] == "[ ]")
                    return false;
            }

            // right column
            for (int row = 0; row < grid.Rows; row++)
            {
                if (grid.Cells[row, grid.Columns - 1] == "[ ]")
                    return false;
            }

            return true;
        }

    }
}
