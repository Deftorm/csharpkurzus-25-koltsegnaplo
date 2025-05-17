using System;

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
        Console.Clear();
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
        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (menuPoints.TryGetValue(key, out var action))
            {
                Console.Clear();
                action();
                break;
            }
        }
    }
    private static void AddNewMonth()
    {
        Console.Title = "ÚJ HÓNAP";
        Console.CursorVisible = true;
        UIManager.Print("\n\n\n");

        //ÉV BEKÉRÉSE
        UIManager.Print("Év: ");
        int cursorRow = UIManager.GetConsoleCursorPosition()[0];
        int cursorCol = UIManager.GetConsoleCursorPosition()[1];
        UIManager.PrintTip(0, "Adjon meg egy évszámot amely legalább 1900\n");
        bool goodInput = false;
        ushort year = 0;
        string rawInput = "";
        do
        {
            Console.SetCursorPosition(cursorCol, cursorRow);
            UIManager.ClearInputLeftovers(cursorRow, cursorCol, rawInput.Length);
            try
            {
                UIManager.CC(ConsoleColor.Yellow);
                rawInput = Console.ReadLine();
                UIManager.CC(ConsoleColor.Green);
                UIManager.ClearLine(1);
                year = Convert.ToUInt16(rawInput);
                if (year >= 1900) goodInput = true;
                else UIManager.PrintError(2, 1);
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);

        //HÓNAP BEKÉRÉSE
        // Igen, ez tiszta duplikált kód, de nem tudtam most jobbat kitalálni...
        UIManager.ClearLine(0);
        UIManager.PrintTip(0, "Adja meg a hónap számát! Pl.: Április --> 4\n");
        cursorRow++;
        Console.SetCursorPosition(0, cursorRow);
        UIManager.Print("Hónap: ");
        cursorRow = UIManager.GetConsoleCursorPosition()[0];
        cursorCol = UIManager.GetConsoleCursorPosition()[1];
        goodInput = false;
        Months month = Months.JANUÁR;
        do
        {
            Console.SetCursorPosition(cursorCol, cursorRow);
            UIManager.ClearInputLeftovers(cursorRow, cursorCol, rawInput.Length);
            try
            {
                UIManager.CC(ConsoleColor.Yellow);
                rawInput = Console.ReadLine();
                UIManager.CC(ConsoleColor.Green);
                UIManager.ClearLine(1);
                if (int.TryParse(rawInput, out int monthInt))
                {
                    if (Enum.IsDefined(typeof(Months), monthInt))
                    {
                        month = (Months)monthInt;
                        goodInput = true;
                    }
                    else UIManager.PrintError(2, 1);
                }
                else throw new FormatException();
            }
            catch(FormatException) { UIManager.PrintError(1, 1); }
            catch(Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);
        //Új hónap objektum létrehozása és eltárolása
        MonthlyLedger monthlyLedger = new MonthlyLedger(year, month);
        DataWarden.Instance().StoreMonth(monthlyLedger);
        //Azonnali tranzakció feltöltés rákérdezés
        UIManager.ClearLine(0);
        Console.SetCursorPosition(0, 5);
        Dictionary<string, string> availableKeys = new Dictionary<string, string>();
        availableKeys["Y"] = "Igen";
        availableKeys["N"] = "Nem";
        ConsoleColor[] consoleColors = { ConsoleColor.Green, ConsoleColor.DarkRed };
        UIManager.Print("Feltöltés tranzakciókkal most?\n");
        UIManager.PrintAvailableKeys(availableKeys, consoleColors);
        while(true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Y)
            {
                Console.Clear();
                while (AddNewTranzaction(monthlyLedger)) ;
                break;
            }
            else if (key == ConsoleKey.N) break;
        }
        MainMenu();
    }
    private static bool AddNewTranzaction(MonthlyLedger month)
    {
        return false;
    }
}