using NA9ZHD;

internal class Program
{
    private static void Main(string[] args)
    {
        Startup();
    }

    public static void Startup()
    {
        DataWarden.Instance().CheckDataFolderExistance();
        UIManager.Welcome();
        Console.ReadKey();
    }
}