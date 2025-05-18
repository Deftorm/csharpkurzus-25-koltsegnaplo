using System;
using System.Text.Json;

namespace NA9ZHD;
/// <summary>
/// Szerializációs és fájlokkal való IO műveletekért felelős osztály
/// </summary>
internal class FileManager
{
    public static void WriteMonthToFileJSON(MonthlyLedger month)
    {
        string folderPath = DataWarden.Instance.GetFolderPath();
        string fileName = $"{month.GetYear()}_{month.GetMonth().ToString("D")}.json";
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
        }
        catch (Exception) { UIManager.PrintError(0, 1); }
    }
    public static void ReadAllMonthsFromFilesJSON()
    {
        var options = new JsonSerializerOptions { IncludeFields = true };
        string[] files = Directory.GetFiles(DataWarden.Instance.GetFolderPath(), "*.json");
        foreach (string file in files)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string[] parts = fileName.Split('_');
                if (parts.Length != 2) continue;
                ushort year = ushort.Parse(parts[0]);
                int monthInt = int.Parse(parts[1]);
                if (!Enum.IsDefined(typeof(Months), monthInt)) continue;
                Months month = (Months)monthInt;
                string json = File.ReadAllText(file);
                var transactions = JsonSerializer.Deserialize<List<Transaction>>(json, options);
                if (transactions == null) continue;
                MonthlyLedger ledger = new MonthlyLedger(year, month, transactions);
                DataWarden.Instance.StoreMonth(ledger);
            }
            catch (Exception) { continue; }
        }
    }

}
