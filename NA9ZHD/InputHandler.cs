using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Na majd ez kezeli a felhasználói bemenetet. De lehet hogy irányítani fogja a kurzort lol.
/// </summary>
public static class InputHandler
{
    public static void HandleMenuInput(Dictionary<ConsoleKey, Action> actions)
    {
        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (actions.TryGetValue(key, out var action))
            {
                action();
                break;
            }
        }
    }
}
