namespace EAS_Project
{
    public static class Bot
    {
        public static void MakePieceMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, int remainingMoves, bool isBlock)
        {
            
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
                Grid[] level = afterOneValidMove(grid, currentPlayer, playerA, playerB, false);

                foreach (Grid g in level)
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
                Grid[] level1 = afterOneValidMove(grid, currentPlayer, playerA, playerB, false);

                List<Grid[]> level2 = new List<Grid[]>();

                foreach (Grid g in level1)
                {
                    Grid[] next = afterOneValidMove(g, currentPlayer, playerA, playerB, false);
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


        // Returns all possible grids after 1 valid move (including 4 rotations)
        public static Grid[] afterOneValidMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, bool isBlock)
        {
            List<Grid> results = new List<Grid>();

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
                        results.Add(newGrid);
                    }
                }
            }

            return results.ToArray();
        }
    }
}