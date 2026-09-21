namespace EAS_Project
{
    public static class PlayerSetup
    {
        public static (string nameA, string nameB) GetPlayerNames()
        {
            Console.WriteLine("\n\nHello, Welcome to Connect 4 - Gravity Blockers!");
            Console.WriteLine("Please start by entering the names of both players\n");

            string nameA = GetSinglePlayer("A");
            string nameB = GetSinglePlayer("B");

            Console.WriteLine($"\nWelcome {nameA} and {nameB}! We will begin shortly...\n");
            Thread.Sleep(3000);

            Console.WriteLine("\n\n\nCONNECT 4 - GRAVITY BLOCKERS");


            return (nameA, nameB);
        }

        public static string GetSinglePlayer(string player)
        {
            Console.Write($"Player {player}'s Name: ");
            string? name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write($"Name cannot be empty. Please enter Player {player}'s Name: ");
                name = Console.ReadLine();
            }

            return name;
        }
    }
}
