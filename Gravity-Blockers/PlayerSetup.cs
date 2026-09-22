namespace EAS_Project
{
    public static class PlayerSetup
    {
        public static (Player playerA, Player playerB) GetPlayers()
        {
            string nameA = GetSinglePlayer("A");
            string nameB = GetSinglePlayer("B");

            Player playerA = new Player(nameA);
            Player playerB = new Player(nameB);

            // For playing against built in bot
            playerA.SetBot(playerA.name == "ROBOT");
            playerB.SetBot(playerB.name == "ROBOT");

            return (playerA, playerB);
        }

        public static string GetSinglePlayer(string player) //player is A or B (not be to confused with Player class)
        {
            Console.Write($"Player {player}'s Name: ");
            string? name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Display.DisplayEmptyNameMessage(player);
                name = Console.ReadLine();
            }

            return name;
        }
    }
}
