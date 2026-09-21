namespace EAS_Project
{
    public static class PlayGame
    {
        public static void Play(Grid grid, string nameA, string nameB){
            PlayerMove.DisplayTurn(nameA);

            Console.Write("Answer: ");
            string? answer = Console.ReadLine();

            if (answer == "RIGHT"){
                Console.WriteLine("The grid has rotated RIGHT");
                //grid.RotateRight();
            }
            else if (answer == "LEFT"){
                Console.WriteLine("The grid has rotated LEFT");
                //grid.RotateLeft();
            }

            
        }
    }

}