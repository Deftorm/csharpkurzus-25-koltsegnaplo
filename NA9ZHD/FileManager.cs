using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Szerializációs és fájlokkal való IO műveletekért felelős osztály
/// </summary>
internal class FileManager
{
    public static void WriteMonthToFileJSON(MonthlyLedger month)
    {
        string folderPath = DataWarden.Instance().GetFolderPath();
        string fileName = $"{month.GetYear}_{month.GetMonth().ToString("D2")}.json";
        string fullPath = Path.Combine(folderPath, fileName);
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };
        try
        {
            string json = JsonSerializer.Serialize(month.GetTransactions(), options);
            File.WriteAllText(fullPath, json);
            UIManager.Print("Mentés sikeres");
        }
        catch (Exception) { UIManager.PrintError(0, 1); }
    }
    public static void ReadMonthFromFileJSON(MonthlyLedger month)
    {
        string folderPath = DataWarden.Instance().GetFolderPath();
        string fileName = $"{month.GetYear()}_{month.GetMonth().ToString("D2")}.json";
        string fullPath = Path.Combine(folderPath, fileName);

        if (!File.Exists(fullPath))
        {
            UIManager.PrintError(0, 1);
            return;
        }
        var options = new JsonSerializerOptions
        {
            IncludeFields = true
        };
        try
        {
            string json = File.ReadAllText(fullPath);
            var loadedTransactions = JsonSerializer.Deserialize<List<Transaction>>(json, options);
            if (loadedTransactions != null)
            {
                month.GetTransactions().Clear();
                month.GetTransactions().AddRange(loadedTransactions);
                UIManager.Print($"Beolvasás sikeres: {fileName}\n");
                return;
            }
            else
            {
                UIManager.PrintError(0, 1);
                return;
            }
        }
        catch (Exception)
        {
            UIManager.PrintError(0, 1);
            return;
        }
    }
}
