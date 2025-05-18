using System;
using NA9ZHD;


// Egyébként van egy kis problematika ami eléggé bökködi az agyamat. Szinte minden static.... De azért static, mert felesleges adott osztályból példányosítani
// csak azért hogy akkor azon az objektumon keresztül hívjam meg a metódust. Pl: UIManager.... abból miért lenne példény?
// Nem a legszebb projektem, volt már jobb is tbh. (De mindenképp volt rosszabb is lol)

//Ez az egész kód tiszta dissapointment smh.



/// <summary>
/// Itt történik a fő vezérlés. Vannak főbb metódusok amelyek az elágazásokat/funkciókat képviselik.
/// </summary>
internal class Program
{
    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        DataWarden.Instance.CheckDataFolderExistance();
        DataWarden.Instance.LoadAllMonthsFromFile();
        UIManager.Welcome();
        while(true) MainMenu();
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Belépés utáni első adatbekérés helye lesz. Ezen a ponton lehet az összes funkció felé elindulni.
    /// </summary>
    private static void MainMenu()
    {
        Console.Title = "Költségnapló Alkalmazás";
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
            { ConsoleKey.D1,  AddNewMonth},
            { ConsoleKey.D2, ListMonths},
            { ConsoleKey.D3,  ListYears},
            { ConsoleKey.D4,  () =>
                {
                    Dictionary<string, string> availableKeys = new Dictionary<string, string>();
                    availableKeys["ESC"] = "Főmenü";
                    ConsoleColor[] consoleColors = { ConsoleColor.DarkRed };
                    Dictionary<ConsoleKey, Action> menuPoints = new Dictionary<ConsoleKey, Action>
                    {
                        { ConsoleKey.Escape, () => { return; } }
                    };
                    UIManager.PrintAvailableKeys(availableKeys, consoleColors);
                    GeneralStatistics.ShowGeneralStats();
                    ConsoleKeyInputHandling(menuPoints);
                }
            },
            { ConsoleKey.Escape,  Exit},
        };
        //Bekérés
        ConsoleKeyInputHandling(menuPoints);
    }
    private static void ConsoleKeyInputHandling(Dictionary<ConsoleKey, Action> menuPoints)
    {
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
    private static void Exit()
    {
        DataWarden.Instance.SaveAllMonths();
        Environment.Exit(0);
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
                if (year >= 1900)
                {
                    int existingCount = DataWarden.Instance.GetMonthlyLedgers().Count(m => m.GetYear() == year);
                    if (existingCount < 12) goodInput = true;
                    else UIManager.PrintError(4, 1);
                }
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
                        bool alreadyExists = DataWarden.Instance.GetMonthlyLedgers().Any(m => m.GetYear() == year && (int)m.GetMonth() == monthInt);
                        if (alreadyExists) UIManager.PrintError(5, 1);
                        else goodInput = true;
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
                Console.Title = monthlyLedger.GetMonth().ToString() + " - Tranzakciók felvétele";
                UIManager.consoleCursorRowHelper = 3;
                while (AddNewTranzaction(monthlyLedger)) ;
                break;
            }
            else if (key == ConsoleKey.N) break;
        }
        DataWarden.Instance.StoreMonth(monthlyLedger);
        DataWarden.Instance.SortMonths();
    }
    /// <summary>
    /// Ennek a repetitív kódjáról nem beszélnék. Nagyon zavar engem is, rettenetesen zavar, de nem tudom hogy lehetne ezt jobban leszűkíteni. 
    /// És valami cseszett lineáris.
    /// </summary>
    /// <param name="month">Melyik hónapba töltünk fel tranzakciót.</param>
    /// <returns></returns>
    private static bool AddNewTranzaction(MonthlyLedger month)
    {
        Console.Title = month.GetYear() + " " + month.GetMonth().ToString() + " - Tranzakciók felvétele";

        DateTime date = DateTime.MinValue;
        bool isExpense = true, goodInput = false;
        TransactionCategory category = TransactionCategory.Food;
        int amount = 0;
        string description, rawInput = "";

        Console.CursorVisible = true;
        UIManager.PrintTip(0, "Dátum formátuma: 1900.01.01 00:00:00\n");
        UIManager.PrintTip(2, "A megszakításhoz nyomja meg az ESC gombot.\n");
        Console.CursorTop = UIManager.consoleCursorRowHelper;
        UIManager.Print("Dátum: ");
        UIManager.CC(ConsoleColor.Yellow);
        UIManager.Print(month.GetYear() + "." + month.GetMonth() + ".");
        UIManager.CC(ConsoleColor.Green);
        int cursorRow = UIManager.GetConsoleCursorPosition()[0];
        int cursorCol = UIManager.GetConsoleCursorPosition()[1];
        do
        {
            Console.SetCursorPosition(cursorCol, cursorRow);
            UIManager.ClearInputLeftovers(cursorRow, cursorCol, rawInput.Length);
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape) return false; // megszakítás
            try
            {
                UIManager.CC(ConsoleColor.Yellow);
                UIManager.Print(keyInfo.KeyChar.ToString());
                rawInput = keyInfo.KeyChar + Console.ReadLine();
                UIManager.CC(ConsoleColor.Green);
                date = Convert.ToDateTime(month.GetYear() + "." + month.GetMonth() + "." + rawInput);
                goodInput = true;
                UIManager.ClearLine(1);
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (ArgumentOutOfRangeException) { UIManager.PrintError(2, 1); }
            catch (Exception) { UIManager.PrintError(-1, 1);  }
        }
        while (!goodInput);
        goodInput = false;
        Console.SetCursorPosition(cursorCol + rawInput.Length, cursorRow);
        UIManager.Print(" | Kiadás: ");
        cursorRow = UIManager.GetConsoleCursorPosition()[0];
        cursorCol = UIManager.GetConsoleCursorPosition()[1];
        UIManager.ClearLine(2);
        UIManager.PrintTip(0, "'true' ha igen, 'false' ha nem");
        Console.SetCursorPosition(cursorCol, cursorRow);
        do
        {
            Console.SetCursorPosition(cursorCol, cursorRow);
            UIManager.ClearInputLeftovers(cursorRow, cursorCol, rawInput.Length);
            try
            {
                UIManager.CC(ConsoleColor.Yellow);
                rawInput = Console.ReadLine();
                UIManager.CC(ConsoleColor.Green);
                isExpense = Convert.ToBoolean(rawInput);
                goodInput = true;
                UIManager.ClearLine(1);
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (ArgumentOutOfRangeException) { UIManager.PrintError(2, 1); }
            catch(Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);
        goodInput = false;
        Console.SetCursorPosition(cursorCol + rawInput.Length, cursorRow);
        UIManager.Print(" | Kategória: ");
        cursorRow = UIManager.GetConsoleCursorPosition()[0];
        cursorCol = UIManager.GetConsoleCursorPosition()[1];
        UIManager.PrintTip(0, "'Food (1) Dining (2) Transport (3) Housing (4) Utilities (5) Health (6) Education (7) Entertainment (8) Shopping (9) Travel (10) Gifts (11) Fitness (12) Subscriptions (13) Salary (14) Freelance (15) Scholarship (16) Support (17) Refund (18) Other (19)");
        Console.SetCursorPosition(cursorCol, cursorRow);
        do
        {
            Console.SetCursorPosition(cursorCol, cursorRow);
            UIManager.ClearInputLeftovers(cursorRow, cursorCol, rawInput.Length);
            try
            {
                UIManager.CC(ConsoleColor.Yellow);
                rawInput = Console.ReadLine();
                UIManager.CC(ConsoleColor.Green);
                int categoryInt = Convert.ToInt32(rawInput);
                if ((categoryInt < 1) || (categoryInt > 19) || (isExpense && categoryInt > 13) || (!isExpense && categoryInt < 14)) throw new ArgumentOutOfRangeException();
                else
                {
                    category = (TransactionCategory)categoryInt;
                    goodInput = true;
                }
                UIManager.ClearLine(1);
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (ArgumentOutOfRangeException) { UIManager.PrintError(2, 1); }
            catch (Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);
        goodInput = false;
        Console.SetCursorPosition(cursorCol + rawInput.Length, cursorRow);
        UIManager.Print(" | Összeg: ");
        cursorRow = UIManager.GetConsoleCursorPosition()[0];
        cursorCol = UIManager.GetConsoleCursorPosition()[1];
        UIManager.PrintTip(0, "A tranzakció összege. Nem lehet negatív számot megadni");
        Console.SetCursorPosition(cursorCol, cursorRow);
        do
        {
            Console.SetCursorPosition(cursorCol, cursorRow);
            UIManager.ClearInputLeftovers(cursorRow, cursorCol, rawInput.Length);
            try
            {
                UIManager.CC(ConsoleColor.Yellow);
                rawInput = Console.ReadLine();
                UIManager.CC(ConsoleColor.Green);
                amount = Convert.ToInt32(rawInput);
                if (amount < 0) throw new ArgumentOutOfRangeException();
                goodInput = true;
                UIManager.ClearLine(1);
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (ArgumentOutOfRangeException) { UIManager.PrintError(2, 1); }
            catch (Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);
        Console.SetCursorPosition(cursorCol + rawInput.Length, cursorRow);
        UIManager.Print(" | Leírás: ");
        cursorRow = UIManager.GetConsoleCursorPosition()[0];
        cursorCol = UIManager.GetConsoleCursorPosition()[1];
        UIManager.PrintTip(0, "Rövid, pár szavas leírás a tranzakcióról (pl.: helyszín).");
        Console.SetCursorPosition(cursorCol, cursorRow);
        UIManager.CC(ConsoleColor.Yellow);
        description = Console.ReadLine();
        UIManager.CC(ConsoleColor.Green);

        Transaction transaction = new Transaction(date, isExpense, category, amount, description);
        month.AddTransaction(transaction);
        UIManager.consoleCursorRowHelper++;
        return true;
    }
    private static void ListMonths()
    {
        List<MonthlyLedger> ledgerList = DataWarden.Instance.GetMonthlyLedgers();
        if (ledgerList.Count == 0) return;
        UIManager.PrintTip(0, "Add meg a hónap sorszámát!\n\n");
        for (int i = 0; i < ledgerList.Count; i++)
        {
            UIManager.CC(ConsoleColor.White);
            UIManager.Print("[ ");
            UIManager.CC(ConsoleColor.Cyan);
            UIManager.Print(i.ToString());
            UIManager.CC(ConsoleColor.White);
            UIManager.Print(" ] ");
            UIManager.CC(ConsoleColor.Green);
            UIManager.Print(ledgerList[i].GetYear() + " " + ledgerList[i].GetMonth()+"\n");
        }
        UIManager.Print("\nMegtekintendő hónap: ");
        Console.CursorVisible = true;
        int cursorRow = UIManager.GetConsoleCursorPosition()[0];
        int cursorCol = UIManager.GetConsoleCursorPosition()[1];
        bool goodInput = false;
        uint viewIndex = 0;
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
                viewIndex = Convert.ToUInt32(rawInput);
                if (viewIndex < 0 || viewIndex >= ledgerList.Count) throw new ArgumentOutOfRangeException();
                goodInput = true;
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (ArgumentOutOfRangeException) { UIManager.PrintError(2, 1);  }
            catch (Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);
        ViewMonth(ledgerList[Convert.ToInt32(viewIndex)]);
    }
    private static void ViewMonth(MonthlyLedger ledger)
    {
        Console.Clear();
        Console.Title = ledger.GetYear() + " " + ledger.GetMonth() + " - Megtekintés";
        Console.CursorVisible = false;
        //Menü elkészítése
        Dictionary<string, string> availableKeys = new Dictionary<string, string>();
        availableKeys["1"] = "További tranzakciók felvétele";
        availableKeys["ESC"] = "Főmenü";
        ConsoleColor[] consoleColors = { ConsoleColor.Cyan, ConsoleColor.DarkRed };
        //Menü kiíratás
        UIManager.PrintAvailableKeys(availableKeys, consoleColors);
        
        Statistician statistician = new MonthStatistician(ledger);
        statistician.ShowData();
        statistician.ShowProfit();
        
        //Menü funkcionalitás beállítás
        Dictionary<ConsoleKey, Action> menuPoints = new Dictionary<ConsoleKey, Action>
        {
            { ConsoleKey.D1,  () => {
                Console.Clear();
                Console.Title = ledger.GetMonth().ToString() + " - Tranzakciók felvétele";
                UIManager.consoleCursorRowHelper = 3;
                while(AddNewTranzaction(ledger)) ; }},
            { ConsoleKey.Escape,  MainMenu},
        };
        ConsoleKeyInputHandling(menuPoints);
    }
    private static void ListYears()
    {
        List<MonthlyLedger> allMonths = DataWarden.Instance.GetMonthlyLedgers();
        List<ushort> distinctYears = allMonths.Select(m => m.GetYear()).Distinct().OrderBy(y => y).ToList();

        if (distinctYears.Count == 0) return;

        UIManager.PrintTip(0, "Add meg az év sorszámát!\n\n");
        for (int i = 0; i < distinctYears.Count; i++)
        {
            UIManager.CC(ConsoleColor.White);
            UIManager.Print("[ ");
            UIManager.CC(ConsoleColor.Cyan);
            UIManager.Print(i.ToString());
            UIManager.CC(ConsoleColor.White);
            UIManager.Print(" ] ");
            UIManager.CC(ConsoleColor.Green);
            UIManager.Print(distinctYears[i].ToString() + "\n");
        }
        UIManager.Print("\nMegtekintendő év: ");
        Console.CursorVisible = true;
        int cursorRow = UIManager.GetConsoleCursorPosition()[0];
        int cursorCol = UIManager.GetConsoleCursorPosition()[1];
        bool goodInput = false;
        uint viewIndex = 0;
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
                viewIndex = Convert.ToUInt32(rawInput);
                if (viewIndex >= distinctYears.Count) throw new ArgumentOutOfRangeException();
                goodInput = true;
            }
            catch (FormatException) { UIManager.PrintError(1, 1); }
            catch (ArgumentOutOfRangeException) { UIManager.PrintError(2, 1); }
            catch (Exception) { UIManager.PrintError(-1, 1); }
        }
        while (!goodInput);

        int selectedYear = distinctYears[(int)viewIndex];
        ViewYear(selectedYear);
    }
    private static void ViewYear(int year)
    {
        Console.Clear();
        Console.Title = year + " - Megtekintés";
        Console.CursorVisible = false;

        Dictionary<string, string> availableKeys = new()
        {
            ["ESC"] = "Főmenü"
        };
        ConsoleColor[] consoleColors = { ConsoleColor.DarkRed };
        UIManager.PrintAvailableKeys(availableKeys, consoleColors);
        Dictionary<ConsoleKey, Action> menuPoints = new()
        {
            { ConsoleKey.Escape, MainMenu }
        };

        // Hozzá tartozó hónapok kigyűjtése
        var allMonths = DataWarden.Instance.GetMonthlyLedgers();
        List<MonthlyLedger> yearMonths = allMonths.Where(m => m.GetYear() == year).OrderBy(m => m.GetMonth()).ToList();

        Statistician statistician = new YearStatistician(yearMonths);
        statistician.ShowData();
        statistician.ShowProfit();
        
        ConsoleKeyInputHandling(menuPoints);
    }
}