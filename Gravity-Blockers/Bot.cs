namespace EAS_Project
{
    public static class Bot
    {
        public static bool MakeMove(
            Grid grid, 
            Player currentPlayer, 
            Player playerA,
            Player playerB, 
            int remainingMoves, 
            bool isBlock)
        {
            String piece = Grid.CheckPiece(currentPlayer, playerA, playerB, isBlock);

            // First priority - WIN
            (bool canWin, Grid? winningGrid, int? rotations, int? column) result = Bot.canWin(
                grid, piece, currentPlayer, playerA, playerB, remainingMoves);
            
            if (result.canWin)
            {
                Bot.rotateAndDrop(grid, result.rotations.Value, result.column.Value, currentPlayer, playerA, playerB, piece, isBlock);
            }

            // Second priority - STOP THEM FROM WINNING
            // Needed to simulate if opponent can also win
            string opponentsPiece = (piece == "[A]") ? "[B]" :
                (piece == "[B]") ? "[A]" :
                piece;
            int opponentsMoves = 3;

            result = Bot.canWin(grid, opponentsPiece, currentPlayer, playerA, playerB, opponentsMoves);

            if (result.canWin)
            {
                Bot.rotateAndDrop(
                    grid, result.rotations.Value, result.column.Value, currentPlayer, playerA, playerB, piece, isBlock);
            }

            // Third priority - build on existing pieces
            // Next move should match 3 if possible, else match 2
            (Grid grid, int column, int rotations)[] simulatedMoves = Bot.priorityMoves(
                grid, currentPlayer, playerA, playerB, piece, isBlock);

            if (simulatedMoves.Length > 0)
            {
                Random rng = new Random();

                // Pick a random priority move
                (Grid grid, int column, int rotations) chosen = simulatedMoves[rng.Next(simulatedMoves.Length)];

                // Rotate the ORIGINAL grid right 'rotations' times
                for (int i = 0; i < chosen.rotations; i++)
                {
                    grid.RotateRight();
                    Display.DisplayRotationMessage("RIGHT");
                    Display.DisplayGrid(grid);
                }

                // Now drop the piece in the chosen column
                grid.MakeMove(chosen.column, currentPlayer, playerA, playerB, piece, isBlock);
                Display.DisplayDropSuccessMessage("ROBOT", chosen.column, isBlock);
                Display.DisplayGrid(grid);

                return true;
            }
            else
            // Fourth priority - make a random move
            {
                return Bot.makeRandomMove(grid, currentPlayer, playerA, playerB, piece, isBlock);
            }
        }

        public static bool rotateAndDrop(
            Grid grid, 
            int rotations, 
            int column, 
            Player currentPlayer, 
            Player playerA, 
            Player playerB, 
            string piece, 
            bool isBlock)
        {
            // Rotate the grid
            for (int i = 0; i < rotations; i++)
            {
                grid.RotateRight();
                Display.DisplayTurnNameMessage("RIGHT");
                Display.DisplayGrid(grid);
            }

            // Now make the move
            grid.MakeMove(
                column,
                currentPlayer,
                playerA,
                playerB,
                piece,
                isBlock
            );
            Display.DisplayDropSuccessMessage("ROBOT", column, isBlock);
            
            return true;
        }

        // Rotates all grids in a list to match the original grid
        // Returns all grids AFTER rotation, plus how many right-rotations it took
       public static (Grid rotatedGrid, int rotations)[] rotateAllGrids(
            Grid originalGrid,
            Grid[] listOfGrids)
        {
            List<(Grid rotatedGrid, int rotations)> results =
                new List<(Grid, int)>();

            foreach (Grid g in listOfGrids)
            {
                Grid working = g.Clone();
                int rotations = 0;
                bool matched = false;

                // Try up to 4 orientations
                for (int i = 0; i < 4; i++)
                {
                    if (working.Equals(originalGrid))
                    {
                        results.Add((working.Clone(), rotations));
                        matched = true;
                        break;
                    }

                    // Rotate and try again
                    working.RotateRight();
                    rotations++;
                }

                // If not matched, skip this grid entirely
            }

            return results.ToArray();
        }

        public static void rotateGridRandomly(Grid grid)
        {
            Random rng = new Random();
            int roll = rng.Next(4); // 0–3

            if (roll == 0)
            {
                grid.RotateLeft();
                Display.DisplayRotationMessage("LEFT");
                Display.DisplayGrid(grid);
            }
            else if (roll == 1)
            {
                grid.RotateRight();
                Display.DisplayRotationMessage("RIGHT");
            }
        }

        public static bool makeRandomMove(
            Grid grid, 
            Player currentPlayer, 
            Player playerA, 
            Player playerB, 
            string piece, 
            bool isBlock)
        {

            Bot.rotateGridRandomly(grid);

            // Try up to 4 orientations (original + 3 rotations)
            for (int i = 0; i < 4; i++)
            {
                // Build list of columns
                List<int> columns = new List<int>();
                for (int c = 0; c < grid.Columns; c++)
                {
                    columns.Add(c);
                }

                // Shuffle columns
                Random rng = new Random();
                columns = columns.OrderBy(x => rng.Next()).ToList();

                // Try each column in random order
                foreach (int col in columns)
                {
                    if (grid.IsValidMove(col + 1, isBlock))
                    {
                        grid.MakeMove(col + 1, currentPlayer, playerA, playerB, piece, isBlock);
                        Display.DisplayDropSuccessMessage("ROBOT", col + 1, isBlock);
                        return true;
                    }
                }

                // If no move was possible, rotate and try again
                Bot.rotateGridRandomly(grid);
            }

            return false;
        }

        // Returns a list of all possible moves filtered by best value BEFORE making a move
        // Includes column number & RIGHT rotations
        // Returns empty list if no moves are good value
        public static (Grid grid, int column, int rotations)[] priorityMoves(
            Grid grid, 
            Player currentPlayer,
            Player playerA,
            Player playerB, 
            string piece, 
            bool isBlock)
        {
            // Step 1: simulate all possible moves
            (Grid simulatedGrid, int column, int rotations)[] simulatedMoves =
                Bot.afterOneValidMove(grid, currentPlayer, playerA, playerB, piece, isBlock);

            // Step 2: Try MATCH‑3 first
            List<(Grid grid, int column, int rotations)> match3List =
                new List<(Grid grid, int column, int rotations)>();

            foreach ((Grid simulatedGrid, int column, int rotations) move in simulatedMoves)
            {
                if (Bot.checkMatchThree(move.simulatedGrid, piece))
                {
                    match3List.Add((move.simulatedGrid, move.column, move.rotations));
                }
            }

            if (match3List.Count > 0)
                return match3List.ToArray();

            // Step 3: Try MATCH‑2
            List<(Grid grid, int column, int rotations)> match2List =
                new List<(Grid grid, int column, int rotations)>();

            foreach ((Grid simulatedGrid, int column, int rotations) move in simulatedMoves)
            {
                if (Bot.checkMatchTwo(move.simulatedGrid, piece))
                {
                    match2List.Add((move.simulatedGrid, move.column, move.rotations));
                }
            }

            if (match2List.Count > 0)
                return match2List.ToArray();

            // Step 4: No priority moves found → return empty list
            return Array.Empty<(Grid grid, int column, int rotations)>();
        }

        // Checks if the bot can win this round by simulating every possible move
        // Returns the winning grid
        public static (bool canWin, Grid? winningGrid, int? rotations, int? columnNumber) canWin(
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
                return (false, null, null, null);
            }

            // Simulates 1 move in every possible direction and checks for the win
            if (remainingMoves == 2)
            {
                (Grid grid, int column, int rotations)[] level = Bot.afterOneValidMove(
                    grid, currentPlayer, playerA, playerB, piece, false);

                foreach ((Grid g, int col, int rotations) in level)
                {
                    if (g.CheckWin(g, piece))
                    {
                        Grid winning = g.Clone();
                        return (true, winning, rotations, col);
                    }
                }

                return (false, null, null, null);
            }

            // Simulates 2 moves in every possible direction
            if (remainingMoves == 3)
            {
                // First move: normal piece (no block)
                (Grid grid, int column, int rotations)[] level1 = afterOneValidMove(
                    grid, currentPlayer, playerA, playerB, piece, false);

                // For each first move, simulate a second move
                foreach ((Grid g1, int col1, int rot1) in level1)
                {
                    Grid rotatedG1 = g1.Clone();

                    // Second move simulation on the rotated grid from level1
                    (Grid grid, int column, int rotations)[] level2 =
                        afterOneValidMove(rotatedG1, currentPlayer, playerA, playerB, piece, false);

                    // Check every second-move grid for a win
                    foreach ((Grid g2, int col2, int rot2) in level2)
                    {
                        if (g2.CheckWin(g2, piece))
                        {
                            Grid winning = g2.Clone();
                            return (true, winning, rot1, col1);
                        }
                    }
                }

                // No win found after 2 simulated moves
                return (false, null, null, null);
            }            
            return (false, null, null, null);
        }

        // Returns all possible grids after 1 valid move (including 4 rotations)
        // Each tuple includes: the grid (AFTER rotation), the column used, and how many right-rotations were applied
        public static (Grid grid, int column, int rotations)[] afterOneValidMove(
            Grid grid,
            Player currentPlayer,
            Player playerA,
            Player playerB,
            string piece,
            bool isBlock)
        {
            List<(Grid grid, int column, int rotations)> results =
                new List<(Grid, int, int)>();

            Grid workingGrid = grid; // Original grid

            for (int i = 0; i < 4; i++)
            {
                // For i > 0, rotate a fresh clone of the original grid
                if (i > 0)
                {
                    workingGrid = grid.Clone();
                    for (int r = 0; r < i; r++)
                    {
                        workingGrid.RotateRight();
                    }
                }

                for (int col = 1; col <= workingGrid.Columns; col++)
                {
                    if (workingGrid.IsValidMove(col, isBlock))
                    {
                        Grid newGrid = workingGrid.Clone();
                        newGrid.MakeMove(col, currentPlayer, playerA, playerB, piece, isBlock);

                        // Store grid, column, and number of rotations applied
                        results.Add((newGrid, col, i));
                    }
                }
            }

            return results.ToArray();
        }


        // Checks if the given piece has connect 2 or 3 matched
        public static bool checkMatchTwo(Grid grid, string piece)
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

        public static bool checkMatchThree(Grid grid, string piece)
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