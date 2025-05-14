using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// A konzolos applikáció "frontend" része. Kezeli a bekérést és a kiíratást is.
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
        Console.WriteLine("                            © 2025 - 2025 Költségnapló Konzolapplikáció - You Have No Rights Bozo.");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
    }
}
