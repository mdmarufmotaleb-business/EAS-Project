using EAS_Project;

class Program
{
    static void Main(string[] args)
    {
        var (nameA, nameB) = PlayerSetup.GetPlayerNames();

        Display.DisplayGrid(nameA, nameB);

        PlayerMove.DisplayTurn(nameA);


    }
}
