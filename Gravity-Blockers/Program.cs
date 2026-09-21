using EAS_Project;

class Program
{
    static void Main(string[] args)
    {
        Display.DisplayIntroMessage();

        (Player playerA, Player playerB) = PlayerSetup.GetPlayers();

        Display.DisplayWelcomeMessage(playerA.name, playerB.name);
        Display.DisplayTitleMessage();

        Grid grid = Display.CreateGrid();

        Display.DisplayInitialGrid(grid);

        PlayGame.Play(grid, playerA, playerB);

    }
}
