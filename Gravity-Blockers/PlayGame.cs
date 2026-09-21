namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, Player playerA, Player playerB){
            
            Player currentPlayer = playerA; //References the original playerA, not a duplicate
            
            Display.DisplayTurnNameMessage(currentPlayer.name);
            Display.DisplayTurnMessage();

            while (true) //Game loop starts here
            {
                if (grid.CheckWin(grid, "[A]"))
                {
                    Display.DisplayWinMessage(playerA.name);
                    break;
                }
                else if (grid.CheckWin(grid, "[B]"))
                {
                    Display.DisplayWinMessage(playerB.name);
                    break;
                }
                else if (grid.IsFull(grid))
                {
                    Display.DisplayDrawMessage();
                    break;
                }
            
                Display.DisplayMovesRemainingMessage(currentPlayer);
                
                if (currentPlayer.movesRemaining == 3 || currentPlayer.movesRemaining == 2)
                {
                    Display.DisplayMovePieceMessage();
                    currentPlayer = PlayGame.MakeMove(grid, currentPlayer, playerA, playerB, false); // First and second moves are always PIECES
                }
                else if (currentPlayer.movesRemaining == 1)
                {
                    Display.DisplayMoveBlockMessage();
                    currentPlayer = PlayGame.MakeMove(grid, currentPlayer, playerA, playerB, true); // Third move is always a BLOCK
                }

                Display.DisplayGrid(grid);
            }

        }

        public static void SwitchPlayer(ref Player currentPlayer, Player playerA, Player playerB) //ref modifies copy passed in
        {
            if (currentPlayer == playerA)
            {
                currentPlayer = playerB;
            }
            else if (currentPlayer == playerB)
            {
                currentPlayer = playerA;
            }
        }

        public static Player MakeMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, bool isBlock)
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
                else if (grid.IsValidMove(column, isBlock))
                {
                    currentPlayer.DecrementMoves();
                    grid.MakeMove(column, currentPlayer, playerA, playerB, isBlock);

                    Display.DisplayDropSuccessMessage(currentPlayer.name, column, isBlock);

                    if (isBlock)
                    {
                        currentPlayer.ResetMoves();
                        PlayGame.SwitchPlayer(ref currentPlayer, playerA, playerB);
                    }

                    Display.DisplayTurnNameMessage(currentPlayer.name);
                    Display.DisplayTurnMessage();
                }

                else if (isBlock && !grid.IsValidMove(column, isBlock))
                {
                    Display.DisplayInvalidBlockMessage();
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
            return currentPlayer;
        }
    }
}