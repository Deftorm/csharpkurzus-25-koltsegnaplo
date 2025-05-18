using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// A konzolos applikáció "frontend" része. Kiíratásért felelős.
///</summary>
public static class UIManager
{
    /// <summary>
    /// Segít visszatérni egy adott sorba hogy a kiíratás onnan folytatódjon. Először a tranzakció felvétel során került felhasználásra.
    /// </summary>
    public static int consoleCursorRowHelper = 0;

    /// <summary>
    /// Behelyettesíti a Console.Write metódus szerepét. Manuálisan kell sortörést a végére biggyeszteni
    /// </summary>
    /// <param name="text"></param>
    public static void Print(string text)
    {
        Console.Write(text);
    }
    public static void Welcome()
    {
        Console.SetWindowSize(123, Console.WindowHeight);
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print("" +
            "...........................................................................................................................\r\n" +
            "...........................................................................................................................\r\n" +
            ".................██╗  ██╗██████╗██╗ ██████████████████████╗██████╗███╗   ██╗█████╗██████╗██╗     ██████╗ ..................\r\n" +
            ".................██║ ██╔██╔═══████║ ╚══██╔══██╔════██╔════██╔════╝████╗  ████╔══████╔══████║    ██╔═══██╗..................\r\n" +
            ".................█████╔╝██║   ████║    ██║  ████████████╗ ██║  █████╔██╗ ███████████████╔██║    ██║   ██║..................\r\n" +
            ".................██╔═██╗██║   ████║    ██║  ╚════████╔══╝ ██║   ████║╚██╗████╔══████╔═══╝██║    ██║   ██║..................\r\n" +
            ".................██║  ██╚██████╔█████████║  ██████████████╚██████╔██║ ╚██████║  ████║    ███████╚██████╔╝..................\r\n" +
            ".................╚═╝  ╚═╝╚═════╝╚══════╚═╝  ╚══════╚══════╝╚═════╝╚═╝  ╚═══╚═╝  ╚═╚═╝    ╚══════╝╚═════╝ ..................\r\n" +
            "...........................................................................................................................\r\n" +
            "...........................................................................................................................\r\n");

        Print("...........................................................................................................................\r\n");

        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.Green);
        Print("PROGRAMOZTA:");
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print(".........");
        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.Green);
        Print("Németh Márton");
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print(".........................................................................................\n");

        Print("...........................................................................................................................\r\n");

        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.Green);
        Print("NEPTUN  KÓD:");
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print(".........");
        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.Green);
        Print("NA9ZHD");
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print("................................................................................................\n");

        Print("...........................................................................................................................\r\n");

        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.Green);
        Print("KURZUS NEVE:");
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print(".........");
        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.Green);
        Print("Alkalmazásfejlesztés c# alapokon a modern fejlesztési irányelvek bemutatásával");
        Console.BackgroundColor = ConsoleColor.Green;
        CC(ConsoleColor.Black);
        Print("........................\n");

        Print("...........................................................................................................................\r\n");
        Print("...........................................................................................................................\r\n");
        Print("...........................................................................................................................\r\n");
        Print("..........................© 2025 - 2025 Költségnapló Konzolapplikáció - You Have No Rights Bozo............................\n");
        Console.BackgroundColor = ConsoleColor.Black;
        CC(ConsoleColor.DarkGray);
        Print("\n--// Funkciók navigálása gombnyomással lehetséges. Az elérhető gombokat és funkciókat kilistázzuk. //--\n");
        CC(ConsoleColor.Green);
        Print("\n\nNyomjon meg egy gombot a továbblépéshez!\n");
        Console.ReadKey();
        Console.Clear();
    }
    public static void PrintAvailableKeys(Dictionary<string, string> keys, ConsoleColor[]? colors = null)
    {
        byte colorIndex = 0;
        foreach (string key in keys.Keys)
        {
            string functionDesc = "";
            keys.TryGetValue(key, out functionDesc);
            if (colors == null)
            {
                Print("| ");
                CC(ConsoleColor.White);
                Print("[ ");
                CC(ConsoleColor.Green);
                Print(key.ToString());
                CC(ConsoleColor.White);
                Print(" ]");
                CC(ConsoleColor.Green);
                Print($": {functionDesc} |");
            }
            else
            {
                Print("| ");
                CC(ConsoleColor.White);
                Print("[ ");
                Console.ForegroundColor = colors[colorIndex];
                Print(key.ToString());
                CC(ConsoleColor.White);
                Print(" ]");
                CC(ConsoleColor.Green);
                Print($": {functionDesc} |");
            }
            colorIndex++;
        }
        Console.WriteLine();
    }
    public static void PrintError(int code, int row)
    {
        Console.CursorTop = row;
        CC(ConsoleColor.DarkRed);
        Print("HIBA");
        CC(ConsoleColor.White);
        Print(": ");
        switch (code)
        {
            case 0:
                {
                    Print("Fájlkezelési hiba történt.\n");
                    break;
                }
            case 1:
                {
                    Print("Érvénytelen bemeneti formátum.\n");
                    break;
                }
            case 2:
                {
                    Print("A várt érték intervallumon kívülre esett.\n");
                    break;
                }
            case 3:
                {
                    Print("Nem megfelelő tranzakció kategória. Ha kiadás, akkor 0 < x < 14. Ha bevétel, akkor 13 < x < 20.\n");
                    break;
                }
            case 4:
                {
                    Print("Ehhez az évhez mind a 12 hónap fel van jegyezve.\n");
                    break;
                }
            case 5:
                {
                    Print("Ez a hónap már fel van jegyezve ebben az évben.\n");
                    break;
                }
            default:
                {
                    Print("Egyéb hiba történt.\n");
                    break;
                }
        }
        CC(ConsoleColor.Green);
    }
    public static void CC(ConsoleColor color)
    {
        Console.ForegroundColor = color;
    }
    public static void ClearLine(int row)
    {
        Console.CursorTop = row;
        Console.CursorLeft = 0;
        for (int i = 0; i < Console.WindowWidth; i++) Print(" ");
        Console.CursorLeft = 0;
    }
    /// <summary>
    /// Rossz input esetén eltűntetni az előző input maradványait a konzolról.
    /// </summary>
    /// <param name="row">Melyik sor</param>
    /// <param name="column">Melyik oszlopától kezdődik az input</param>
    /// <param name="length">Hány karakter hosszú volt a bekérés?</param>
    public static void ClearInputLeftovers(int row, int column, int length)
    {
        Console.CursorTop = row;
        Console.CursorLeft = column;
        for (int i = 0; i < length; i++) Print(" ");
        Console.CursorTop = row;
        Console.CursorLeft = column;
    }
    /// <summary>
    /// Lekérdezi a kurzor pozícióját.
    /// </summary>
    /// <returns>Kételemű tömb, első érték sor, második oszlop</returns>
    public static int[] GetConsoleCursorPosition()
    {
        return [Console.CursorTop, Console.CursorLeft];
    }
    /// <summary>
    /// Tipp kiíratása a felhasználónak, hogy legyen valami halovány fingja mit kell csinálni.
    /// </summary>
    /// <param name="row">Melyik sorba kell írni a tippet.</param>
    /// <param name="text">Mi a tipp maga.</param>
    public static void PrintTip(int row, string text)
    {
        Console.CursorTop = row;
        Console.CursorLeft = 0;
        CC(ConsoleColor.DarkGray);
        Print(text);
        CC(ConsoleColor.Green);
    }
}