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
    public static void Welcome()
    {
        Console.SetWindowSize(123, Console.WindowHeight);
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("" +
            "...........................................................................................................................\r\n" +
            "...........................................................................................................................\r\n" +
            ".................██╗  ██╗██████╗██╗ ██████████████████████╗██████╗███╗   ██╗█████╗██████╗██╗     ██████╗ ..................\r\n" +
            ".................██║ ██╔██╔═══████║ ╚══██╔══██╔════██╔════██╔════╝████╗  ████╔══████╔══████║    ██╔═══██╗..................\r\n" +
            ".................█████╔╝██║   ████║    ██║  ████████████╗ ██║  █████╔██╗ ███████████████╔██║    ██║   ██║..................\r\n" +
            ".................██╔═██╗██║   ████║    ██║  ╚════████╔══╝ ██║   ████║╚██╗████╔══████╔═══╝██║    ██║   ██║..................\r\n" +
            ".................██║  ██╚██████╔█████████║  ██████████████╚██████╔██║ ╚██████║  ████║    ███████╚██████╔╝..................\r\n" +
            ".................╚═╝  ╚═╝╚═════╝╚══════╚═╝  ╚══════╚══════╝╚═════╝╚═╝  ╚═══╚═╝  ╚═╚═╝    ╚══════╝╚═════╝ ..................\r\n" +
            "...........................................................................................................................\r\n" +
            "...........................................................................................................................\r");

        Console.WriteLine("...........................................................................................................................\r");

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("PROGRAMOZTA:");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(".........");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Németh Márton");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(".........................................................................................");

        Console.WriteLine("...........................................................................................................................\r");

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("NEPTUN  KÓD:");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(".........");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("NA9ZHD");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("................................................................................................");

        Console.WriteLine("...........................................................................................................................\r");

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("KURZUS NEVE:");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(".........");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Alkalmazásfejlesztés c# alapokon a modern fejlesztési irányelvek bemutatásával");
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("........................");

        Console.WriteLine("...........................................................................................................................\r");
        Console.WriteLine("...........................................................................................................................\r");
        Console.WriteLine("...........................................................................................................................\r");
        Console.WriteLine("..........................© 2025 - 2025 Költségnapló Konzolapplikáció - You Have No Rights Bozo............................");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n--// Funkciók navigálása gombnyomással lehetséges. Az elérhető gombokat és funkciókat kilistázzuk. //--");
        Console.ForegroundColor= ConsoleColor.Green;
        Console.WriteLine("\n\nNyomjon meg egy gombot a továbblépéshez!");
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
                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[ ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(key.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ]");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($": {functionDesc} |");
            }
            else
            {
                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[ ");
                Console.ForegroundColor = colors[colorIndex];
                Console.Write(key.ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ]");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($": {functionDesc} |");
            }
            colorIndex++;
        }
    }
}