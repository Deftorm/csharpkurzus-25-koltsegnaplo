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
    /// <summary>
    /// Ezzel lehet tetszőleges számú tárggyal rendelkező menü inputját kezelni.
    /// </summary>
    /// <param name="actions">Tartalmazza a lenyomandó gombot, és a hozzárendelt lamdbával hívott függvényt</param>
    public static void HandleMenuInput(Dictionary<ConsoleKey, Action> actions)
    {
        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (actions.TryGetValue(key, out var action))
            {
                Console.Clear();
                action();
                break;
            }
        }
    }
}
