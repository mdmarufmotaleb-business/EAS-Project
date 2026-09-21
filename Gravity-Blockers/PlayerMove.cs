namespace EAS_Project
{
    public static class PlayerMove
    {
        public static void DisplayTurn(string name)
        {
            Console.WriteLine($"\n{name}'s turn\n");
            Console.WriteLine("Please select a valid column number to drop your piece");
            Console.WriteLine("Please type 'RIGHT' or 'LEFT' to rotate the board\n");
        }

    }
    
}