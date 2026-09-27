namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, Player playerA, Player playerB){
            
            Player currentPlayer = playerA; 
            
            Display.DisplayTurnNameMessage(currentPlayer.name);
            Display.DisplayTurnMessage();

            while (true) //Game loop starts here
            {
                string piece = (currentPlayer == playerA) ? "[A]" : "[B]";

                if (grid.CheckWin(grid, piece))
                {
                    Display.DisplayWinMessage(currentPlayer.name);
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
                    if (!currentPlayer.isBot)
                    {
                        currentPlayer = PlayGame.MakeMove(grid, currentPlayer, playerA, playerB, piece, false); // First and second moves are always PIECES
                    }
                    else
                    {
                        Bot.MakeMove(grid, currentPlayer, playerA, playerB, currentPlayer.movesRemaining, false);

                        Display.DisplayTurnNameMessage(currentPlayer.name);
                        Display.DisplayTurnMessage();

                        currentPlayer.DecrementMoves();
                    }
                    
                }
                else if (currentPlayer.movesRemaining == 1)
                {
                    Display.DisplayMoveBlockMessage();

                    if (!currentPlayer.isBot)
                    {
                        currentPlayer = PlayGame.MakeMove(grid, currentPlayer, playerA, playerB, piece, true); // Third move is always a BLOCK
                    }
                    else
                    {
                        Bot.MakeMove(grid, currentPlayer, playerA, playerB, currentPlayer.movesRemaining, true);

                        Display.DisplayTurnNameMessage(currentPlayer.name);
                        Display.DisplayTurnMessage();
                        currentPlayer.DecrementMoves();

                        currentPlayer.ResetMoves();
                        PlayGame.SwitchPlayer(ref currentPlayer, playerA, playerB);
                    }
                
                }

                Display.DisplayGrid(grid);
            }

        }

        public static void SwitchPlayer(ref Player currentPlayer, Player playerA, Player playerB)
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

        public static Player MakeMove(Grid grid, Player currentPlayer, Player playerA, Player playerB, string piece, bool isBlock)
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
                    grid.MakeMove(column, currentPlayer, playerA, playerB, piece, isBlock);

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