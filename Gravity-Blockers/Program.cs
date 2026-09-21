using EAS_Project;

class Program
{
    static void Main(string[] args)
    {
        var (nameA, nameB) = PlayerSetup.GetPlayerNames();

        Grid grid = Display.CreateGrid();
        Display.DisplayInitialGrid(grid);

        PlayGame.Play(grid, nameA, nameB);

    }
}
