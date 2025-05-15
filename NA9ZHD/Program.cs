using NA9ZHD;
/// <summary>
/// Itt történik a fő vezérlés. Vannak főbb metódusok amelyek az elágazásokat/funkciókat képviselik.
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        Startup();
        MainMenu();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Ellenőrizzük az adatos mappa létezését és üdvözüljük a felhasználót.
    /// </summary>
    private static void Startup()
    {
        Console.CursorVisible = false;
        DataWarden.Instance().CheckDataFolderExistance();
        UIManager.Welcome();
    }
    /// <summary>
    /// Belépés utáni első adatbekérés helye lesz. Ezen a ponton lehet az összes funkció felé elindulni.
    /// </summary>
    private static void MainMenu()
    {
        //Előkészítés - UI
        Dictionary<string, string> availableKeys = new Dictionary<string, string>();
        availableKeys["1"] = "Új hónap";
        availableKeys["2"] = "Hónap megtekintése";
        availableKeys["3"] = "Év megtekintése";
        availableKeys["4"] = "Statisztikák";
        availableKeys["ESC"] = "Kilépés";
        ConsoleColor[] consoleColors = { ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.DarkRed };
        //Kiíratás
        UIManager.PrintAvailableKeys(availableKeys, consoleColors);

        //Előkészítés - Input
        Dictionary<ConsoleKey, Action> menuPoints = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.D1,  () => AddNewMonth()},
            { ConsoleKey.D2,  () => throw new NotImplementedException()},
            { ConsoleKey.D3,  () => throw new NotImplementedException()},
            { ConsoleKey.D4,  () => throw new NotImplementedException()},
            { ConsoleKey.Escape,  () => Environment.Exit(0) },
        };
        //Bekérés
        InputHandler.HandleMenuInput(menuPoints);
    }
    private static void AddNewMonth()
    {
        Console.Title = "ÚJ HÓNAP";
        Console.CursorVisible = true;
        
    }
}