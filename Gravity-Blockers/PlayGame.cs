namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, string nameA, string nameB){
            PlayGame.DisplayTurn(nameA);
            PlayGame.DisplayTurnMessage();

            while (true)
            {
                Console.Write("Answer: ");
                string? answer = Console.ReadLine();

                if (answer == "RIGHT"){
                    grid.RotateRight();
                    Console.WriteLine("\nThe grid has rotated RIGHT");
                    PlayGame.DisplayTurnMessage();
                }
                else if (answer == "LEFT"){
                    Console.WriteLine("\nThe grid has rotated LEFT");
                    grid.RotateLeft();
                    PlayGame.DisplayTurnMessage();
                }
                else if (int.TryParse(answer, out int column))
                {
                    
                    if (!grid.IsValidColumn(column))
                    {
                        Console.WriteLine($"\nColumn {column} is invalid. Please choose a column between 1 and {grid.Columns}");
                    }
                    
                    else if (grid.IsValidMove(column))
                    {
                        grid.MakeMove(column);
                        Console.WriteLine($"\n{nameA} has dropped a piece in column {column}");
                        PlayGame.DisplayTurnMessage();
                    }
                    
                    else
                    {
                        Console.WriteLine($"\nColumn {column} is unavailable. Please choose another column");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again");
                }

                Display.DisplayGrid(grid);

            }

        }

        public static void DisplayTurn(string name)
        {
            Console.WriteLine($"\n{name}'s turn\n");
        }

        public static void DisplayTurnMessage()
        {
            Console.WriteLine("\nPlease select a valid column number to drop your piece");
            Console.WriteLine("Or type 'RIGHT'/'LEFT' to rotate the board\n");
        }

    }


}