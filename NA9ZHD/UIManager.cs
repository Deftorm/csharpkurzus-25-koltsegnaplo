using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// A konzolos applikáció "frontend" része.
/// Kezeli a bekérést és a kiíratást is.
/// 
/// Mi? Azt mondtam kezeli a bekérést IS? Hülye vagyok én? Meg akarom törni a SRP-t? Mi a f@? Ha input, akkor InputHandler.cs viszont látásra.
/// </summary>
public static class UIManager
{
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
                    Print("Érvénytelen bemenet.\n");
                    break;
                }
            case 2:
                {
                    Print("A várt érték intervallumon kívülre esett.\n");
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
    /// Rossz input esetén eltűntetni az előző input maradványait.
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
        return new int[] { Console.CursorTop, Console.CursorLeft };
    }
    public static void PrintTip(int row, string text)
    {
        Console.CursorTop = row;
        Console.CursorLeft = 0;
        CC(ConsoleColor.DarkGray);
        Print(text);
        CC(ConsoleColor.Green);
    }
}