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

public enum Months
{
    JANUÁR = 1,
    FEBRUÁR = 2,
    MÁRCIUS = 3,
    ÁPRILIS = 4,
    MÁJUS = 5,
    JÚNIUS = 6,
    JÚLIUS = 7,
    AUGUSZTUS = 8,
    SZEPTEMBER = 9,
    OKTÓBER = 10,
    NOVEMBER = 11,
    DECEMBER = 12
}

public enum TransactionCategory
{
    // Alapkiadások
    Food,               // élelmiszer, bolt
    Dining,             // étterem, kávézó
    Transport,          // tömegközlekedés, taxi, üzemanyag
    Housing,            // albérlet, rezsi
    Utilities,          // áram, víz, internet
    Health,             // gyógyszer, orvos, biztosítás
    Education,          // tankönyv, kurzus, tandíj

    // Személyes kiadások
    Entertainment,      // mozi, streaming, játék
    Shopping,           // ruha, elektronika, kütyük
    Travel,             // utazás, szállás
    Gifts,              // ajándék, adomány
    Fitness,            // edzőterem, sportfelszerelés
    Subscriptions,      // havi előfizetések (Netflix, Spotify stb.)

    // Bevétel típusok
    Salary,             // fizetés
    Freelance,          // másodállás, projektmunkák
    Scholarship,        // ösztöndíj
    Support,            // szülői vagy állami támogatás
    Refund,             // visszatérítés
    // Egyéb
    Other,
}
