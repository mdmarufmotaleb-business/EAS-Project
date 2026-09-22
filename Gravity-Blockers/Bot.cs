namespace EAS_Project
{
    public static class Bot
    {
        // Returns true after a move has been made
        public static bool MakePieceMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, int remainingMoves, bool isBlock)
        {
            String piece = Grid.CheckPiece(currentPlayer, playerA, playerB, isBlock);

            // First priority - WIN
            (bool canWin, Grid? winningGrid) result = Bot.canWin(grid, piece, currentPlayer, playerA, playerB, remainingMoves);
            
            if (result.canWin)
            {
                continue;// add logic to make this move
                return true;
            }

            // Second priority - STOP THEM FROM WINNING
            
            // Needed to simulate if opponent can win
            result = Bot.canWin(grid, piece, currentPlayer, playerA, playerB, remainingMoves);
            string newPiece = (piece == "[A]") ? "[B]" :
                (piece == "[B]") ? "[A]" :
                piece;

            if (result.canWin)
            {
                continue;// add logic to make this move
                return true;
            }

            // Third priority - build on existing pieces
            // Next move should match 3 if possible, else match 2

            (Grid grid, int column)[] level = Bot.priorityMoves(grid, currentPlayer, playerA, playerB, isBlock);

            if (level.Length > 0)
            {
                continue; 
            }

            // Fourth priority - add a move randomly
            else
            {
                return Bot.makeRandomMove(grid, currentPlayer, playerA, playerB, isBlock);
            }

            return false;

        }

        // Given a selection of moves, choose a random one and make it
        public static bool makeRandomMoveBySelection(Grid grid, Player currentPlayer, Player playerA, Player playerB, bool isBlock, (Grid int)[] selection)
        {

        }

        public static bool makeRandomMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, bool isBlock)
        {
            // Try up to 4 orientations (original + 3 rotations)
            for (int i = 0; i < 4; i++)
            {
                // Build list of columns
                List<int> columns = new List<int>();
                for (int c = 0; c < grid.Columns; c++)
                {
                    columns.Add(c + 1); // 1-based
                }

                // Shuffle columns
                Random rng = new Random();
                columns = columns.OrderBy(x => rng.Next()).ToList();

                // Try each column in random order
                foreach (int col in columns)
                {
                    if (grid.IsValidMove(col, isBlock))
                    {
                        grid.MakeMove(col, currentPlayer, playerA, playerB, isBlock);
                        return true;
                    }
                }

                // If no move was possible, rotate and try again
                grid.RotateRight();
            }

            return false; // no move possible even after 4 rotations
        }

        // Returns a list of all possible moves filtered by best value
        // Also includes column number of where to drop it
        // Returns empty list if no moves are good value
        public static (Grid grid, int column)[] priorityMoves(
            Grid grid,
            Player currentPlayer,
            Player playerA,
            Player playerB,
            bool isBlock)
        {
            
            (Grid grid, int column)[] level =
                Bot.afterOneValidMove(grid, currentPlayer, playerA, playerB, isBlock);

            // MATCH‑3 filtering
            List<(Grid grid, int column)> match3List = new List<(Grid, int)>();

            foreach ((Grid g, int col) in level)
            {
                if (checkMatchThree(g, piece))
                    match3List.Add((g, col));
            }

            // If any match‑3 grids exist, keep only those
            if (match3List.Count > 0)
            {
                level = match3List.ToArray();
            }
            else
            {
                // MATCH‑2 filtering
                List<(Grid grid, int column)> match2List = new List<(Grid, int)>();

                foreach ((Grid g, int col) in level)
                {
                    if (checkMatchTwo(g, piece))
                        match2List.Add((g, col));
                }

                // If match‑2 exists, keep them; otherwise empty the list
                if (match2List.Count > 0)
                    level = match2List.ToArray();
                else
                    level = Array.Empty<(Grid grid, int column)>();
            }

            return level;
        }

        // Checks if the bot can win this round by simulating every possible move
        public static (bool canWin, Grid? winningGrid) canWin(
            Grid grid, 
            string piece, 
            Player currentPlayer, 
            Player playerA, 
            Player playerB, 
            int remainingMoves)
        {
            // If only 1 move left, it is block so can't win
            if (remainingMoves == 1)
            {
                return (false, null);
            }

            // Simulates 1 move in every possible direction and checks for the win
            if (remainingMoves == 2)
            {
                (Grid grid, int column)[] level = Bot.afterOneValidMove(grid, currentPlayer, playerA, playerB, false);


                foreach ((Grid g, int col) in level)
                {
                    if (g.CheckWin(g, piece))
                    {
                        Grid winning = g.Clone();
                        return (true, winning);
                    }
                }


                return (false, null);
            }

            // Simulates 2 moves in every possible direction
            if (remainingMoves == 3)
            {
                // first move: normal piece (no block)
                (Grid grid, int column)[] level1 = afterOneValidMove(grid, currentPlayer, playerA, playerB, false);


                List<Grid[]> level2 = new List<Grid[]>();

                foreach ((Grid g, int col) in level1)
                {
                    (Grid grid, int column)[] next = afterOneValidMove(g, currentPlayer, playerA, playerB, false);
                    level2.Add(next);
                }


                // check every grid produced at the end
                foreach (Grid[] arr in level2)
                {
                    foreach (Grid g in arr)
                    {
                        if (g.CheckWin(g, piece))
                        {
                            Grid winning = g.Clone();
                            return (true, winning);
                        }
                    }
                }

                return (false, null);
            }

            return (false, null);
        }


        // Returns all possible grids after 1 valid move (including 4 rotations and column dropped in)
        // All returned grids are already rotated
        public static (Grid grid, int column)[] afterOneValidMove(
            Grid grid,
            Player currentPlayer,
            Player playerA,
            Player playerB,
            bool isBlock)
        {
            List<(Grid grid, int column)> results = new List<(Grid, int)>();

            Grid workingGrid = grid; // Original grid

            for (int i = 0; i < 4; i++)
            {
                // For i > 0, rotate a fresh clone of the original grid
                if (i > 0)
                {
                    workingGrid = grid.Clone();
                    workingGrid.RotateRight();
                }

                for (int col = 1; col <= workingGrid.Columns; col++)
                {
                    if (workingGrid.IsValidMove(col, isBlock))
                    {
                        Grid newGrid = workingGrid.Clone();
                        newGrid.MakeMove(col, currentPlayer, playerA, playerB, isBlock);

                        // Store BOTH the grid and the column used
                        results.Add((newGrid, col));
                    }
                }
            }

            return results.ToArray();
        }


        // Checks if 2 of the given pieces are in any row in the grid
        public bool checkMatchTwo(Grid grid, string piece)
        {
            // horizontal check
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns - 1; c++)
                {
                    if (grid.Cells[r, c] == piece && grid.Cells[r, c + 1] == piece)
                        return true;
                }
            }

            // vertical check
            for (int r = 0; r < grid.Rows - 1; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    if (grid.Cells[r, c] == piece && grid.Cells[r + 1, c] == piece)
                        return true;
                }
            }

            // diagonal down-right
            for (int r = 0; r < grid.Rows - 1; r++)
            {
                for (int c = 0; c < grid.Columns - 1; c++)
                {
                    if (grid.Cells[r, c] == piece && grid.Cells[r + 1, c + 1] == piece)
                        return true;
                }
            }

            // diagonal down-left
            for (int r = 0; r < grid.Rows - 1; r++)
            {
                for (int c = 1; c < grid.Columns; c++)
                {
                    if (grid.Cells[r, c] == piece && grid.Cells[r + 1, c - 1] == piece)
                        return true;
                }
            }

            return false;
        }
        public bool checkMatchThree(Grid grid, string piece)
        {
            // horizontal check
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns - 2; c++)
                {
                    if (grid.Cells[r, c] == piece &&
                        grid.Cells[r, c + 1] == piece &&
                        grid.Cells[r, c + 2] == piece)
                        return true;
                }
            }

            // vertical check
            for (int r = 0; r < grid.Rows - 2; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    if (grid.Cells[r, c] == piece &&
                        grid.Cells[r + 1, c] == piece &&
                        grid.Cells[r + 2, c] == piece)
                        return true;
                }
            }

            // diagonal down-right
            for (int r = 0; r < grid.Rows - 2; r++)
            {
                for (int c = 0; c < grid.Columns - 2; c++)
                {
                    if (grid.Cells[r, c] == piece &&
                        grid.Cells[r + 1, c + 1] == piece &&
                        grid.Cells[r + 2, c + 2] == piece)
                        return true;
                }
            }

            // diagonal down-left
            for (int r = 0; r < grid.Rows - 2; r++)
            {
                for (int c = 2; c < grid.Columns; c++)
                {
                    if (grid.Cells[r, c] == piece &&
                        grid.Cells[r + 1, c - 1] == piece &&
                        grid.Cells[r + 2, c - 2] == piece)
                        return true;
                }
            }

            return false;
        }

    }
}