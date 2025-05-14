using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace NA9ZHD;
/// <summary>
/// Adatokat tároló Singleton osztály
/// </summary>
public class DataWarden
{
    private static DataWarden? instance;
    
    private readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\KOLTSEGNAPLO";
    private List<MonthlyLedger> months = new List<MonthlyLedger>();

    private DataWarden() { }
    public static DataWarden Instance()
    {
        if (instance == null)
        {
            instance = new DataWarden();
        }
        return instance;
    }

    public void CheckDataFolderExistance()
    {
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
    }
    public void LoadAllMonthsFromFile()
    {
        throw new NotImplementedException();
    }
    public void SaveAllMonths()
    {
        throw new NotImplementedException();
    }
    public List<MonthlyLedger> GetMonthlyLedgers()
    {
        return months;
    }
}
