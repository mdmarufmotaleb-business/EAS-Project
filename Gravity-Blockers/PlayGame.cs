namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, string nameA, string nameB){
            PlayerMove.DisplayTurn(nameA);

            Console.Write("Answer: ");
            string? answer = Console.ReadLine();

            if (answer == "RIGHT"){
                grid.RotateRight();
                Console.WriteLine("\nThe grid has rotated RIGHT\n");
            }
            else if (answer == "LEFT"){
                Console.WriteLine("\nThe grid has rotated LEFT\n");
                grid.RotateLeft();
            }

            Display.DisplayGrid(grid);

            
        }
    }

}