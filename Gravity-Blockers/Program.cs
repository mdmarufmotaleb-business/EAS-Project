using EAS_Project;

class Program
{
    static void Main(string[] args)
    {
        var (nameA, nameB) = PlayerSetup.GetPlayerNames();

        Console.WriteLine($" {nameA} & {nameB}");
    }
}
