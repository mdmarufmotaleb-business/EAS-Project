namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, Player playerA, Player playerB){
            
            Player currentPlayer = playerA;
            
            Display.DisplayTurnNameMessage(currentPlayer.name);
            Display.DisplayTurnMessage();

            while (true)
            {
                PlayGame.DropPiece(grid, currentPlayer);
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

        public static void DropPiece(Grid grid, Player currentPlayer)
        {
            Console.Write("Answer: ");
            string? answer = Console.ReadLine();

            if (answer == "RIGHT"){
                grid.RotateRight();
                Display.DisplayRotationMessage("RIGHT");
                Display.DisplayTurnMessage();
            }
            else if (answer == "LEFT"){
                grid.RotateLeft();
                Display.DisplayRotationMessage("LEFT");
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
                    grid.MakeMove(column);
                    Display.DisplayDropSuccessMessage(currentPlayer.name, column);
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