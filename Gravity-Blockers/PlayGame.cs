namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, Player playerA, Player playerB){
            
            Player currentPlayer = playerA; //Reference the original playerA, not a duplicate
            
            Display.DisplayTurnNameMessage(currentPlayer.name);
            Display.DisplayTurnMessage();

            while (true) //Game loop starts here
            {
                Display.DisplayMovesRemainingMessage(currentPlayer);
                
                if (currentPlayer.movesRemaining == 3 || currentPlayer.movesRemaining == 2)
                {
                    Display.DisplayMovePieceMessage();
                    PlayGame.MakeMove(grid, currentPlayer, playerA, playerB, false); // First and second moves are always PIECES
                }
                else if (currentPlayer.movesRemaining == 1)
                {
                    Display.DisplayMoveBlockMessage();
                    PlayGame.MakeMove(grid, currentPlayer, playerA, playerB, true); // Third move is always a BLOCK
                }

                Display.DisplayGrid(grid);
            }

        }

        public static void SwitchPlayer(ref Player currentPlayer, Player playerA, Player playerB) //ref modifies original copy
        {
            if (currentPlayer == playerA)
            {
                currentPlayer = playerB;
            }
            else
            {
                currentPlayer = playerA;
            }
        }

        public static void MakeMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, bool isBlock)
        {
            Console.Write("\nAnswer: ");
            string? answer = Console.ReadLine();

            if (answer?.ToUpper() == "RIGHT"){
                grid.RotateRight();

                Display.DisplayRotationMessage("RIGHT");
                Display.DisplayTurnNameMessage(currentPlayer.name);
                Display.DisplayTurnMessage();
            }
            else if (answer?.ToUpper() == "LEFT"){
                grid.RotateLeft();

                Display.DisplayRotationMessage("LEFT");
                Display.DisplayTurnNameMessage(currentPlayer.name);
                Display.DisplayTurnMessage();
            }
            else if (int.TryParse(answer, out int column)) //If its a valid integer
            {
                
                if (!grid.IsValidColumn(column))
                {
                    Display.DisplayInvalidColumnMessage(column, grid.Columns);
                }
                
                else if (grid.IsValidMove(column))
                {
                    currentPlayer.DecrementMoves();
                    grid.MakeMove(column, currentPlayer, playerA, playerB, isBlock);

                    // Blocks are always dropped last, then switch players
                    if (isBlock)
                    {
                        currentPlayer.ResetMoves();
                        PlayGame.SwitchPlayer(ref currentPlayer, playerA, playerB);
                    }

                    Display.DisplayDropSuccessMessage(currentPlayer.name, column, isBlock);
                    Display.DisplayTurnNameMessage(currentPlayer.name);
                    Display.DisplayTurnMessage();
                }
                
                else
                {
                    Display.DisplayFullColumnMessage(column);
                }
            }
            else
            {
                Display.DisplayInvalidInputMessage(currentPlayer.name);
            }
        }
    }
}