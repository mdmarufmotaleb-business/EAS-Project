namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, string nameA, string nameB){
            
            string currentPlayer = nameA;
            
            Display.DisplayTurn(currentPlayer);
            Display.DisplayTurnMessage();

            while (true)
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
                else if (int.TryParse(answer, out int column))
                {
                    
                    if (!grid.IsValidColumn(column))
                    {
                        Display.DisplayInvalidColumnMessage(column, grid.Columns);
                    }
                    
                    else if (grid.IsValidMove(column))
                    {
                        grid.MakeMove(column);
                        Display.DisplayDropSuccessMessage(currentPlayer, column);
                        Display.DisplayTurnMessage();
                    }
                    
                    else
                    {
                        Display.DisplayFullColumnMessage(column);
                    }
                }
                else
                {
                    Display.DisplayInvalidInputMessage();
                }

                Display.DisplayGrid(grid);

            }

        }
    }

}